using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Tags;

namespace ArientMusicPlayer {
	//Contains the entry point. Not really used lol.
	public static class Arient {

		//references for all component instances.
		public static ArientWindow arientWindow;

		//Entry Point. Loads: ArientWindow Components > ArientBackend.Initialize > ArientBackend.LoadSettings
		#region Main/Exit

		[STAThread]
		static void Main() {

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			arientWindow = new ArientWindow();
			//arientBackend = new Arient();
			InitializeArientBackend();
#pragma warning disable CS4014 
			LoadSettingsAsync();//Suppressed green squiggly Warning cos i need this behaviour
#pragma warning restore CS4014
			Application.Run(arientWindow);
			UnloadBASS();
		}

		//Free BASS stuff before stopping the app completely.
		static void UnloadBASS() {
			Logger.Debug("Program Exiting, freeing memory!");
			// free BASS
			if (Bass.BASS_Free()) {
				Logger.Debug("BASS released from memory!");
			} else {
				Logger.Debug("Error freeing BASS: " + Bass.BASS_ErrorGetCode());
			}
			Logger.Debug("Program Exited.");
		}

		#endregion


		//====================START OF BACKEND FEATURES====================

		#region Startup
		//Initialization BASS.
		static void InitializeArientBackend() {

			// init BASS using the default output device
			if (!Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero)) {
				Logger.Debug("Error During Initialization of BASS: " + Bass.BASS_ErrorGetCode());
			} else {
				Logger.Debug("BASS Initialized!");
			}
		}

		//Loading of saved settings + TESTING stuff.
		static async Task LoadSettingsAsync() {
			
			if (FileManager.CheckPlaylistExists("Local files")) {
				internalPlaylist = FileManager.ImportPlaylistJSON("Local files");
			} else {
				Logger.Debug("Playlist not found, importing from m38u lol");
				internalPlaylist = await Task.Run(() => FileManager.ImportPlaylistM3U8("C:\\WORK\\APP\\ArientMusicPlayer\\AMP\\bin\\Debug\\Local files.m3u8"));
				internalPlaylist.currentSongIndex = 0;
				FileManager.WritePlaylistToJSON(internalPlaylist);
			}
			arientWindow.UpdatePlaylistWindow();
		}

		#endregion

		#region Playback Controls

		static int currentChannel = 0;
		static bool isPlaying;

		public static void StartPlayback() {

			// create a stream channel from a file

			//BASS_StreamCreateFile streams from Memory and plays the file as-is. Easy but not very noice.
			//BASS_StreamCreate can be used to apply DSP/FX processing to any sample data, by setting DSP/FX on
			//the stream and feeding the data through BASS_ChannelGetData(Int32, IntPtr, Int32) with STREAMPROC_DUMMY

			//TODO: Add a Playlist.
			//TODO: Replace BASS_StreamCreateFile with BASS_StreamCreate

			if (currentChannel == 0) {
				//No channels created yet, create a channel and play it.
				Logger.Debug("Loading Music file, doing BASS_StreamCreateFile.");
				currentChannel = Bass.BASS_StreamCreateFile(internalPlaylist.songs[internalPlaylist.currentSongIndex].filename, 0, 0, BASSFlag.BASS_DEFAULT);
			}

			if (currentChannel == 0) {
				//If after creating a channel and channel still 0, log an error.
				Logger.Error("Error During creating channel: " + Bass.BASS_ErrorGetCode());
			} else {
				//Stream successfully created, play the audio!
				if (Bass.BASS_ChannelPlay(currentChannel, false)) {
					isPlaying = true;
					Logger.Debug("Playback Started!");
				} else {
					//If there's an Error playing, Log the error.
					Logger.Error("Error starting Playback: " + Bass.BASS_ErrorGetCode());
				}
			}
		}

		public static void PausePlayback() {

			//do BASS_ChannelPause.
			if (currentChannel != 0) {
				if (Bass.BASS_ChannelPause(currentChannel)) {
					Logger.Debug("Channel Paused!");
					isPlaying = false;
				} else {
					Logger.Error("Error during pausing playback: " + Bass.BASS_ErrorGetCode());
				}
			}
		}

		public static void TogglePlayPause() {

			if (!isPlaying) {
				StartPlayback();
			} else {
				PausePlayback();
			}
		}

