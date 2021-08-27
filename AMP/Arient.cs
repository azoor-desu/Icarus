using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using Un4seen.Bass;

namespace ArientMusicPlayer {
    //Contains the entry point. Not really used lol.
    public static class Arient {

        //references for all component instances.
        public static ArientWindow arientWindow;
        //public static Arient arientBackend; //this object

        //Entry Point. Loads: ArientWindow Components > ArientBackend.Initialize > ArientBackend.LoadSettings
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
        }

        //Is only here for OnProcessExit to reference.
        public static void ExitApplication() {
            Logging.Debug("Program Exiting, freeing memory!");
            // free BASS
            if (Bass.BASS_Free()) {
                Logging.Debug("BASS released from memory!");
            } else {
                Logging.Debug("Error freeing BASS: " + Bass.BASS_ErrorGetCode());
            }
            Logging.Debug("Program Exited.");

            Application.Exit();
        }

        //OnExit. Hopefully it works when user does EndTask lol.
        static void OnProcessExit(object sender, EventArgs e) {
            ExitApplication();
        }

        #endregion


        //====================START OF BACKEND FEATURES====================

        public static List<string> internalPlaylist = new List<string>();

        #region Startup
        //Initialization BASS.
        static void InitializeArientBackend() {

            // init BASS using the default output device
            if (!Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero)) {
                Logging.Debug("Error During Initialization of BASS: " + Bass.BASS_ErrorGetCode());
            } else {
                Logging.Debug("BASS Initialized!");
            }
        }

        //Loading of saved settings + TESTING stuff.
        static void LoadSettings() {
            LoadInternalPlaylist();
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
                Logging.Debug("Loading Music file, doing BASS_StreamCreateFile.");
                currentChannel = Bass.BASS_StreamCreateFile(internalPlaylist[internalPlaylistIndex], 0, 0, BASSFlag.BASS_DEFAULT);
            }

            if (currentChannel == 0) {
                //If after creating a channel and channel still 0, log an error.
                Logging.Error("Error During creating channel: " + Bass.BASS_ErrorGetCode());
            } else {
                //Stream successfully created, play the audio!
                if (Bass.BASS_ChannelPlay(currentChannel, false)) {
                    isPlaying = true;
                    Logging.Debug("Playback Started!");
                } else {
                    //If there's an Error playing, Log the error.
                    Logging.Error("Error starting Playback: " + Bass.BASS_ErrorGetCode());
                }
            }
        }

        public static void PausePlayback() {

            //do BASS_ChannelPause.
            if (currentChannel != 0) {
                if (Bass.BASS_ChannelPause(currentChannel)) {
                    Logging.Debug("Channel Paused!");
                    isPlaying = false;
                } else {
                    Logging.Error("Error during pausing playback: " + Bass.BASS_ErrorGetCode());
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
                    Logging.Debug("Error during Stopping playback: " + Bass.BASS_ErrorGetCode());
                }
                if (!Bass.BASS_StreamFree(currentChannel)) {
                    Logging.Debug("Error during Freeing stream: " + Bass.BASS_ErrorGetCode());
                }
            }
            Logging.Debug("Playback Stopped!");
            isPlaying = false;
            currentChannel = 0;
        }

        public static void NextTrack() {
            //Do BASS_ChannelPause and BASS_StreamFree,
            //change the internalPlaylistIndex,
            //then run StartPlayback()
            if (currentChannel != 0) {

                if (!Bass.BASS_ChannelStop(currentChannel)) {
                    Logging.Debug("Error during Stopping playback: " + Bass.BASS_ErrorGetCode());
                    return;
                }
                if (!Bass.BASS_StreamFree(currentChannel)) {
                    Logging.Debug("Error during Freeing stream: " + Bass.BASS_ErrorGetCode());
                    return;
                }
            }

            //Clamp the max index.
            internalPlaylistIndex++;
            if (internalPlaylistIndex > internalPlaylist.Count - 1) {
                internalPlaylistIndex = 0;
            }

            currentChannel = 0;
            StartPlayback();
        }

        public static void PrevTrack() {
            //Do BASS_ChannelPause and BASS_StreamFree,
            //change the internalPlaylistIndex,
            //then run StartPlayback()
            if (currentChannel != 0) {

                if (!Bass.BASS_ChannelStop(currentChannel)) {
                    Logging.Debug("Error during Stopping playback: " + Bass.BASS_ErrorGetCode());
                    return;
                }
                if (!Bass.BASS_StreamFree(currentChannel)) {
                    Logging.Debug("Error during Freeing stream: " + Bass.BASS_ErrorGetCode());
                    return;
                }
            }

            //Clamp the min index.
            internalPlaylistIndex--;
            if (internalPlaylistIndex < 0) {
                internalPlaylistIndex = internalPlaylist.Count - 1;
            }

            currentChannel = 0;
            StartPlayback();
        }

        #endregion

        #region Playlist Controls

        static int internalPlaylistIndex = 0;

        //Load a list of Files to be used as the Internal Playlist.
        public static void LoadInternalPlaylist() {
            FileManager.ImportPlaylist("C:\\WORK\\APP\\ArientMusicPlayer\\AMP\\bin\\Debug\\Local files.m3u8", ref internalPlaylist);
            internalPlaylistIndex = 0;
            arientWindow.UpdatePlaylistWindow();
        }


        #endregion

        #region Settings Management
        //Load in settings from some savefile.
        public static bool settingMinToTray = true;


        #endregion


    }


    public static class Logging {

        public static readonly DateTime dateTime = DateTime.Now;

        static string currentSession = DateTime.Now.ToString().Replace("/", "-").Replace(":", ".").Replace(" ", "_");

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
