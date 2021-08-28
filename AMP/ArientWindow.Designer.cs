
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
			this.playlistListView = new System.Windows.Forms.ListView();
			this.entryID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.entryAlbum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.entryArtist = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.entrySongLength = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.entryFileFormat = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
			this.songName = new System.Windows.Forms.TextBox();
			this.btnExitApp = new System.Windows.Forms.Button();
			this.btnPrev = new System.Windows.Forms.Button();
			this.btnNext = new System.Windows.Forms.Button();
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.menuContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.menuToolStripMenuItemOptions = new System.Windows.Forms.ToolStripMenuItem();
			this.menuToolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStripMenuLabel = new System.Windows.Forms.ToolStripMenuItem();
			this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.trayContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.trayiconToolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
			this.btnMinimize = new System.Windows.Forms.Button();
			this.menuPanel = new System.Windows.Forms.Panel();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.btnCreateNewPlaylist = new System.Windows.Forms.Button();
			this.btnDeletePlaylist = new System.Windows.Forms.Button();
			this.btnImportPlaylist = new System.Windows.Forms.Button();
			this.btnExportPlaylist = new System.Windows.Forms.Button();
			this.trackBar1 = new System.Windows.Forms.TrackBar();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			entryTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.playlistContextMenuStrip.SuspendLayout();
			this.menuStrip.SuspendLayout();
			this.menuContextMenuStrip.SuspendLayout();
			this.trayContextMenuStrip.SuspendLayout();
			this.menuPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// entryTitle
			// 
			entryTitle.Text = "Title";
			entryTitle.Width = 108;
			// 
			// btnPlay
			// 
			this.btnPlay.Location = new System.Drawing.Point(42, 69);
			this.btnPlay.Name = "btnPlay";
			this.btnPlay.Size = new System.Drawing.Size(105, 42);
			this.btnPlay.TabIndex = 0;
			this.btnPlay.Text = "Play/Pause";
			this.btnPlay.UseVisualStyleBackColor = true;
			// 
			// btnPause
			// 
			this.btnPause.Location = new System.Drawing.Point(153, 69);
			this.btnPause.Name = "btnPause";
			this.btnPause.Size = new System.Drawing.Size(111, 42);
			this.btnPause.TabIndex = 3;
			this.btnPause.Text = "Pause";
			this.btnPause.UseVisualStyleBackColor = true;
			// 
			// btnStop
			// 
			this.btnStop.Location = new System.Drawing.Point(270, 69);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(113, 42);
			this.btnStop.TabIndex = 4;
			this.btnStop.Text = "Stop";
			this.btnStop.UseVisualStyleBackColor = true;
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
			this.playlistListView.ContextMenuStrip = this.playlistContextMenuStrip;
			this.playlistListView.FullRowSelect = true;
			this.playlistListView.HideSelection = false;
			this.playlistListView.Location = new System.Drawing.Point(404, 99);
			this.playlistListView.Name = "playlistListView";
			this.playlistListView.ShowGroups = false;
			this.playlistListView.Size = new System.Drawing.Size(397, 335);
			this.playlistListView.TabIndex = 5;
			this.playlistListView.TabStop = false;
			this.playlistListView.UseCompatibleStateImageBehavior = false;
			this.playlistListView.View = System.Windows.Forms.View.Details;
			// 
			// entryID
			// 
			this.entryID.Text = "No.";
			this.entryID.Width = 31;
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
			// songName
			// 
			this.songName.Location = new System.Drawing.Point(42, 307);
			this.songName.Name = "songName";
			this.songName.Size = new System.Drawing.Size(329, 20);
			this.songName.TabIndex = 6;
			// 
			// btnExitApp
			// 
			this.btnExitApp.Location = new System.Drawing.Point(776, 0);
			this.btnExitApp.Name = "btnExitApp";
			this.btnExitApp.Size = new System.Drawing.Size(37, 24);
			this.btnExitApp.TabIndex = 8;
			this.btnExitApp.Text = "Exit";
			this.btnExitApp.UseVisualStyleBackColor = true;
			// 
			// btnPrev
			// 
			this.btnPrev.Location = new System.Drawing.Point(42, 117);
			this.btnPrev.Name = "btnPrev";
			this.btnPrev.Size = new System.Drawing.Size(40, 47);
			this.btnPrev.TabIndex = 9;
			this.btnPrev.Text = "<";
			this.btnPrev.UseVisualStyleBackColor = true;
			// 
			// btnNext
			// 
			this.btnNext.Location = new System.Drawing.Point(107, 117);
			this.btnNext.Name = "btnNext";
			this.btnNext.Size = new System.Drawing.Size(40, 47);
			this.btnNext.TabIndex = 10;
			this.btnNext.Text = ">";
			this.btnNext.UseVisualStyleBackColor = true;
			// 
			// menuStrip
			// 
			this.menuStrip.ContextMenuStrip = this.menuContextMenuStrip;
			this.menuStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuStripMenuLabel});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Padding = new System.Windows.Forms.Padding(0, 3, 0, 2);
			this.menuStrip.Size = new System.Drawing.Size(52, 24);
			this.menuStrip.TabIndex = 12;
			this.menuStrip.Text = "menuStrip";
			// 
			// menuContextMenuStrip
			// 
			this.menuContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItemOptions,
            this.menuToolStripMenuItemExit});
			this.menuContextMenuStrip.Name = "trayContextMenuStrip";
			this.menuContextMenuStrip.OwnerItem = this.menuStripMenuLabel;
			this.menuContextMenuStrip.Size = new System.Drawing.Size(117, 48);
			// 
			// menuToolStripMenuItemOptions
			// 
			this.menuToolStripMenuItemOptions.Name = "menuToolStripMenuItemOptions";
			this.menuToolStripMenuItemOptions.Size = new System.Drawing.Size(116, 22);
			this.menuToolStripMenuItemOptions.Text = "Options";
			// 
			// menuToolStripMenuItemExit
			// 
			this.menuToolStripMenuItemExit.Name = "menuToolStripMenuItemExit";
			this.menuToolStripMenuItemExit.Size = new System.Drawing.Size(116, 22);
			this.menuToolStripMenuItemExit.Text = "Exit";
			// 
			// menuStripMenuLabel
			// 
			this.menuStripMenuLabel.DropDown = this.menuContextMenuStrip;
			this.menuStripMenuLabel.Name = "menuStripMenuLabel";
			this.menuStripMenuLabel.Size = new System.Drawing.Size(50, 19);
			this.menuStripMenuLabel.Text = "Menu";
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
			this.btnMinimize.Location = new System.Drawing.Point(724, 0);
			this.btnMinimize.Name = "btnMinimize";
			this.btnMinimize.Size = new System.Drawing.Size(56, 24);
			this.btnMinimize.TabIndex = 13;
			this.btnMinimize.Text = "Minimize";
			this.btnMinimize.UseVisualStyleBackColor = true;
			// 
			// menuPanel
			// 
			this.menuPanel.BackColor = System.Drawing.SystemColors.MenuBar;
			this.menuPanel.Controls.Add(this.btnMinimize);
			this.menuPanel.Controls.Add(this.btnExitApp);
			this.menuPanel.Location = new System.Drawing.Point(0, 0);
			this.menuPanel.Margin = new System.Windows.Forms.Padding(0);
			this.menuPanel.Name = "menuPanel";
			this.menuPanel.Size = new System.Drawing.Size(813, 24);
			this.menuPanel.TabIndex = 14;
			// 
			// comboBox1
			// 
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(404, 72);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(206, 21);
			this.comboBox1.TabIndex = 15;
			// 
			// btnCreateNewPlaylist
			// 
			this.btnCreateNewPlaylist.Location = new System.Drawing.Point(616, 69);
			this.btnCreateNewPlaylist.Name = "btnCreateNewPlaylist";
			this.btnCreateNewPlaylist.Size = new System.Drawing.Size(44, 24);
			this.btnCreateNewPlaylist.TabIndex = 16;
			this.btnCreateNewPlaylist.Text = "New";
			this.btnCreateNewPlaylist.UseVisualStyleBackColor = true;
			// 
			// btnDeletePlaylist
			// 
			this.btnDeletePlaylist.Location = new System.Drawing.Point(666, 69);
			this.btnDeletePlaylist.Name = "btnDeletePlaylist";
			this.btnDeletePlaylist.Size = new System.Drawing.Size(50, 24);
			this.btnDeletePlaylist.TabIndex = 17;
			this.btnDeletePlaylist.Text = "Delete";
			this.btnDeletePlaylist.UseVisualStyleBackColor = true;
			// 
			// btnImportPlaylist
			// 
			this.btnImportPlaylist.Location = new System.Drawing.Point(707, 69);
			this.btnImportPlaylist.Name = "btnImportPlaylist";
			this.btnImportPlaylist.Size = new System.Drawing.Size(50, 24);
			this.btnImportPlaylist.TabIndex = 18;
			this.btnImportPlaylist.Text = "Import";
			this.btnImportPlaylist.UseVisualStyleBackColor = true;
			// 
			// btnExportPlaylist
			// 
			this.btnExportPlaylist.Location = new System.Drawing.Point(763, 69);
			this.btnExportPlaylist.Name = "btnExportPlaylist";
			this.btnExportPlaylist.Size = new System.Drawing.Size(50, 24);
			this.btnExportPlaylist.TabIndex = 19;
			this.btnExportPlaylist.Text = "Export";
			this.btnExportPlaylist.UseVisualStyleBackColor = true;
			// 
			// trackBar1
			// 
			this.trackBar1.Location = new System.Drawing.Point(171, 119);
			this.trackBar1.Name = "trackBar1";
			this.trackBar1.Size = new System.Drawing.Size(190, 45);
			this.trackBar1.TabIndex = 20;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(185, 170);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(123, 118);
			this.pictureBox1.TabIndex = 21;
			this.pictureBox1.TabStop = false;
			// 
			// ArientWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(813, 487);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.trackBar1);
			this.Controls.Add(this.btnExportPlaylist);
			this.Controls.Add(this.btnImportPlaylist);
			this.Controls.Add(this.btnDeletePlaylist);
			this.Controls.Add(this.btnCreateNewPlaylist);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.menuStrip);
			this.Controls.Add(this.btnNext);
			this.Controls.Add(this.btnPrev);
			this.Controls.Add(this.songName);
			this.Controls.Add(this.playlistListView);
			this.Controls.Add(this.btnStop);
			this.Controls.Add(this.btnPause);
			this.Controls.Add(this.btnPlay);
			this.Controls.Add(this.menuPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip;
			this.Name = "ArientWindow";
			this.Text = "Arient Music Player";
			this.playlistContextMenuStrip.ResumeLayout(false);
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.menuContextMenuStrip.ResumeLayout(false);
			this.trayContextMenuStrip.ResumeLayout(false);
			this.menuPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnPlay;
		private System.Windows.Forms.Button btnPause;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.ListView playlistListView;
		private System.Windows.Forms.TextBox songName;
		private System.Windows.Forms.Button btnExitApp;
		private System.Windows.Forms.Button btnPrev;
		private System.Windows.Forms.Button btnNext;
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.NotifyIcon trayIcon;
		private System.Windows.Forms.Button btnMinimize;
		private System.Windows.Forms.Panel menuPanel;
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
		private System.Windows.Forms.ColumnHeader entryID;
		private System.Windows.Forms.ColumnHeader entryAlbum;
		private System.Windows.Forms.ColumnHeader entryArtist;
		private System.Windows.Forms.ColumnHeader entrySongLength;
		private System.Windows.Forms.ColumnHeader entryFileFormat;
		private System.Windows.Forms.ToolStripSeparator playlistMenuSeparator3;
		private System.Windows.Forms.ToolStripMenuItem playlistMenuItemMoveUp;
		private System.Windows.Forms.ToolStripMenuItem playlistMenuItemMoveDown;
		private System.Windows.Forms.ToolStripMenuItem playlistMenuItemMoveTop;
		private System.Windows.Forms.ToolStripMenuItem playlistMenuItemMoveBottom;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Button btnCreateNewPlaylist;
		private System.Windows.Forms.Button btnDeletePlaylist;
		private System.Windows.Forms.Button btnImportPlaylist;
		private System.Windows.Forms.Button btnExportPlaylist;
		private System.Windows.Forms.TrackBar trackBar1;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}

