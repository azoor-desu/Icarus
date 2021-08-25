using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace ArientMusicPlayer {
	
	//handles things realated to the Window itself (Updating text, displaying stuff)
	//AKA FRONTEND
	public partial class ArientWindow : Form {

        ArientBackend arientBackend;

		public const int WM_NCLBUTTONDOWN = 0xA1;
		public const int HT_CAPTION = 0x2;

		[DllImportAttribute("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd,
						 int Msg, int wParam, int lParam);
		[DllImportAttribute("user32.dll")]
		public static extern bool ReleaseCapture();

		public ArientWindow() {
			InitializeComponent();
			InitializeExtraComponents();
			arientBackend = new ArientBackend(this);
		}

		//Contains all the Events for buttons and Mouse actions
		void InitializeExtraComponents() {
			btnPlay.MouseClick += new MouseEventHandler(btnPlay_Click);
			btnPause.MouseClick += new MouseEventHandler(btnPause_Click);
			btnStop.MouseClick += new MouseEventHandler(btnStop_Click);
			btnPrev.MouseClick += new MouseEventHandler(btnPrev_Click);
			btnNext.MouseClick += new MouseEventHandler(btnNext_Click);
			btnExitApp.Click += new EventHandler(UniversalExitApp);
			btnMinimize.MouseClick += new MouseEventHandler(btnMinimize_Click);
			MouseDown += new MouseEventHandler(UniversalDragWindowAction);
			menuPanel.MouseDown += new MouseEventHandler(menuPanel_MouseDown);
			//menuPanel.MouseDoubleClick += new MouseEventHandler(UniversalMaximize); //dosent work if MouseDown is there too.
			playlistListView.MouseDoubleClick += new MouseEventHandler(playlistView_MouseDoubleClick);
			playlistListView.MouseClick += new MouseEventHandler(playlistView_MouseDown);
			trayIcon.MouseDoubleClick += new MouseEventHandler(trayIcon_MouseDoubleClick);
			trayIcon.MouseClick += new MouseEventHandler(trayIcon_MouseClick);
			trayIcon.Visible = false;
			menuToolStripMenuItemExit.Click += new EventHandler(UniversalExitApp);
			trayiconToolStripMenuItemExit.Click += new EventHandler(UniversalExitApp);
		}

		#region Music Player Interface Buttons
		private void btnPlay_Click(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				arientBackend.TogglePlayPause();
			}
		}

		private void btnPause_Click(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				arientBackend.PausePlayback();
			}
		}

		private void btnStop_Click(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				arientBackend.StopPlayback();
			}
		}

		private void btnPrev_Click(object sender, MouseEventArgs e) {
					if (e.Button == MouseButtons.Left) {
						arientBackend.PrevTrack();
					}
		}

		private void btnNext_Click(object sender, MouseEventArgs e) {
						if (e.Button == MouseButtons.Left) {
							arientBackend.NextTrack();
						}
		}

		
		#endregion

		#region Playlist Display

		public void UpdatePlaylistWindow(string[] fileNames) {
			
			foreach (string name in fileNames) {
				//listViewItem.Name = name;
				ListViewItem listViewItem = new ListViewItem(name);
				playlistListView.Items.Add(listViewItem);
			}

			for (int i = 0; i < 100; i++) {
				ListViewItem listViewItem = new ListViewItem("Dustin Biebere - Bebe\nMy Hart weel go on || ALLAH IS GREATEST");
				playlistListView.Items.Add(listViewItem);
			}
		}

		#endregion

		#region Windows Stuff (Dragging, Minimizing, tray, Exit etc)
		private void menuPanel_MouseDown(object sender, MouseEventArgs e) {
			//Handles Holding Down and Double Clicking.
			if (e.Clicks == 1) {
				UniversalDragWindowAction(sender, e);
			} else if (e.Clicks == 2){
				UniversalMaximize(sender, e);
			}
		}

		private void UniversalDragWindowAction(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				ReleaseCapture();
				SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
			}
		}

		private void UniversalMaximize(object sender, MouseEventArgs e) {
			//Do Fullscreen/regular toggle
			if (WindowState == FormWindowState.Maximized) {
				WindowState = FormWindowState.Normal;
			} else {
				WindowState = FormWindowState.Maximized;
			}
		}

		private void btnMinimize_Click(object sender, MouseEventArgs e) {

			WindowState = FormWindowState.Minimized;
			if (arientBackend.settingMinToTray) {
				//Pressing Minimize minimizes to tray.
				Hide();
				ShowInTaskbar = false;
				trayIcon.Visible = true;
			}
		}

		private void trayIcon_MouseDoubleClick(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				Show();
				WindowState = FormWindowState.Normal;
				ShowInTaskbar = true;
				trayIcon.Visible = false;
			}
		}

		private void trayIcon_MouseClick(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				//Left Click Action
			}

			if (e.Button == MouseButtons.Middle) {
				//Mid Click Action
			}

			if (e.Button == MouseButtons.Right) {
				//Right Click Action
			}
		}

		private void UniversalExitApp(object sender, EventArgs e) {
			MainEntryPoint.ExitApplication();
		}

		#endregion

		#region Playlistview Controls
		void playlistView_MouseDoubleClick(object sender, MouseEventArgs e) {
			ListViewHitTestInfo info = playlistListView.HitTest(e.X, e.Y);
			ListViewItem item = info.Item;

			if (item != null) {
				MessageBox.Show("The selected Item Name is: " + item.Text);
			} else {
				playlistListView.SelectedItems.Clear();
				MessageBox.Show("No Item is selected");
			}
		}



		void playlistView_MouseDown(object sender, MouseEventArgs e) {
			ListViewHitTestInfo info = playlistListView.HitTest(e.X, e.Y);
			ListViewItem item = info.Item;

			if (item != null) {
				songName.Text = item.Text;
			} else {
				playlistListView.SelectedItems.Clear();
				songName.Text = "No Item is Selected";
			}
		}

		#endregion

		#region Context Menu

		#endregion
	}


}
