using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace ArientMusicPlayer {

	//handles things realated to the Window itself (Updating text, displaying stuff)
	//AKA FRONTEND
	public partial class ArientWindow : Form {

		public ArientWindow() {
			InitializeComponent();
			InitializeExtraComponents();
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
			topPanel.MouseDown += new MouseEventHandler(menuPanel_MouseDown);
			//menuPanel.MouseDoubleClick += new MouseEventHandler(UniversalMaximize); //dosent work if MouseDown is there too.
			playlistListView.MouseDoubleClick += new MouseEventHandler(playlistView_MouseDoubleClick);
			playlistListView.MouseClick += new MouseEventHandler(playlistView_MouseDown);
			trayIcon.MouseDoubleClick += new MouseEventHandler(trayIcon_MouseDoubleClick);
			trayIcon.MouseClick += new MouseEventHandler(trayIcon_MouseClick);
			trayIcon.Visible = false;
			menuToolStripMenuItemExit.Click += new EventHandler(UniversalExitApp);
			trayiconToolStripMenuItemExit.Click += new EventHandler(UniversalExitApp);

			this.playlistMenuItemPlayItem.Click += new System.EventHandler(this.playlistMenuItemPlayItem_Click);
			this.playlistMenuItemFileInfo.Click += new System.EventHandler(this.playlistMenuItemFileInfo_Click);
			this.playlistMenuItemAddToPlaylist.Click += new System.EventHandler(this.playlistMenuItemAddToPlaylist_Click);
			this.playlistMenuItemFileLocation.Click += new System.EventHandler(this.playlistMenuItemFileLocation_Click);
			this.playlistMenuItemRemoveFrmPlaylist.Click += new System.EventHandler(this.playlistMenuItemRemoveFrmPlaylist_Click);
			this.playlistMenuItemDeleteFromDisk.Click += new System.EventHandler(this.playlistMenuItemDeleteFromDisk_Click);

		}

		#region Music Player Interface Buttons
		private void btnPlay_Click(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				Arient.TogglePlayPause();
			}
		}

		private void btnPause_Click(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				Arient.PausePlayback();
			}
		}

		private void btnStop_Click(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				Arient.StopPlayback();
			}
		}

		private void btnPrev_Click(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				Arient.PrevTrack();
			}
		}

		private void btnNext_Click(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				Arient.NextTrack();
			}
		}
		#endregion

		#region Playlist Display

		ListViewItem listViewItem;
		public void LoadPlaylistWindow(int loadedPlaylistIndex) {
			playlistListView.Items.Clear();
			for (int i = 0; i < Arient.loadedPlaylists[loadedPlaylistIndex].songs.Length; i++) {
				listViewItem = new ListViewItem((i + 1).ToString());
				listViewItem.SubItems.Add(Arient.loadedPlaylists[loadedPlaylistIndex].songs[i].title);
				listViewItem.SubItems.Add(Arient.loadedPlaylists[loadedPlaylistIndex].songs[i].album);
				listViewItem.SubItems.Add(Arient.loadedPlaylists[loadedPlaylistIndex].songs[i].artist);
				listViewItem.SubItems.Add(Arient.loadedPlaylists[loadedPlaylistIndex].songs[i].duration.ToString());
				listViewItem.SubItems.Add(Arient.loadedPlaylists[loadedPlaylistIndex].songs[i].format);
				playlistListView.Items.Add(listViewItem);
			}
			Arient.currentActivePlaylist = loadedPlaylistIndex; //this should be the only place where
																//currentActivePlaylist should be changed
																//i.e. loading a new playlist into view
		}

		public void OnChangeTrackPlaylist(int newTrackIndex) {
			playlistListView.Select();
			playlistListView.Items[newTrackIndex].Focused = true;

			//Clear all other selections
			foreach (ListViewItem item in playlistListView.Items) {
				item.Selected = false;
			}

			playlistListView.Items[newTrackIndex].Selected = true;
			playlistListView.Items[newTrackIndex].EnsureVisible();
		}

		#endregion

		#region Windows Stuff (Dragging, Minimizing, tray, Exit etc)
		public const int WM_NCLBUTTONDOWN = 0xA1;
		public const int HT_CAPTION = 0x2;

		[DllImportAttribute("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd,
						 int Msg, int wParam, int lParam);
		[DllImportAttribute("user32.dll")]
		public static extern bool ReleaseCapture();

		private void menuPanel_MouseDown(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				//Handles Holding Down and Double Clicking.
				if (e.Clicks == 1) {
					UniversalDragWindowAction(sender, e);
				} else if (e.Clicks == 2) {
					UniversalMaximize(sender, e);
				}
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
			if (Arient.settingMinToTray) {
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
			Application.Exit();
		}

		#endregion

		#region Playlistview Controls
		void playlistView_MouseDoubleClick(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				ListViewHitTestInfo info = playlistListView.HitTest(e.X, e.Y);
				ListViewItem item = info.Item;

				if (item != null) {
					MessageBox.Show("The selected Item Name is: " + item.Text);
				} else {
					playlistListView.SelectedItems.Clear();
					MessageBox.Show("No Item is selected");
				}
			}
		}



		void playlistView_MouseDown(object sender, MouseEventArgs e) {
			ListViewHitTestInfo info = playlistListView.HitTest(e.X, e.Y);
			ListViewItem item = info.Item;

			if (item != null) {
				labelMainTitle.Text = item.Text;
			} else {
				playlistListView.SelectedItems.Clear();
				labelMainTitle.Text = "No Item is Selected";
			}
		}

		#endregion

		#region Context Menu Playlist
		private void playlistMenuItemPlayItem_Click(object sender, EventArgs e) {
			Arient.StartPlayback(int.Parse(playlistListView.SelectedItems[0].Text) - 1);
		}

		private void playlistMenuItemAddToPlaylist_Click(object sender, EventArgs e) {

		}

		private void playlistMenuItemMoveUp_Click(object sender, EventArgs e) {

		}

		private void playlistMenuItemMoveDown_Click(object sender, EventArgs e) {

		}

		private void playlistMenuItemMoveTop_Click(object sender, EventArgs e) {

		}

		private void playlistMenuItemMoveBottom_Click(object sender, EventArgs e) {

		}

		private void playlistMenuItemFileInfo_Click(object sender, EventArgs e) {

		}

		private void playlistMenuItemFileLocation_Click(object sender, EventArgs e) {

		}

		private void playlistMenuItemRemoveFrmPlaylist_Click(object sender, EventArgs e) {

		}

		private void playlistMenuItemDeleteFromDisk_Click(object sender, EventArgs e) {

		}

		#endregion
	}
}
