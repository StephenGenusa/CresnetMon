namespace CresNetMon
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.label1 = new System.Windows.Forms.Label();
			this.selComPort = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtDeviceId = new System.Windows.Forms.TextBox();
			this.btnRefreshPorts = new System.Windows.Forms.Button();
			this.btnStart = new System.Windows.Forms.Button();
			this.viewResults = new System.Windows.Forms.ListView();
			this.colId = new System.Windows.Forms.ColumnHeader();
			this.colTime = new System.Windows.Forms.ColumnHeader();
			this.colDevice = new System.Windows.Forms.ColumnHeader();
			this.colSent = new System.Windows.Forms.ColumnHeader();
			this.colReceive = new System.Windows.Forms.ColumnHeader();
			this.btnClear = new System.Windows.Forms.Button();
			this.statusBar = new System.Windows.Forms.StatusStrip();
			this.statusText = new System.Windows.Forms.ToolStripStatusLabel();
			this.label4 = new System.Windows.Forms.Label();
			this.statusBar.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(32, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "COM Port";
			// 
			// selCresNetPort
			// 
			this.selComPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.selComPort.FormattingEnabled = true;
			this.selComPort.Location = new System.Drawing.Point(12, 30);
			this.selComPort.Name = "selCresNetPort";
			this.selComPort.Size = new System.Drawing.Size(87, 21);
			this.selComPort.Sorted = true;
			this.selComPort.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(129, 14);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(55, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "Device ID";
			// 
			// txtDeviceId
			// 
			this.txtDeviceId.Location = new System.Drawing.Point(145, 30);
			this.txtDeviceId.Name = "txtDeviceId";
			this.txtDeviceId.Size = new System.Drawing.Size(24, 20);
			this.txtDeviceId.TabIndex = 2;
			// 
			// btnRefreshPorts
			// 
			this.btnRefreshPorts.Image = ((System.Drawing.Image)(resources.GetObject("btnRefreshPorts.Image")));
			this.btnRefreshPorts.Location = new System.Drawing.Point(105, 29);
			this.btnRefreshPorts.Name = "btnRefreshPorts";
			this.btnRefreshPorts.Size = new System.Drawing.Size(24, 23);
			this.btnRefreshPorts.TabIndex = 3;
			this.btnRefreshPorts.UseVisualStyleBackColor = true;
			this.btnRefreshPorts.Click += new System.EventHandler(this.btnRefreshPorts_Click);
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(12, 58);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(75, 23);
			this.btnStart.TabIndex = 4;
			this.btnStart.Text = "Start";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// viewResults
			// 
			this.viewResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.viewResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colId,
            this.colTime,
            this.colDevice,
            this.colSent,
            this.colReceive});
			this.viewResults.GridLines = true;
			this.viewResults.Location = new System.Drawing.Point(13, 88);
			this.viewResults.Name = "viewResults";
			this.viewResults.Size = new System.Drawing.Size(391, 200);
			this.viewResults.TabIndex = 5;
			this.viewResults.UseCompatibleStateImageBehavior = false;
			this.viewResults.View = System.Windows.Forms.View.Details;
			// 
			// colId
			// 
			this.colId.Text = "ID";
			this.colId.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.colId.Width = 44;
			// 
			// colTime
			// 
			this.colTime.Text = "Time";
			// 
			// colDevice
			// 
			this.colDevice.Text = "Dev";
			this.colDevice.Width = 36;
			// 
			// colSent
			// 
			this.colSent.Text = "Sent";
			this.colSent.Width = 120;
			// 
			// colReceive
			// 
			this.colReceive.Text = "Received";
			this.colReceive.Width = 122;
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(94, 58);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(75, 23);
			this.btnClear.TabIndex = 6;
			this.btnClear.Text = "Clear";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// statusBar
			// 
			this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusText});
			this.statusBar.Location = new System.Drawing.Point(0, 291);
			this.statusBar.Name = "statusBar";
			this.statusBar.Size = new System.Drawing.Size(416, 22);
			this.statusBar.TabIndex = 8;
			this.statusBar.Text = "statusStrip1";
			// 
			// statusText
			// 
			this.statusText.Name = "statusText";
			this.statusText.Size = new System.Drawing.Size(0, 17);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(175, 33);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(63, 13);
			this.label4.TabIndex = 9;
			this.label4.Text = "Blank for All";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(416, 313);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.statusBar);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.viewResults);
			this.Controls.Add(this.btnStart);
			this.Controls.Add(this.btnRefreshPorts);
			this.Controls.Add(this.txtDeviceId);
			this.Controls.Add(this.selComPort);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Name = "MainForm";
			this.Text = "Cresnet Monitor";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.statusBar.ResumeLayout(false);
			this.statusBar.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox selComPort;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtDeviceId;
		private System.Windows.Forms.Button btnRefreshPorts;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.ListView viewResults;
		private System.Windows.Forms.ColumnHeader colSent;
		private System.Windows.Forms.ColumnHeader colReceive;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.StatusStrip statusBar;
		private System.Windows.Forms.ToolStripStatusLabel statusText;
		private System.Windows.Forms.ColumnHeader colId;
		private System.Windows.Forms.ColumnHeader colTime;
		private System.Windows.Forms.ColumnHeader colDevice;
		private System.Windows.Forms.Label label4;
	}
}

