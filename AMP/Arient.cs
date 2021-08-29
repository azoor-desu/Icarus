﻿using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Un4seen.Bass;

namespace ArientMusicPlayer {
	//Contains the entry point. Not really used lol.
	public static class Arient {

		//references for all component instances.
		public static ArientWindow arientWindow;

		#region Main/Exit

		[STAThread]
		static void Main() {

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			arientWindow = new ArientWindow();
			//arientBackend = new Arient();
			InitializeArientBackend();
			LoadSettings();
			Application.Run(arientWindow);

			//Before program shuts down, force save all playlists.
			SaveAllPlaylists();

			//Free BASS stuff before stopping the app completely.
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

		#region Startup/saving
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
		static void LoadSettings() {

			//if (FileManager.CheckPlaylistExists("Local files")) {
			//	loadedPlaylists[0] = FileManager.LoadPlaylistFromDisk("Local files");
			//} else {
			//	Logger.Debug("Playlist not found, importing from m38u lol");
			//	loadedPlaylists[0] = await Task.Run(() => FileManager.ImportPlaylistM3U8("C:\\WORK\\APP\\ArientMusicPlayer\\AMP\\bin\\Debug\\Local files.m3u8"));
			//	loadedPlaylists[0].currentSongIndex = 0;
			//	FileManager.SavePlaylistToDisk(loadedPlaylists[0]);
			//}
			//arientWindow.LoadPlaylistWindow(0);

			//Load in ALL saved playlists.
			loadedPlaylists = FileManager.LoadAllPlaylistsFromDisk(); //loaded in alphabetical order.

			//If there are no saved playlists, make one default blank playlist.
			if (loadedPlaylists == null) {
				loadedPlaylists = new Playlist[1];
				loadedPlaylists[0] = new Playlist();
				loadedPlaylists[0].songs = new TagInfo[0];
				loadedPlaylists[0].name = "Default";
				FileManager.SavePlaylistToDisk(loadedPlaylists[0],true);
			}

			//load the playlist in the App window.
			arientWindow.LoadPlaylistWindow(0); //todo: replace "0" with "lastusedplaylist"

		}

		//Save all playlists before exit.
		static void SaveAllPlaylists() {
			Logger.Debug("Saving all playlists...");
			foreach (Playlist playlist in loadedPlaylists) {
				FileManager.SavePlaylistToDisk(playlist);
			}
			Logger.Debug("Playlists saved.");
		}

		//Todo: Add option to save app settings too.

		#endregion

		#region Playback Controls

		static int currentChannel = 0; //for use for starting and stopping streams.
		static bool isPlaying; //for use for play/pause toggle

		public static void StartPlayback(int currPlaylistIndex) {

			if (currentActivePlaylist != -1 && loadedPlaylists[currentActivePlaylist].songs.Length > 0) {
				// create a stream channel from a file

				//TODO: Replace BASS_StreamCreateFile with BASS_StreamCreate

				//if something is currently playing/paused. Stop it.
				if (currentChannel != 0) {
					if (!Bass.BASS_ChannelStop(currentChannel))
						Logger.Debug("Error during Stopping playback: " + Bass.BASS_ErrorGetCode());
				}

				//Load a new stream, regardless of play/paused/stopped status.
				currentChannel = Bass.BASS_StreamCreateFile(loadedPlaylists[currentActivePlaylist].songs[currPlaylistIndex].filename, 0, 0, BASSFlag.BASS_DEFAULT);
				Logger.Debug("Loading Music file: " + loadedPlaylists[currentActivePlaylist].songs[currPlaylistIndex].filename);

				if (currentChannel != 0) {
					//Stream successfully loaded. Play the stream.
					if (Bass.BASS_ChannelPlay(currentChannel, true)) {
						isPlaying = true;
						OnChangeTrack(currPlaylistIndex);
						loadedPlaylists[currentActivePlaylist].currentSongIndex = currPlaylistIndex;
						Logger.Debug("Playback Started!");
					} else {
						//If there's an Error playing, Log the error.
						Logger.Error("Error starting Playback: " + Bass.BASS_ErrorGetCode());
					}
				} else {
					Logger.Error("Error During creating channel: " + Bass.BASS_ErrorGetCode());
				}
			} else {
				MessageBox.Show("There are no Playlists loaded.");
			}
		}

		public static void PausePlayback() {

			//do BASS_ChannelPause.
			if (currentActivePlaylist != -1 && currentChannel != 0) {
				if (Bass.BASS_ChannelPause(currentChannel)) {
					Logger.Debug("Channel Paused!");
					isPlaying = false;
				} else {
					Logger.Error("Error during pausing playback: " + Bass.BASS_ErrorGetCode());
				}
			}
		}

		public static void TogglePlayPause() {
			if (currentActivePlaylist != -1) {
				if (!isPlaying) {
					StartPlayback(loadedPlaylists[currentActivePlaylist].currentSongIndex);
				} else {
					PausePlayback();
				}
			}
		}

		public static void StopPlayback() {

			//Do BASS_ChannelStop and BASS_StreamFree
			if (currentActivePlaylist != -1 && currentChannel != 0) {

				if (!Bass.BASS_ChannelStop(currentChannel)) 
					Logger.Debug("Error during Stopping playback: " + Bass.BASS_ErrorGetCode());
				if (!Bass.BASS_StreamFree(currentChannel)) 
					Logger.Debug("Error during Freeing stream: " + Bass.BASS_ErrorGetCode());
			}
			Logger.Debug("Playback Stopped!");
			isPlaying = false;
			currentChannel = 0;
		}

		public static void NextTrack() {

			if (currentActivePlaylist != -1) {
				//Stop playback, change the internalPlaylistIndex,
				//then run StartPlayback()
				StopPlayback();

				//Clamp the max index.
				loadedPlaylists[currentActivePlaylist].currentSongIndex++;
				if (loadedPlaylists[currentActivePlaylist].currentSongIndex > loadedPlaylists[currentActivePlaylist].songs.Length - 1) {
					loadedPlaylists[currentActivePlaylist].currentSongIndex = 0;
				}

				StartPlayback(loadedPlaylists[currentActivePlaylist].currentSongIndex);
			}
		}

		public static void PrevTrack() {
			if (currentActivePlaylist != -1) {
				//Stop playback, change the internalPlaylistIndex,
				//then run StartPlayback()stIndex,
				StopPlayback();

				//Clamp the min index.
				loadedPlaylists[currentActivePlaylist].currentSongIndex--;
				if (loadedPlaylists[currentActivePlaylist].currentSongIndex < 0) {
					loadedPlaylists[currentActivePlaylist].currentSongIndex = loadedPlaylists[currentActivePlaylist].songs.Length - 1;
				}

				StartPlayback(loadedPlaylists[currentActivePlaylist].currentSongIndex);
			}
		}

		public static void OnChangeTrack(int newTrackIndex) {
			arientWindow.OnChangeTrackPlaylist(newTrackIndex);
			//do update for pictures and labels too
		}

		#endregion

		#region Playlist Controls

		public class Playlist {

			#region Constructors
			public Playlist()//WARNING: Need to manually set songs array length.
			{

			}

			public Playlist(int length) {
				songs = new TagInfo[length]; //set the length
			}
			#endregion

			public string filename = ""; //not saved to disk. only assigned when loaded in.
											  //Used when renaming or deleting playlist.

			public string name;
			public int currentSongIndex = -1; //default. Indicates there's no songs.
			public double currentSongPos = 0;
			public bool shuffle = false;
			public bool repeatPlaylist = true;
			public TagInfo[] songs;

			//public void AddSong(string path) {
			//	//Create new array with length +1 and copy all old data to it.
			//	TagInfo[] newArray = new TagInfo[songs.Length + 1];
			//	for (int i = 0; i < songs.Length; i++) {
			//		newArray[i] = songs[i];
			//	}
			//	//Add new song to end of array.
			//	newArray[newArray.Length - 1] = FileManager.GetTag(path);
			//	songs = newArray;
			//}
		}

		public struct TagInfo {
			public string filename { get; set; }
			public string title { get; set; }
			public string artist { get; set; }
			public string album { get; set; }
			public string albumartist { get; set; }
			public string year { get; set; }
			public double duration { get; set; }
			public string genre { get; set; }
			public string track { get; set; }
			public string disc { get; set; }
			public int bitrate { get; set; }
			public int frequency { get; set; }
			public string format { get; set; }
			public long size { get; set; }
			public string mood { get; set; }
			public string rating { get; set; }
		}

		#endregion

		#region Settings Management
		//Load in settings from some savefile.
		public static bool settingMinToTray = true;
		public static Playlist[] loadedPlaylists = new Playlist[1];
		public static int currentActivePlaylist = -1; //will only swap when user physically
													  //changes current view and a LoadPlaylistWindow
													  //is called
		#endregion


	}
}
