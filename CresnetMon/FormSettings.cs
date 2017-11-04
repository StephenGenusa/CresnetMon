using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CresNetMon
{
	public class FormSettings
	{
		#region Constructors
		
		internal FormSettings() { }
		internal FormSettings(Form form) { SaveForm(form); }

		#endregion


		#region Public Fields - saved in Settings file

		public Point Location;
		public Size Size;
		public bool IsMaximized;

		#endregion


		#region Native Windows Interface
		
		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

		private struct WINDOWPLACEMENT
		{
			public int length;
			public int flags;
			public int showCmd;
			public System.Drawing.Point ptMinPosition;
			public System.Drawing.Point ptMaxPosition;
			public System.Drawing.Rectangle rcNormalPosition;
		}

		public static FormWindowState GetRestoreWindowState(Form form)
		{
			const int WPF_RESTORETOMAXIMIZED = 0x2;
			WINDOWPLACEMENT placement = new WINDOWPLACEMENT();
			placement.length = Marshal.SizeOf(placement);
			GetWindowPlacement(form.Handle, ref placement);

			if ((placement.flags & WPF_RESTORETOMAXIMIZED) == WPF_RESTORETOMAXIMIZED)
				return FormWindowState.Maximized;
			else
				return FormWindowState.Normal;
		}

		#endregion


		#region Internal Methods

		internal void SaveForm(Form form)
		{
			FormWindowState state;
			Rectangle bounds;

			state = form.WindowState;
			bounds = state == FormWindowState.Normal ? form.Bounds : form.RestoreBounds;
			Location = bounds.Location;
			Size = bounds.Size;

			if (state == FormWindowState.Minimized)
				state = GetRestoreWindowState(form);
			IsMaximized = state == FormWindowState.Maximized;
		}

		internal bool RestoreForm(Form form)
		{
			if (Size.Height == 0)
				return false;

			form.Location = Location;
			form.Size = Size;
			if (IsMaximized)
				form.WindowState = FormWindowState.Maximized;
			return true;
		}

		internal static bool RestoreForm(FormSettings settings, Form form)
		{
			if (settings != null)
				return settings.RestoreForm(form);
			return false;
		}

		#endregion
	}
}