		public static void StopPlayback() {

			//Do BASS_ChannelStop and BASS_StreamFree
			if (currentChannel != 0) {

				if (!Bass.BASS_ChannelStop(currentChannel)) {
					Logger.Debug("Error during Stopping playback: " + Bass.BASS_ErrorGetCode());
				}
				if (!Bass.BASS_StreamFree(currentChannel)) {
					Logger.Debug("Error during Freeing stream: " + Bass.BASS_ErrorGetCode());
				}
			}
			Logger.Debug("Playback Stopped!");
			isPlaying = false;
			currentChannel = 0;
		}

		public static void NextTrack() {
			//Stop playback, change the internalPlaylistIndex,
			//then run StartPlayback()
			StopPlayback();

			//Clamp the max index.
			internalPlaylist.currentSongIndex++;
			if (internalPlaylist.currentSongIndex > internalPlaylist.songs.Length - 1) {
				internalPlaylist.currentSongIndex = 0;
			}

			StartPlayback();
		}

		public static void PrevTrack() {
			//Stop playback, change the internalPlaylistIndex,
			//then run StartPlayback()stIndex,
			StopPlayback();

			//Clamp the min index.
			internalPlaylist.currentSongIndex--;
			if (internalPlaylist.currentSongIndex < 0) {
				internalPlaylist.currentSongIndex = internalPlaylist.songs.Length - 1;
			}

			StartPlayback();
		}

		#endregion

		#region Playlist Controls

		//TODO: Replace TAG_INFO with a custom struct, to save space when saving playlists.

		public class Playlist {

			#region Constructors
			public Playlist() { //WARNING: Need to manually set TAG_INFO.

			}

			public Playlist(int length, bool createEmptyTags = false) {
				SetSongLength(length, createEmptyTags);
			}
			#endregion

			public string name;
			public int currentSongIndex = 0;
			public bool shuffle = false;
			public bool repeatPlaylist = true;
			public bool loaded = false;
			public TAG_INFO[] songs;

			//Helper Methods for managing TAG_INFO songs.

			public void SetSongLength(int length, bool createEmptyTags) {
				songs = new TAG_INFO[length]; //set the length
				if (createEmptyTags) {
					for (int i = 0; i < length; i++) { //loop length and do NEW.
						songs[i] = new TAG_INFO();
					}
				}
			}

			public void AddSong(string path) {
				//Create new array with length +1 and copy all old data to it.
				TAG_INFO[] newArray = new TAG_INFO[songs.Length + 1];
				for (int i = 0; i < songs.Length; i++) {
					newArray[i] = songs[i];
				}
				//Add new song to end of array.
				newArray[newArray.Length - 1] = FileManager.GetTag(path);
				songs = newArray;
			}
		}



		#endregion

		#region Settings Management
		//Load in settings from some savefile.
		public static bool settingMinToTray = true;
		public static Playlist internalPlaylist = new Playlist();
		#endregion


	}

	public static class Logger {

		public static readonly DateTime dateTime = DateTime.Now;

		static readonly string currentSession = DateTime.Now.ToString().Replace("/", "-").Replace(":", ".").Replace(" ", "_");

		public static void Debug(string lines) {
			//Write the string to a file.append mode is enabled so that the log
			//lines get appended to  test.txt than wiping content and writing the log

			using (System.IO.StreamWriter file = new System.IO.StreamWriter(Directory.GetCurrentDirectory() + "\\Log.txt", true)) {
				file.WriteLine("[" + DateTime.Now + "] " + lines);
			}
		}

		public static void Warning(string lines) {
			//Write the string to a file.append mode is enabled so that the log
			//lines get appended to  test.txt than wiping content and writing the log

			using (System.IO.StreamWriter file = new System.IO.StreamWriter(Directory.GetCurrentDirectory() + "\\Log.txt", true)) {
				file.WriteLine("[" + DateTime.Now + "] WARNING:" + lines);
			}
		}

		public static void Error(string lines) {
			//Write the string to a file.append mode is enabled so that the log
			//lines get appended to  test.txt than wiping content and writing the log

			using (System.IO.StreamWriter file = new System.IO.StreamWriter(Directory.GetCurrentDirectory() + "\\Log.txt", true)) {
				file.WriteLine("[" + DateTime.Now + "] ERROR: " + lines);
			}
		}
	}
}
