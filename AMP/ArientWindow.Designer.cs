
namespace ArientMusicPlayer
{
	partial class ArientWindow
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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.ColumnHeader entryTitle;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ArientWindow));
			this.btnPlay = new System.Windows.Forms.Button();
			this.btnPause = new System.Windows.Forms.Button();
			this.btnStop = new System.Windows.Forms.Button();
			this.playlistContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.playlistMenuItemPlayItem = new System.Windows.Forms.ToolStripMenuItem();
			this.playlistMenuSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.playlistMenuItemMoveUp = new System.Windows.Forms.ToolStripMenuItem();
			this.playlistMenuItemMoveDown = new System.Windows.Forms.ToolStripMenuItem();
			this.playlistMenuItemMoveTop = new System.Windows.Forms.ToolStripMenuItem();
			this.playlistMenuItemMoveBottom = new System.Windows.Forms.ToolStripMenuItem();
			this.playlistMenuSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.playlistMenuItemFileInfo = new System.Windows.Forms.ToolStripMenuItem();
			this.playlistMenuItemFileLocation = new System.Windows.Forms.ToolStripMenuItem();
			this.playlistMenuSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.playlistMenuItemAddToPlaylist = new System.Windows.Forms.ToolStripMenuItem();
			this.playlistMenuItemRemoveFrmPlaylist = new System.Windows.Forms.ToolStripMenuItem();
			this.playlistMenuItemDeleteFromDisk = new System.Windows.Forms.ToolStripMenuItem();
			this.labelMainTitle = new System.Windows.Forms.TextBox();
			this.btnExitApp = new System.Windows.Forms.Button();
			this.btnPrev = new System.Windows.Forms.Button();
			this.btnNext = new System.Windows.Forms.Button();
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.menuContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.menuToolStripMenuItemImportPlaylist = new System.Windows.Forms.ToolStripMenuItem();
			this.menuToolStripMenuItemExportPlaylist = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.menuToolStripMenuItemOptions = new System.Windows.Forms.ToolStripMenuItem();
			this.menuToolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStripMenuLabel = new System.Windows.Forms.ToolStripMenuItem();
			this.syncToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.trayContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.trayiconToolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
			this.btnMinimize = new System.Windows.Forms.Button();
			this.topPanel = new System.Windows.Forms.Panel();
			this.musicProgressTrackBar = new System.Windows.Forms.TrackBar();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.labelMainArtist = new System.Windows.Forms.TextBox();
			this.labelMainAlbum = new System.Windows.Forms.TextBox();
			this.labelMainExtraInfo = new System.Windows.Forms.TextBox();
			this.audioLevelTrackBar = new System.Windows.Forms.TrackBar();
			this.checkBoxShuffle = new System.Windows.Forms.CheckBox();
			this.checkBoxRepeat = new System.Windows.Forms.CheckBox();
			this.labelPlaylistSelection = new System.Windows.Forms.TextBox();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.btnRenamePlaylist = new System.Windows.Forms.Button();
			this.btnDeletePlaylist = new System.Windows.Forms.Button();
			this.btnCreateNewPlaylist = new System.Windows.Forms.Button();
			this.playlistSelectComboBox = new System.Windows.Forms.ComboBox();
			this.panel3 = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.labelVolumeLevel = new System.Windows.Forms.TextBox();
			this.labelPlaybackTime = new System.Windows.Forms.TextBox();
			this.PlaylistPanel = new System.Windows.Forms.Panel();
			this.playlistListView = new System.Windows.Forms.ListView();
			this.entryID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.entryAlbum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.entryArtist = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.entrySongLength = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.entryFileFormat = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.panel4 = new System.Windows.Forms.Panel();
			this.PlayerPanel = new System.Windows.Forms.Panel();
			entryTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.playlistContextMenuStrip.SuspendLayout();
			this.menuStrip.SuspendLayout();
			this.menuContextMenuStrip.SuspendLayout();
			this.trayContextMenuStrip.SuspendLayout();
			this.topPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.musicProgressTrackBar)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.audioLevelTrackBar)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			this.panel1.SuspendLayout();
			this.PlaylistPanel.SuspendLayout();
			this.panel4.SuspendLayout();
			this.PlayerPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// entryTitle
			// 
			entryTitle.Text = "Title";
			entryTitle.Width = 108;
			// 
			// btnPlay
			// 
			this.btnPlay.Location = new System.Drawing.Point(97, 260);
			this.btnPlay.Name = "btnPlay";
			this.btnPlay.Size = new System.Drawing.Size(71, 42);
			this.btnPlay.TabIndex = 0;
			this.btnPlay.Text = "Play/Pause";
			this.btnPlay.UseVisualStyleBackColor = true;
			// 
			// btnPause
			// 
			this.btnPause.Location = new System.Drawing.Point(174, 260);
			this.btnPause.Name = "btnPause";
			this.btnPause.Size = new System.Drawing.Size(52, 42);
			this.btnPause.TabIndex = 3;
			this.btnPause.Text = "Pause";
			this.btnPause.UseVisualStyleBackColor = true;
			// 
			// btnStop
			// 
			this.btnStop.Location = new System.Drawing.Point(231, 260);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(66, 42);
			this.btnStop.TabIndex = 4;
			this.btnStop.Text = "Stop";
			this.btnStop.UseVisualStyleBackColor = true;
			// 
			// playlistContextMenuStrip
			// 
			this.playlistContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playlistMenuItemPlayItem,
            this.playlistMenuSeparator3,
            this.playlistMenuItemMoveUp,
            this.playlistMenuItemMoveDown,
            this.playlistMenuItemMoveTop,
            this.playlistMenuItemMoveBottom,
            this.playlistMenuSeparator1,
            this.playlistMenuItemFileInfo,
            this.playlistMenuItemFileLocation,
            this.playlistMenuSeparator2,
            this.playlistMenuItemAddToPlaylist,
            this.playlistMenuItemRemoveFrmPlaylist,
            this.playlistMenuItemDeleteFromDisk});
			this.playlistContextMenuStrip.Name = "playlistContextMenuStrip";
			this.playlistContextMenuStrip.Size = new System.Drawing.Size(187, 242);
			// 
			// playlistMenuItemPlayItem
			// 
			this.playlistMenuItemPlayItem.Name = "playlistMenuItemPlayItem";
			this.playlistMenuItemPlayItem.Size = new System.Drawing.Size(186, 22);
			this.playlistMenuItemPlayItem.Text = "Play File";
			// 
			// playlistMenuSeparator3
			// 
			this.playlistMenuSeparator3.Name = "playlistMenuSeparator3";
			this.playlistMenuSeparator3.Size = new System.Drawing.Size(183, 6);
			// 
			// playlistMenuItemMoveUp
			// 
			this.playlistMenuItemMoveUp.Name = "playlistMenuItemMoveUp";
			this.playlistMenuItemMoveUp.Size = new System.Drawing.Size(186, 22);
			this.playlistMenuItemMoveUp.Text = "Move Up";
			this.playlistMenuItemMoveUp.Click += new System.EventHandler(this.playlistMenuItemMoveUp_Click);
			// 
			// playlistMenuItemMoveDown
			// 
			this.playlistMenuItemMoveDown.Name = "playlistMenuItemMoveDown";
			this.playlistMenuItemMoveDown.Size = new System.Drawing.Size(186, 22);
			this.playlistMenuItemMoveDown.Text = "Move Down";
			this.playlistMenuItemMoveDown.Click += new System.EventHandler(this.playlistMenuItemMoveDown_Click);
			// 
			// playlistMenuItemMoveTop
			// 
			this.playlistMenuItemMoveTop.Name = "playlistMenuItemMoveTop";
			this.playlistMenuItemMoveTop.Size = new System.Drawing.Size(186, 22);
			this.playlistMenuItemMoveTop.Text = "Move to Top";
			this.playlistMenuItemMoveTop.Click += new System.EventHandler(this.playlistMenuItemMoveTop_Click);
			// 
			// playlistMenuItemMoveBottom
			// 
			this.playlistMenuItemMoveBottom.Name = "playlistMenuItemMoveBottom";
			this.playlistMenuItemMoveBottom.Size = new System.Drawing.Size(186, 22);
			this.playlistMenuItemMoveBottom.Text = "Move to Bottom";
			this.playlistMenuItemMoveBottom.Click += new System.EventHandler(this.playlistMenuItemMoveBottom_Click);
			// 
			// playlistMenuSeparator1
			// 
			this.playlistMenuSeparator1.Name = "playlistMenuSeparator1";
			this.playlistMenuSeparator1.Size = new System.Drawing.Size(183, 6);
			// 
			// playlistMenuItemFileInfo
			// 
			this.playlistMenuItemFileInfo.Name = "playlistMenuItemFileInfo";
			this.playlistMenuItemFileInfo.Size = new System.Drawing.Size(186, 22);
			this.playlistMenuItemFileInfo.Text = "File Info";
			// 
			// playlistMenuItemFileLocation
			// 
			this.playlistMenuItemFileLocation.Name = "playlistMenuItemFileLocation";
			this.playlistMenuItemFileLocation.Size = new System.Drawing.Size(186, 22);
			this.playlistMenuItemFileLocation.Text = "Open File Location";
			// 
			// playlistMenuSeparator2
			// 
			this.playlistMenuSeparator2.Name = "playlistMenuSeparator2";
			this.playlistMenuSeparator2.Size = new System.Drawing.Size(183, 6);
			// 
			// playlistMenuItemAddToPlaylist
			// 
			this.playlistMenuItemAddToPlaylist.Name = "playlistMenuItemAddToPlaylist";
			this.playlistMenuItemAddToPlaylist.Size = new System.Drawing.Size(186, 22);
			this.playlistMenuItemAddToPlaylist.Text = "Add to Playlist";
			// 
			// playlistMenuItemRemoveFrmPlaylist
			// 
			this.playlistMenuItemRemoveFrmPlaylist.Name = "playlistMenuItemRemoveFrmPlaylist";
			this.playlistMenuItemRemoveFrmPlaylist.Size = new System.Drawing.Size(186, 22);
			this.playlistMenuItemRemoveFrmPlaylist.Text = "Remove from Playlist";
			// 
			// playlistMenuItemDeleteFromDisk
			// 
			this.playlistMenuItemDeleteFromDisk.Name = "playlistMenuItemDeleteFromDisk";
			this.playlistMenuItemDeleteFromDisk.Size = new System.Drawing.Size(186, 22);
			this.playlistMenuItemDeleteFromDisk.Text = "Delete File";
			// 
			// labelMainTitle
			// 
			this.labelMainTitle.BackColor = System.Drawing.SystemColors.Control;
			this.labelMainTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.labelMainTitle.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.labelMainTitle.ForeColor = System.Drawing.SystemColors.WindowText;
			this.labelMainTitle.Location = new System.Drawing.Point(223, 43);
			this.labelMainTitle.Name = "labelMainTitle";
			this.labelMainTitle.ReadOnly = true;
			this.labelMainTitle.Size = new System.Drawing.Size(283, 33);
			this.labelMainTitle.TabIndex = 6;
			this.labelMainTitle.Text = "あんたほんとにさいていだね、ちんぽ切りますください";
			this.labelMainTitle.WordWrap = false;
			// 
			// btnExitApp
			// 
			this.btnExitApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnExitApp.Location = new System.Drawing.Point(918, 0);
			this.btnExitApp.Name = "btnExitApp";
			this.btnExitApp.Size = new System.Drawing.Size(37, 24);
			this.btnExitApp.TabIndex = 8;
			this.btnExitApp.Text = "Exit";
			this.btnExitApp.UseVisualStyleBackColor = true;
			// 
			// btnPrev
			// 
			this.btnPrev.Location = new System.Drawing.Point(9, 260);
			this.btnPrev.Name = "btnPrev";
			this.btnPrev.Size = new System.Drawing.Size(40, 42);
			this.btnPrev.TabIndex = 9;
			this.btnPrev.Text = "<";
			this.btnPrev.UseVisualStyleBackColor = true;
			// 
			// btnNext
			// 
			this.btnNext.Location = new System.Drawing.Point(53, 260);
			this.btnNext.Name = "btnNext";
			this.btnNext.Size = new System.Drawing.Size(40, 42);
			this.btnNext.TabIndex = 10;
			this.btnNext.Text = ">";
			this.btnNext.UseVisualStyleBackColor = true;
			// 
			// menuStrip
			// 
			this.menuStrip.BackColor = System.Drawing.SystemColors.MenuBar;
			this.menuStrip.ContextMenuStrip = this.menuContextMenuStrip;
			this.menuStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuStripMenuLabel,
            this.syncToolStripMenuItem});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Padding = new System.Windows.Forms.Padding(0, 3, 0, 2);
			this.menuStrip.Size = new System.Drawing.Size(96, 24);
			this.menuStrip.TabIndex = 12;
			this.menuStrip.Text = "menuStrip";
			// 
			// menuContextMenuStrip
			// 
			this.menuContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItemImportPlaylist,
            this.menuToolStripMenuItemExportPlaylist,
            this.toolStripSeparator1,
            this.menuToolStripMenuItemOptions,
            this.menuToolStripMenuItemExit});
			this.menuContextMenuStrip.Name = "trayContextMenuStrip";
			this.menuContextMenuStrip.OwnerItem = this.menuStripMenuLabel;
			this.menuContextMenuStrip.Size = new System.Drawing.Size(151, 98);
			// 
			// menuToolStripMenuItemImportPlaylist
			// 
			this.menuToolStripMenuItemImportPlaylist.Name = "menuToolStripMenuItemImportPlaylist";
			this.menuToolStripMenuItemImportPlaylist.Size = new System.Drawing.Size(150, 22);
			this.menuToolStripMenuItemImportPlaylist.Text = "Import Playlist";
			// 
			// menuToolStripMenuItemExportPlaylist
			// 
			this.menuToolStripMenuItemExportPlaylist.Name = "menuToolStripMenuItemExportPlaylist";
			this.menuToolStripMenuItemExportPlaylist.Size = new System.Drawing.Size(150, 22);
			this.menuToolStripMenuItemExportPlaylist.Text = "Export Playlist";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(147, 6);
			// 
			// menuToolStripMenuItemOptions
			// 
			this.menuToolStripMenuItemOptions.Name = "menuToolStripMenuItemOptions";
			this.menuToolStripMenuItemOptions.Size = new System.Drawing.Size(150, 22);
			this.menuToolStripMenuItemOptions.Text = "Options";
			// 
			// menuToolStripMenuItemExit
			// 
			this.menuToolStripMenuItemExit.Name = "menuToolStripMenuItemExit";
			this.menuToolStripMenuItemExit.Size = new System.Drawing.Size(150, 22);
			this.menuToolStripMenuItemExit.Text = "Exit";
			// 
			// menuStripMenuLabel
			// 
			this.menuStripMenuLabel.BackColor = System.Drawing.SystemColors.MenuBar;
			this.menuStripMenuLabel.DropDown = this.menuContextMenuStrip;
			this.menuStripMenuLabel.Name = "menuStripMenuLabel";
			this.menuStripMenuLabel.Size = new System.Drawing.Size(50, 19);
			this.menuStripMenuLabel.Text = "Menu";
			// 
			// syncToolStripMenuItem
			// 
			this.syncToolStripMenuItem.BackColor = System.Drawing.SystemColors.MenuBar;
			this.syncToolStripMenuItem.Name = "syncToolStripMenuItem";
			this.syncToolStripMenuItem.Size = new System.Drawing.Size(44, 19);
			this.syncToolStripMenuItem.Text = "Sync";
			// 
			// trayIcon
			// 
			this.trayIcon.BalloonTipText = "Arient Music Player is running!";
			this.trayIcon.BalloonTipTitle = "Arient Music Player";
			this.trayIcon.ContextMenuStrip = this.trayContextMenuStrip;
			this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
			this.trayIcon.Text = "Notify Icon!";
			this.trayIcon.Visible = true;
			// 
			// trayContextMenuStrip
			// 
			this.trayContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trayiconToolStripMenuItemExit});
			this.trayContextMenuStrip.Name = "trayContextMenuStrip";
			this.trayContextMenuStrip.Size = new System.Drawing.Size(94, 26);
			// 
			// trayiconToolStripMenuItemExit
			// 
			this.trayiconToolStripMenuItemExit.Name = "trayiconToolStripMenuItemExit";
			this.trayiconToolStripMenuItemExit.Size = new System.Drawing.Size(93, 22);
			this.trayiconToolStripMenuItemExit.Text = "Exit";
			// 
			// btnMinimize
			// 
			this.btnMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnMinimize.Location = new System.Drawing.Point(866, 0);
			this.btnMinimize.Name = "btnMinimize";
			this.btnMinimize.Size = new System.Drawing.Size(56, 24);
			this.btnMinimize.TabIndex = 13;
			this.btnMinimize.Text = "Minimize";
			this.btnMinimize.UseVisualStyleBackColor = true;
			// 
			// topPanel
			// 
			this.topPanel.BackColor = System.Drawing.SystemColors.MenuBar;
			this.topPanel.Controls.Add(this.menuStrip);
			this.topPanel.Controls.Add(this.btnMinimize);
			this.topPanel.Controls.Add(this.btnExitApp);
			this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.topPanel.Location = new System.Drawing.Point(0, 0);
			this.topPanel.Margin = new System.Windows.Forms.Padding(0);
			this.topPanel.Name = "topPanel";
			this.topPanel.Size = new System.Drawing.Size(955, 24);
			this.topPanel.TabIndex = 14;
			// 
			// musicProgressTrackBar
			// 
			this.musicProgressTrackBar.Location = new System.Drawing.Point(8, 234);
			this.musicProgressTrackBar.Maximum = 100;
			this.musicProgressTrackBar.Name = "musicProgressTrackBar";
			this.musicProgressTrackBar.Size = new System.Drawing.Size(428, 45);
			this.musicProgressTrackBar.TabIndex = 20;
			this.musicProgressTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(12, 22);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(201, 203);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 21;
			this.pictureBox1.TabStop = false;
			// 
			// labelMainArtist
			// 
			this.labelMainArtist.BackColor = System.Drawing.SystemColors.Control;
			this.labelMainArtist.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.labelMainArtist.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.labelMainArtist.ForeColor = System.Drawing.SystemColors.WindowText;
			this.labelMainArtist.Location = new System.Drawing.Point(228, 82);
			this.labelMainArtist.Name = "labelMainArtist";
			this.labelMainArtist.ReadOnly = true;
			this.labelMainArtist.Size = new System.Drawing.Size(283, 23);
			this.labelMainArtist.TabIndex = 24;
			this.labelMainArtist.Text = "Becky ft Lemmesmash";
			this.labelMainArtist.WordWrap = false;
			// 
			// labelMainAlbum
			// 
			this.labelMainAlbum.BackColor = System.Drawing.SystemColors.Control;
			this.labelMainAlbum.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.labelMainAlbum.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.labelMainAlbum.ForeColor = System.Drawing.SystemColors.WindowText;
			this.labelMainAlbum.Location = new System.Drawing.Point(228, 108);
			this.labelMainAlbum.Name = "labelMainAlbum";
			this.labelMainAlbum.ReadOnly = true;
			this.labelMainAlbum.Size = new System.Drawing.Size(283, 20);
			this.labelMainAlbum.TabIndex = 25;
			this.labelMainAlbum.Text = "Where all my dreams go to die";
			this.labelMainAlbum.WordWrap = false;
			// 
			// labelMainExtraInfo
			// 
			this.labelMainExtraInfo.BackColor = System.Drawing.SystemColors.Control;
			this.labelMainExtraInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.labelMainExtraInfo.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.labelMainExtraInfo.ForeColor = System.Drawing.SystemColors.WindowText;
			this.labelMainExtraInfo.Location = new System.Drawing.Point(225, 135);
			this.labelMainExtraInfo.Multiline = true;
			this.labelMainExtraInfo.Name = "labelMainExtraInfo";
			this.labelMainExtraInfo.ReadOnly = true;
			this.labelMainExtraInfo.Size = new System.Drawing.Size(206, 29);
			this.labelMainExtraInfo.TabIndex = 26;
			this.labelMainExtraInfo.Text = "MP3, 44100hz, 320kbps, 123bpm, 16 bit, 1440p, 144hz, HDMI, PPOG";
			// 
			// audioLevelTrackBar
			// 
			this.audioLevelTrackBar.Location = new System.Drawing.Point(296, 276);
			this.audioLevelTrackBar.Maximum = 100;
			this.audioLevelTrackBar.Name = "audioLevelTrackBar";
			this.audioLevelTrackBar.Size = new System.Drawing.Size(138, 45);
			this.audioLevelTrackBar.TabIndex = 27;
			this.audioLevelTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
			// 
			// checkBoxShuffle
			// 
			this.checkBoxShuffle.AutoSize = true;
			this.checkBoxShuffle.Location = new System.Drawing.Point(1, 5);
			this.checkBoxShuffle.Name = "checkBoxShuffle";
			this.checkBoxShuffle.Size = new System.Drawing.Size(59, 17);
			this.checkBoxShuffle.TabIndex = 28;
			this.checkBoxShuffle.Text = "Shuffle";
			this.checkBoxShuffle.UseVisualStyleBackColor = true;
			// 
			// checkBoxRepeat
			// 
			this.checkBoxRepeat.AutoSize = true;
			this.checkBoxRepeat.Location = new System.Drawing.Point(63, 5);
			this.checkBoxRepeat.Name = "checkBoxRepeat";
			this.checkBoxRepeat.Size = new System.Drawing.Size(61, 17);
			this.checkBoxRepeat.TabIndex = 29;
			this.checkBoxRepeat.Text = "Repeat";
			this.checkBoxRepeat.UseVisualStyleBackColor = true;
			// 
			// labelPlaylistSelection
			// 
			this.labelPlaylistSelection.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.labelPlaylistSelection.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.labelPlaylistSelection.Location = new System.Drawing.Point(86, 23);
			this.labelPlaylistSelection.Multiline = true;
			this.labelPlaylistSelection.Name = "labelPlaylistSelection";
			this.labelPlaylistSelection.ReadOnly = true;
			this.labelPlaylistSelection.Size = new System.Drawing.Size(311, 54);
			this.labelPlaylistSelection.TabIndex = 23;
			this.labelPlaylistSelection.Text = "None";
			this.labelPlaylistSelection.WordWrap = false;
			// 
			// pictureBox2
			// 
			this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
			this.pictureBox2.Location = new System.Drawing.Point(3, 3);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(75, 75);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox2.TabIndex = 22;
			this.pictureBox2.TabStop = false;
			// 
			// btnRenamePlaylist
			// 
			this.btnRenamePlaylist.Location = new System.Drawing.Point(304, 82);
			this.btnRenamePlaylist.Name = "btnRenamePlaylist";
			this.btnRenamePlaylist.Size = new System.Drawing.Size(56, 21);
			this.btnRenamePlaylist.TabIndex = 18;
			this.btnRenamePlaylist.Text = "Rename";
			this.btnRenamePlaylist.UseVisualStyleBackColor = true;
			// 
			// btnDeletePlaylist
			// 
			this.btnDeletePlaylist.Location = new System.Drawing.Point(257, 82);
			this.btnDeletePlaylist.Name = "btnDeletePlaylist";
			this.btnDeletePlaylist.Size = new System.Drawing.Size(46, 21);
			this.btnDeletePlaylist.TabIndex = 17;
			this.btnDeletePlaylist.Text = "Delete";
			this.btnDeletePlaylist.UseVisualStyleBackColor = true;
			// 
			// btnCreateNewPlaylist
			// 
			this.btnCreateNewPlaylist.Location = new System.Drawing.Point(211, 82);
			this.btnCreateNewPlaylist.Name = "btnCreateNewPlaylist";
			this.btnCreateNewPlaylist.Size = new System.Drawing.Size(46, 21);
			this.btnCreateNewPlaylist.TabIndex = 16;
			this.btnCreateNewPlaylist.Text = "New";
			this.btnCreateNewPlaylist.UseVisualStyleBackColor = true;
			// 
			// playlistSelectComboBox
			// 
			this.playlistSelectComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.playlistSelectComboBox.FormattingEnabled = true;
			this.playlistSelectComboBox.Location = new System.Drawing.Point(3, 82);
			this.playlistSelectComboBox.Name = "playlistSelectComboBox";
			this.playlistSelectComboBox.Size = new System.Drawing.Size(206, 21);
			this.playlistSelectComboBox.TabIndex = 15;
			// 
			// panel3
			// 
			this.panel3.AutoSize = true;
			this.panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel3.Location = new System.Drawing.Point(0, 115);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(420, 0);
			this.panel3.TabIndex = 31;
			// 
			// panel1
			// 
			this.panel1.ContextMenuStrip = this.playlistContextMenuStrip;
			this.panel1.Controls.Add(this.textBox2);
			this.panel1.Controls.Add(this.btnRenamePlaylist);
			this.panel1.Controls.Add(this.btnDeletePlaylist);
			this.panel1.Controls.Add(this.labelPlaylistSelection);
			this.panel1.Controls.Add(this.btnCreateNewPlaylist);
			this.panel1.Controls.Add(this.pictureBox2);
			this.panel1.Controls.Add(this.panel3);
			this.panel1.Controls.Add(this.playlistSelectComboBox);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(1, 2);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(420, 115);
			this.panel1.TabIndex = 30;
			// 
			// textBox2
			// 
			this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox2.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBox2.Location = new System.Drawing.Point(85, 9);
			this.textBox2.Name = "textBox2";
			this.textBox2.ReadOnly = true;
			this.textBox2.Size = new System.Drawing.Size(111, 13);
			this.textBox2.TabIndex = 32;
			this.textBox2.Text = "Current Selection:";
			// 
			// labelVolumeLevel
			// 
			this.labelVolumeLevel.BackColor = System.Drawing.SystemColors.Control;
			this.labelVolumeLevel.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.labelVolumeLevel.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.labelVolumeLevel.ForeColor = System.Drawing.SystemColors.WindowText;
			this.labelVolumeLevel.Location = new System.Drawing.Point(303, 262);
			this.labelVolumeLevel.Name = "labelVolumeLevel";
			this.labelVolumeLevel.ReadOnly = true;
			this.labelVolumeLevel.Size = new System.Drawing.Size(131, 15);
			this.labelVolumeLevel.TabIndex = 31;
			this.labelVolumeLevel.Text = "Volume: 100%";
			this.labelVolumeLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.labelVolumeLevel.WordWrap = false;
			// 
			// labelPlaybackTime
			// 
			this.labelPlaybackTime.BackColor = System.Drawing.SystemColors.Control;
			this.labelPlaybackTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.labelPlaybackTime.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.labelPlaybackTime.ForeColor = System.Drawing.SystemColors.WindowText;
			this.labelPlaybackTime.Location = new System.Drawing.Point(277, 217);
			this.labelPlaybackTime.Name = "labelPlaybackTime";
			this.labelPlaybackTime.ReadOnly = true;
			this.labelPlaybackTime.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.labelPlaybackTime.Size = new System.Drawing.Size(150, 17);
			this.labelPlaybackTime.TabIndex = 32;
			this.labelPlaybackTime.Text = "Elapsed: 1:43/3:46";
			this.labelPlaybackTime.WordWrap = false;
			// 
			// PlaylistPanel
			// 
			this.PlaylistPanel.Controls.Add(this.playlistListView);
			this.PlaylistPanel.Controls.Add(this.panel4);
			this.PlaylistPanel.Controls.Add(this.panel1);
			this.PlaylistPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PlaylistPanel.Location = new System.Drawing.Point(524, 24);
			this.PlaylistPanel.Name = "PlaylistPanel";
			this.PlaylistPanel.Padding = new System.Windows.Forms.Padding(1, 2, 10, 2);
			this.PlaylistPanel.Size = new System.Drawing.Size(431, 475);
			this.PlaylistPanel.TabIndex = 33;
			// 
			// playlistListView
			// 
			this.playlistListView.AllowDrop = true;
			this.playlistListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.entryID,
            entryTitle,
            this.entryAlbum,
            this.entryArtist,
            this.entrySongLength,
            this.entryFileFormat});
			this.playlistListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.playlistListView.FullRowSelect = true;
			this.playlistListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.playlistListView.HideSelection = false;
			this.playlistListView.Location = new System.Drawing.Point(1, 117);
			this.playlistListView.MinimumSize = new System.Drawing.Size(350, 100);
			this.playlistListView.Name = "playlistListView";
			this.playlistListView.ShowGroups = false;
			this.playlistListView.Size = new System.Drawing.Size(420, 331);
			this.playlistListView.TabIndex = 5;
			this.playlistListView.TabStop = false;
			this.playlistListView.UseCompatibleStateImageBehavior = false;
			this.playlistListView.View = System.Windows.Forms.View.Details;
			// 
			// entryID
			// 
			this.entryID.Text = "Track";
			this.entryID.Width = 42;
			// 
			// entryAlbum
			// 
			this.entryAlbum.Text = "Album";
			this.entryAlbum.Width = 71;
			// 
			// entryArtist
			// 
			this.entryArtist.Text = "Artists";
			this.entryArtist.Width = 69;
			// 
			// entrySongLength
			// 
			this.entrySongLength.Text = "Length";
			this.entrySongLength.Width = 48;
			// 
			// entryFileFormat
			// 
			this.entryFileFormat.Text = "Format";
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.checkBoxRepeat);
			this.panel4.Controls.Add(this.checkBoxShuffle);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel4.Location = new System.Drawing.Point(1, 448);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(420, 25);
			this.panel4.TabIndex = 31;
			// 
			// PlayerPanel
			// 
			this.PlayerPanel.Controls.Add(this.labelPlaybackTime);
			this.PlayerPanel.Controls.Add(this.labelVolumeLevel);
			this.PlayerPanel.Controls.Add(this.labelMainExtraInfo);
			this.PlayerPanel.Controls.Add(this.labelMainAlbum);
			this.PlayerPanel.Controls.Add(this.labelMainArtist);
			this.PlayerPanel.Controls.Add(this.pictureBox1);
			this.PlayerPanel.Controls.Add(this.btnNext);
			this.PlayerPanel.Controls.Add(this.btnPrev);
			this.PlayerPanel.Controls.Add(this.labelMainTitle);
			this.PlayerPanel.Controls.Add(this.btnStop);
			this.PlayerPanel.Controls.Add(this.btnPause);
			this.PlayerPanel.Controls.Add(this.btnPlay);
			this.PlayerPanel.Controls.Add(this.musicProgressTrackBar);
			this.PlayerPanel.Controls.Add(this.audioLevelTrackBar);
			this.PlayerPanel.Dock = System.Windows.Forms.DockStyle.Left;
			this.PlayerPanel.Location = new System.Drawing.Point(0, 24);
			this.PlayerPanel.Name = "PlayerPanel";
			this.PlayerPanel.Size = new System.Drawing.Size(524, 475);
			this.PlayerPanel.TabIndex = 34;
			// 
			// ArientWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(955, 499);
			this.ControlBox = false;
			this.Controls.Add(this.PlaylistPanel);
			this.Controls.Add(this.PlayerPanel);
			this.Controls.Add(this.topPanel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip;
			this.MinimumSize = new System.Drawing.Size(960, 365);
			this.Name = "ArientWindow";
			this.playlistContextMenuStrip.ResumeLayout(false);
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.menuContextMenuStrip.ResumeLayout(false);
			this.trayContextMenuStrip.ResumeLayout(false);
			this.topPanel.ResumeLayout(false);
			this.topPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.musicProgressTrackBar)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.audioLevelTrackBar)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.PlaylistPanel.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			this.PlayerPanel.ResumeLayout(false);
			this.PlayerPanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnPlay;
		private System.Windows.Forms.Button btnPause;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.TextBox labelMainTitle;
		private System.Windows.Forms.Button btnExitApp;
		private System.Windows.Forms.Button btnPrev;
		private System.Windows.Forms.Button btnNext;
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.NotifyIcon trayIcon;
		private System.Windows.Forms.Button btnMinimize;
		private System.Windows.Forms.Panel topPanel;
		private System.Windows.Forms.ContextMenuStrip trayContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem trayiconToolStripMenuItemExit;
		private System.Windows.Forms.ContextMenuStrip playlistContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem playlistMenuItemPlayItem;
		private System.Windows.Forms.ToolStripMenuItem playlistMenuItemAddToPlaylist;
		private System.Windows.Forms.ToolStripSeparator playlistMenuSeparator1;
		private System.Windows.Forms.ToolStripMenuItem playlistMenuItemFileInfo;
		private System.Windows.Forms.ToolStripMenuItem playlistMenuItemFileLocation;
		private System.Windows.Forms.ToolStripSeparator playlistMenuSeparator2;
		private System.Windows.Forms.ToolStripMenuItem playlistMenuItemRemoveFrmPlaylist;
		private System.Windows.Forms.ToolStripMenuItem playlistMenuItemDeleteFromDisk;
		private System.Windows.Forms.ToolStripMenuItem menuStripMenuLabel;
		private System.Windows.Forms.ContextMenuStrip menuContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItemOptions;
		private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItemExit;
		private System.Windows.Forms.ToolStripSeparator playlistMenuSeparator3;
		private System.Windows.Forms.ToolStripMenuItem playlistMenuItemMoveUp;
		private System.Windows.Forms.ToolStripMenuItem playlistMenuItemMoveDown;
		private System.Windows.Forms.ToolStripMenuItem playlistMenuItemMoveTop;
		private System.Windows.Forms.ToolStripMenuItem playlistMenuItemMoveBottom;
		private System.Windows.Forms.TrackBar musicProgressTrackBar;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.TextBox labelMainArtist;
		private System.Windows.Forms.TextBox labelMainAlbum;
		private System.Windows.Forms.TextBox labelMainExtraInfo;
		private System.Windows.Forms.TrackBar audioLevelTrackBar;
		private System.Windows.Forms.CheckBox checkBoxShuffle;
		private System.Windows.Forms.CheckBox checkBoxRepeat;
		private System.Windows.Forms.TextBox labelPlaylistSelection;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.Button btnRenamePlaylist;
		private System.Windows.Forms.Button btnDeletePlaylist;
		private System.Windows.Forms.Button btnCreateNewPlaylist;
		private System.Windows.Forms.ComboBox playlistSelectComboBox;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TextBox labelVolumeLevel;
		private System.Windows.Forms.ToolStripMenuItem syncToolStripMenuItem;
		private System.Windows.Forms.TextBox labelPlaybackTime;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Panel PlaylistPanel;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Panel PlayerPanel;
		private System.Windows.Forms.ListView playlistListView;
		private System.Windows.Forms.ColumnHeader entryID;
		private System.Windows.Forms.ColumnHeader entryAlbum;
		private System.Windows.Forms.ColumnHeader entryArtist;
		private System.Windows.Forms.ColumnHeader entrySongLength;
		private System.Windows.Forms.ColumnHeader entryFileFormat;
		private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItemImportPlaylist;
		private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItemExportPlaylist;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
	}
}

