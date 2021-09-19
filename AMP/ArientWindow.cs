using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;


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

			playlistListView.SelectedIndexChanged += new EventHandler(OnPlaylistWindowSelectionChange);
			playlistSelectComboBox.SelectedIndexChanged += new EventHandler(OnPlaylistSelectComboBoxChange);
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

		#region Playlist Controls & display
		void playlistView_MouseDoubleClick(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				ListViewHitTestInfo info = playlistListView.HitTest(e.X, e.Y);
				ListViewItem item = info.Item;

				if (item != null) {
					Arient.StartPlayback(Arient.currentDisplayedPlaylist,int.Parse(item.SubItems[0].Text)-1);
				}
			}
		}

		void playlistView_MouseDown(object sender, MouseEventArgs e) {

			ListViewHitTestInfo info = playlistListView.HitTest(e.X, e.Y);
			ListViewItem item = info.Item;

			if (e.Button == MouseButtons.Left) {
				
			}

			if (e.Button == MouseButtons.Right) {
				if (item != null) {
					Point pos = PointToClient(Cursor.Position);
					playlistContextMenuStrip.Show(this, pos);
				}
			}
		}

		ListViewItem listViewItem;
		public void LoadPlaylistWindow(int displayPlaylistIndex) {
			playlistListView.Items.Clear();
			for (int i = 0; i < Arient.loadedPlaylists[displayPlaylistIndex].songs.Count; i++) {
				listViewItem = new ListViewItem((i + 1).ToString());
				listViewItem.SubItems.Add(Arient.loadedPlaylists[displayPlaylistIndex].songs[i].title);
				listViewItem.SubItems.Add(Arient.loadedPlaylists[displayPlaylistIndex].songs[i].album);
				listViewItem.SubItems.Add(Arient.loadedPlaylists[displayPlaylistIndex].songs[i].artist);
				listViewItem.SubItems.Add(Arient.loadedPlaylists[displayPlaylistIndex].songs[i].duration.ToString());
				listViewItem.SubItems.Add(Arient.loadedPlaylists[displayPlaylistIndex].songs[i].format);
				playlistListView.Items.Add(listViewItem);
			}
			Arient.currentDisplayedPlaylist = displayPlaylistIndex; //this should be the only place where
																//currentActivePlaylist should be changed
																//i.e. loading a new playlist into view
		}

		public void LoadLibraryWindow() {
			libraryListView.Items.Clear();
			for (int i = 0; i < Arient.libraryPlaylist.songs.Count; i++) {
				listViewItem = new ListViewItem((i + 1).ToString());
				listViewItem.SubItems.Add(Arient.libraryPlaylist.songs[i].title);
				listViewItem.SubItems.Add(Arient.libraryPlaylist.songs[i].album);
				listViewItem.SubItems.Add(Arient.libraryPlaylist.songs[i].artist);
				listViewItem.SubItems.Add(Arient.libraryPlaylist.songs[i].duration.ToString());
				listViewItem.SubItems.Add(Arient.libraryPlaylist.songs[i].format);
				libraryListView.Items.Add(listViewItem);
			}
		}

		public void OnChangeTrackPlaylist(int newTrackIndex) {

		//If user is looking at the currently playing playlist, update the selections.
		//if not, then don't fuck up whatever the user is doing on another playlist.
			if (Arient.currentDisplayedPlaylist == Arient.currentPlayingPlaylist) {
				playlistListView.Select();
				playlistListView.Items[newTrackIndex].Focused = true;

				//Clear all other selections
				foreach (ListViewItem item in playlistListView.Items) {
					item.Selected = false;
				}

				playlistListView.Items[newTrackIndex].Selected = true;
				playlistListView.Items[newTrackIndex].EnsureVisible();
			}
		}

		public void OnPlaylistWindowSelectionChange(object sender, EventArgs e) {
			
			if (playlistListView.SelectedItems.Count > 0) {
				TagInfo tag = Arient.GetLoadedTagInfo(Arient.currentDisplayedPlaylist, int.Parse(playlistListView.SelectedItems[0].SubItems[0].Text) - 1);
				//Generate text for display. Store in an array. Will be 4 lines tall.
				string[] display = new string[4];

				TimeSpan ts = TimeSpan.FromSeconds((int)tag.duration);
				display[0] = tag.title + " [" + String.Format("{0}:{1:D2}", ts.Minutes, ts.Seconds) + "]";
				display[1] = tag.artist;
				display[2] = tag.album;

				if (tag.format != "") {
					display[3] += tag.format + " | ";
				}
				if (tag.size != 0) {
					display[3] += (tag.size / 1048576f).ToString("0.00") + "mb, ";
				}
				if (tag.frequency != 0) {
					display[3] += tag.frequency + "hz, ";
				}
				if (tag.bitrate != 0) {
					display[3] += tag.bitrate + "kbps, ";
				}
				if (tag.bpm != "" && tag.bpm != null) {
					display[3] += tag.bpm + "bpm, ";
				}

				//remove trailing space and comma if any
				display[3] = display[3].TrimEnd(',', ' ');

				labelPlaylistSelection.Lines = display;
			}

		}

		public void UpdateMainTitle(TagInfo tag) {

			//TimeSpan ts = TimeSpan.FromSeconds((int)tag.duration);
			labelMainTitle.Text = tag.title;
			labelMainArtist.Text = tag.artist;
			labelMainAlbum.Text = tag.album;
			string info = "";
			if (tag.format != "") {
				info += tag.format + " | ";
			}
			if (tag.size != 0) {
				info += (tag.size / 1048576f).ToString("0.00") + "mb, ";
			}
			if (tag.frequency != 0) {
				info += tag.frequency + "hz, ";
			}
			if (tag.bitrate != 0) {
				info += tag.bitrate + "kbps, ";
			}
			if (tag.bpm != "" && tag.bpm != null) {
				info += tag.bpm + "bpm, ";
			}

			//remove trailing space and comma if any
			info = info.TrimEnd(',', ' ');

			labelMainExtraInfo.Text = info;
		}

		//Same as above but with zero info.
		public void UpdateMainTitle() {
			labelMainTitle.Text = "Stopped";
			labelMainArtist.Text = "";
			labelMainAlbum.Text = "";
			labelMainExtraInfo.Text = "";
		}

		//Playlist Combo Box stuff
		public void UpdatePlaylistSelectComboBox(int indexToDisplay) {
			playlistSelectComboBox.Items.Clear();
			foreach (Playlist playlist in Arient.loadedPlaylists) {
				playlistSelectComboBox.Items.Add(playlist.name); //added items should be
																 //in same order as loadedPlaylists.
			}
			if (indexToDisplay >= 0) {
				playlistSelectComboBox.SelectedIndex = indexToDisplay;
			}
		}

		public void OnPlaylistSelectComboBoxChange(object sender, EventArgs e) {
			//Load new Playlist in ListView.
			//if the current displaying playlist is the same
			//as the currently loaded one, dont change.

			LoadPlaylistWindow(playlistSelectComboBox.SelectedIndex); //Assuming playlistSelectComboBox items
																	  //are still in order with loadedPlaylists
		}

		#endregion

		#region Context Menu Playlist
		private void playlistMenuItemPlayItem_Click(object sender, EventArgs e) {
			Arient.StartPlayback(Arient.currentDisplayedPlaylist, int.Parse(playlistListView.SelectedItems[0].Text) - 1);
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
