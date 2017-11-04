using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CresNetMon.Properties;

namespace CresNetMon
{
	public partial class MainForm : Form
	{
		#region Constants

		const string StrStatus = "Polling count: ";
		const int MessageSize = 10;
		const int MasterAddr = 0x02;
		const int MinMsgAddr = MasterAddr;
		const int MaxMsgAddr = 0xFE;
		const int MaxMsgSize = 30;

		#endregion


		#region Types

		enum SerialState
		{
			Searching,
			Ready,
			Addressed,
			Payload,
		}

		delegate void DisplayMessageHandler(string strMes, byte bDevId, bool fToMaster);

		#endregion


		#region Fields

		bool m_fIsRunning;
		SerialPort m_serCresnet;
		byte m_bDevId;
		byte m_bSendId;
		byte m_bDestId;
		byte m_bPollId;
		int m_iMsgSize;
		SerialState m_state;
		List<byte> m_lstMessage;
		int m_iMsgCnt;
		DisplayMessageHandler DisplayMessageDelegate;

		#endregion


		#region Constructor

		public MainForm()
		{
			InitializeComponent();
			m_lstMessage = new List<byte>();
			DisplayMessageDelegate = new DisplayMessageHandler(DisplayMessage);
		}

 		#endregion


		#region Private Methods

		void RefreshPorts()
		{
			string[] arstrPorts;
			string str;

			arstrPorts = SerialPort.GetPortNames();

			str = (string)selComPort.SelectedItem;
			selComPort.Items.Clear();
			selComPort.Items.AddRange(arstrPorts);
			selComPort.SelectedItem = str;
		}

		bool OpenPort(string strPort, ref SerialPort port)
		{
			try
			{
				port = new SerialPort(strPort, 38400);
				port.Open();
			}
			catch (Exception exc)
			{
				Debug.WriteLine("Port open failed: " + exc.Message);
				return false;
			}
			return true;
		}

		string FormatMessage(List<byte> mes)
		{
			StringBuilder stb;
			stb = new StringBuilder(mes.Count * 3);
			foreach (byte b in mes)
			{
				stb.Append(b.ToString("X2"));
				stb.Append(' ');
			}
			return stb.ToString();
		}

		void ShowStatus()
		{
			statusText.Text = StrStatus + m_iMsgCnt.ToString();
		}

		void DisplayMessage(string strMes, byte bDevId, bool fToMaster)
		{
			ListViewItem item;


			if (strMes == null)
			{
				m_iMsgCnt++;
				ShowStatus();
				return;
			}

			item = new ListViewItem(new string[] {
				m_iMsgCnt.ToString(), 
				DateTime.Now.ToString("H:mm:ss"), 
				bDevId.ToString("X2"),
				fToMaster ? "" : strMes, 
				fToMaster ? strMes : "" 
			});
			viewResults.Items.Add(item);
		}

		// Executed in a different thread
		void CresNetProcessByte()
		{
			byte	b;

			while (m_serCresnet.IsOpen)
			{
				try
				{
					b = (byte)m_serCresnet.ReadByte();

					switch (m_state)
					{
						case SerialState.Searching:
							if (b == 0)
								m_state = SerialState.Ready;
							break;

						case SerialState.Ready:
							if (b >= MinMsgAddr && b < MaxMsgAddr)
							{
								m_state = SerialState.Addressed;
								m_bDestId = b;
								if (b != MasterAddr)
									m_bSendId = b;
							}
							else if (b != 0)
								m_state = SerialState.Searching;
							// Leave in Ready state if zero
							break;

						case SerialState.Addressed:
							if (b > MaxMsgSize)
								m_state = SerialState.Searching;

							else if (b != 0)
							{
								m_iMsgSize = b;
								m_state = SerialState.Payload;
							}
							else
							{
								m_state = SerialState.Ready;
								// Count polling cycles
								if (m_bDestId != MasterAddr)
								{
									// Choose an ID to represent the polling cycle
									if (m_bPollId == 0)
										m_bPollId = m_bDestId;
									if (m_bDestId == m_bPollId)
										BeginInvoke(DisplayMessageDelegate, null, (byte)0, false);
								}
							}
							break;

						case SerialState.Payload:
							m_lstMessage.Add(b);
							m_iMsgSize--;
							if (m_iMsgSize == 0)
								ShowMessage();
							break;
					}
				}
				catch (Exception exc)
				{
					Debug.WriteLine("Exception in CresNetProcessByte: " + exc.Message);
					return;
				}
			}
		}

		void ShowMessage()
		{
			byte bDest;

			bDest = m_bDestId == MasterAddr ? m_bSendId : m_bDestId;

			if (m_bDevId == 0 || bDest == m_bDevId)
			{
				BeginInvoke(DisplayMessageDelegate, FormatMessage(m_lstMessage), bDest, m_bDestId == MasterAddr);
			}
			m_lstMessage.Clear();
			m_state = SerialState.Ready;
		}

		#endregion


		#region Event Handlers

		private void MainForm_Load(object sender, EventArgs e)
		{
			// Read in saved per-user settings
			if (!Settings.Default.SettingsUpgraded)
			{
				Settings.Default.Upgrade();
				Settings.Default.SettingsUpgraded = true;
				Settings.Default.Save();
			}
			FormSettings.RestoreForm(Settings.Default.MainForm, this);

			m_fIsRunning = false;
			txtDeviceId.Text = Settings.Default.DeviceId;
			RefreshPorts();
			selComPort.SelectedItem = Settings.Default.ComPort;
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (m_serCresnet != null && m_serCresnet.IsOpen)
				m_serCresnet.Close();

			Settings.Default.MainForm = new FormSettings(this);
			Settings.Default.DeviceId = txtDeviceId.Text;
			Settings.Default.ComPort = (string)selComPort.SelectedItem;
			Settings.Default.Save();
		}

		private void btnRefreshPorts_Click(object sender, EventArgs e)
		{
			RefreshPorts();
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			if (m_fIsRunning)
			{
				m_serCresnet.Close();
				btnStart.Text = "Start";
				m_fIsRunning = false;
			}
			else
			{
				if (string.IsNullOrEmpty(txtDeviceId.Text))
					m_bDevId = 0;

				else if (!byte.TryParse(txtDeviceId.Text, NumberStyles.AllowHexSpecifier, null, out m_bDevId))
				{
					MessageBox.Show("Invalid Device ID", "Cresnet Monitor", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				if (!OpenPort((string)selComPort.SelectedItem, ref m_serCresnet))
					return;

				m_lstMessage = new List<byte>(MessageSize);
				m_state = SerialState.Searching;
				btnStart.Text = "Stop";
				m_fIsRunning = true;
				new Thread(CresNetProcessByte).Start();
			}
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			viewResults.Items.Clear();
			m_iMsgCnt = 0;
			if (!m_fIsRunning)
				m_bPollId = 0;
			ShowStatus();
		}

		#endregion

	}
}
