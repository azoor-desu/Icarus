using System;
using System.Collections.Generic;
using Un4seen.Bass;

namespace ArientMusicPlayer {
    //Handles processing of files and shit, then passes to ArientWindow.
    //AKA BACKEND.

    public class ArientBackend {

        List<string> internalPlaylist = new List<string>();
        ArientWindow arientWindow;

        //Initialization of Core features.
        public ArientBackend(ArientWindow _arientWindow) {

            arientWindow = _arientWindow;

            // init BASS using the default output device
            if (!Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero)) {
                Logging.Debug("Error During Initialization of BASS: " + Bass.BASS_ErrorGetCode());
            } else {
                Logging.Debug("BASS Initialized!");
            }

            LoadInternalPlaylist();
        }


        #region Playback Controls

        int currentChannel = 0;
        bool isPlaying;

        public void StartPlayback() {

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

        public void PausePlayback() {

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

        public void TogglePlayPause() {

            if (!isPlaying) {
                StartPlayback();
            } else {
                PausePlayback();
            }
        }

        public void StopPlayback() {

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

        public void NextTrack() {
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

        public void PrevTrack() {
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

        int internalPlaylistIndex = 0;

        //Load a list of Files to be used as the Internal Playlist.
        public void LoadInternalPlaylist() {
            FileManager.ImportPlaylist("C:\\WORK\\APP\\ArientMusicPlayer\\AMP\\bin\\Debug\\Local files.m3u8", ref internalPlaylist);
            internalPlaylistIndex = 0;
            arientWindow.UpdatePlaylistWindow(internalPlaylist.ToArray());
        }


        #endregion

        #region Settings Management
        //Load in settings from some savefile.
        public bool settingMinToTray = true;


        #endregion

    }
}
