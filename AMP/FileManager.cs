using System.Collections.Generic;
using System.IO;
using Un4seen.Bass.AddOn.Tags;


namespace ArientMusicPlayer {
	public class FileManager {

		#region External Imports
		//Loads in a Playlist file from disk
		public static Arient.Playlist ImportPlaylistM3U8 (string pathofile) {

			//await Task.Yield(); //for use with an async method.
			//Put this at top of the method to force the whole thing to be an async method.
			//usage is: internalPlaylist = await ImportPlaylist(); << must be in another async method.

			List<Arient.TagInfo> tempTags = new List<Arient.TagInfo>();

			if (File.Exists(pathofile)) {
				string[] lines = File.ReadAllLines(pathofile);
				//BUILT FOR .m38u FILES.

				//Loop thru and ignore all arrays starting with a # or if total characters > 8 (C:/a.mp3 is 8 chars)
				//TODO: Do a regex that validates filepaths.
				//See lowest reply: https://stackoverflow.com/questions/12947405/string-path-validation

				foreach (string line in lines) {
					if (line[0] != '#' && line.Length > 8) {

						//Add each item into tempTags, rejecting those paths where the tag can't be found.
						var tag = ConvertToTagInfo(GetTag(line));
						if (tag.filename != "") {
							tempTags.Add(tag);
						}
					}
				}

				//temp now holds all (hopefully) valid filepaths. Transfer them into a new Playlist Object!

				if (tempTags.Count != 0) { //If ther isn't any valid filepaths, Log an error and return null.

					//Create a new Playlist, loop through the new TagInfo in Playlist, and write the TAGS.

					Arient.Playlist tempPlaylist = new Arient.Playlist(tempTags.Count) {

						//Add the name and array of tags to the name var in tempPlaylist
						name = Path.GetFileName(pathofile).Split('.')[0],
						songs = tempTags.ToArray()
					};

					return tempPlaylist;
				} else {
					Logger.Error("Loading Playlist: No valid songs found in playlist! " + pathofile);
				}

			} else {
				Logger.Error("Loading Playlist: File path of Playlist does not exist: " + pathofile);
			}
			return null;
		}

		#endregion

		#region Internal Saving and Parsing of .bigoof file

		//Save Playlsit class to file on disk
		public static void SavePlaylistToDisk(Arient.Playlist playlist) {
			string towrite = "";

			//Create Header
			towrite += "#.bigoof file version|1.0\n\n";

			//Add playlist name and any other settings.
			towrite += "PlaylistName|" + playlist.name + 
			"\nCurrentSongIndex|" + playlist.currentSongIndex +
			"\nCurrentSongPos|" + playlist.currentSongPos +
			"\nShuffle|" + playlist.shuffle +
			"\nRepeat|" + playlist.repeatPlaylist + 
			"\n\n";

			//Add individual songs, items seperated by |
			foreach (Arient.TagInfo tag in playlist.songs) {
				towrite +=
				tag.filename + "|" +
				tag.title + "|" +
				tag.artist + "|" +
				tag.album + "|" +
				tag.albumartist + "|" +
				tag.year + "|" +
				tag.duration + "|" +
				tag.genre + "|" +
				tag.track + "|" +
				tag.disc + "|" +
				tag.bitrate + "|" +
				tag.frequency + "|" +
				tag.format + "|" +
				tag.size + "|" +
				tag.mood + "|" +
				tag.rating + "\n";
			}

			//Write to file
			string path = Directory.GetCurrentDirectory() + "\\data\\";
			Directory.CreateDirectory(path);
			File.WriteAllText(path + playlist.name + ".bigoof", towrite);

		}

		//Load a Playlist file from disk. Quicker than parsing each music file.
		public static Arient.Playlist LoadPlaylistFromDisk(string playlistname) {

			//Create a Object to return.
			Arient.Playlist toreturn = null;

			//Generate Filepath and check if file exists.
			string filepath = Directory.GetCurrentDirectory() + "\\data\\" + playlistname + ".bigoof";
			if (File.Exists(filepath)) {
				toreturn = new Arient.Playlist(); //override null value to prepare for assignment.

				//load in file to a string
				string[] rawLines = File.ReadAllLines(filepath);

				//Check version of file
				string version = rawLines[0].Split('|')[1];

				//Use different methods to parse depending on version.
				switch (version) {
					case "1.0":

						//==============PARSE STARTS=====================

						//Hardcoded. Indexes 2 to 6 are custom values.
						//Songs start at index 8.

						toreturn.name = playlistname;
						toreturn.currentSongIndex = int.Parse(rawLines[3].Split('|')[1]);
						toreturn.currentSongPos = double.Parse(rawLines[4].Split('|')[1]);
						if (rawLines[5].Split('|')[1] == "True") {
							toreturn.shuffle = true;
						} else {
							toreturn.shuffle = false;
						}

						if (rawLines[6].Split('|')[1] == "True") {
							toreturn.repeatPlaylist = true;
						} else {
							toreturn.repeatPlaylist = false;
						}

						//Determine length of song array and set it.
						toreturn.songs = new Arient.TagInfo[rawLines.Length - 8];


						//Loop through the songs and add to Playlist. Use the toreturn.songs's length
						//the rawLine equavilent is i + 8, because the songs in the file are 8 below the top line
						for (int i = 0; i < toreturn.songs.Length; i++) {
							string[] items = rawLines[i+8].Split('|');
							toreturn.songs[i].filename = items[0];
							toreturn.songs[i].title = items[1];
							toreturn.songs[i].artist = items[2];
							toreturn.songs[i].album = items[3];
							toreturn.songs[i].albumartist = items[4];
							toreturn.songs[i].year = items[5];
							toreturn.songs[i].duration = double.Parse(items[6]);
							toreturn.songs[i].genre = items[7];
							toreturn.songs[i].track = items[8];
							toreturn.songs[i].disc = items[9];
							toreturn.songs[i].bitrate = int.Parse(items[10]);
							toreturn.songs[i].frequency = int.Parse(items[11]);
							toreturn.songs[i].format = items[12];
							toreturn.songs[i].size = long.Parse(items[13]);
							toreturn.songs[i].mood = items[14];
							toreturn.songs[i].rating = items[15];
						}
						break;
				}

			} else {
				Logger.Error("Loading Playlist from disk: Could not find the saved playlist named " + playlistname);
			}
			return toreturn;
		}

		#endregion

		public static bool CheckPlaylistExists(string playlistName) {
			return File.Exists(Directory.GetCurrentDirectory() + "\\data\\" + playlistName + ".bigoof");
		}

		//Loads TAG_INFO from a music file and returns it.
		public static TAG_INFO GetTag(string path) {
			TAG_INFO toreturn = null;

			if (File.Exists(path)) {
				toreturn = BassTags.BASS_TAG_GetFromFile(path);
			} else {
				Logger.Error("Unable to load tag from file: " + path);
			}

			return toreturn;
		}

		public static Arient.TagInfo ConvertToTagInfo(TAG_INFO old) {

			Arient.TagInfo toreturn = new Arient.TagInfo();

			if (old != null) {
				//Map all used stuff from TAG_INFO into toreturn.
				toreturn.filename = old.filename;
				toreturn.title = old.title;
				toreturn.artist = old.artist;
				toreturn.album = old.album;
				toreturn.albumartist = old.albumartist;
				toreturn.year = old.year;
				toreturn.duration = old.duration;
				toreturn.genre = old.genre;
				toreturn.track = old.track;
				toreturn.disc = old.disc;
				toreturn.bitrate = old.bitrate;
				toreturn.frequency = old.channelinfo.freq;
				toreturn.size = new FileInfo(old.filename).Length;
				toreturn.rating = old.rating;

				switch (old.channelinfo.ctype) {
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_MUSIC_MO3:
						toreturn.format = "MO3";
						break;
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_MUSIC_MOD:
						toreturn.format = "MOD";
						break;
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_MUSIC_MTM:
						toreturn.format = "MTM";
						break;
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_MUSIC_S3M:
						toreturn.format = "S3M";
						break;
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_MUSIC_XM:
						toreturn.format = "XM";
						break;
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_STREAM_AAC:
						toreturn.format = "AAC";
						break;
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_STREAM_AC3:
						toreturn.format = "AC3";
						break;
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_STREAM_ADX:
						toreturn.format = "ADX";
						break;
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_STREAM_AIFF:
						toreturn.format = "WAV";
						break;
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_STREAM_AIX:
						toreturn.format = "AIX";
						break;
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_STREAM_ALAC:
						toreturn.format = "ALAC";
						break;
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_STREAM_AM:
						toreturn.format = "AM";
						break;
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_STREAM_APE:
						toreturn.format = "APE";
						break;
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_STREAM_CA:
						toreturn.format = "CA";
						break;
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_STREAM_CD:
						toreturn.format = "CD";
						break;
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_STREAM_DSD:
						toreturn.format = "DSD";
						break;
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_STREAM_FLAC:
						toreturn.format = "FLAC";
						break;
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_STREAM_FLAC_OGG:
						toreturn.format = "FLAC OGG";
						break;
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_STREAM_MF:
						toreturn.format = "MF";
						break;
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_STREAM_MIDI:
						toreturn.format = "MIDI";
						break;
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_STREAM_MP1:
						toreturn.format = "MP1";
						break;
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_STREAM_MP2:
						toreturn.format = "MP2";
						break;
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_STREAM_MP3:
						toreturn.format = "MP3";
						break;
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_STREAM_MP4:
						toreturn.format = "MP4";
						break;
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_STREAM_MPC:
						toreturn.format = "MPC";
						break;
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_STREAM_OFR:
						toreturn.format = "OFR";
						break;
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_STREAM_OGG:
						toreturn.format = "OGG";
						break;
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_STREAM_OPUS:
						toreturn.format = "OPUS";
						break;
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_STREAM_TTA:
						toreturn.format = "TTA";
						break;
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_STREAM_VIDEO:
						toreturn.format = "VIDEO";
						break;
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_STREAM_WAV:
						toreturn.format = "WAV";
						break;
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_STREAM_WAV_FLOAT:
						toreturn.format = "WAV";
						break;
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_STREAM_WAV_PCM:
						toreturn.format = "WAV";
						break;
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_STREAM_WINAMP:
						toreturn.format = "WINAMP";
						break;
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_STREAM_WMA:
						toreturn.format = "WMA";
						break;
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_STREAM_WMA_MP3:
						toreturn.format = "WMA";
						break;
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_STREAM_WV:
						toreturn.format = "WV";
						break;
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_STREAM_WV_H:
						toreturn.format = "WV";
						break;
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_STREAM_WV_L:
						toreturn.format = "WV";
						break;
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_STREAM_WV_LH:
						toreturn.format = "WV";
						break;
					case Un4seen.Bass.BASSChannelType.BASS_CTYPE_UNKNOWN:
						toreturn.format = "UNK";
						break;
					default:
						toreturn.format = "UNK";
						break;
				}
			} else { toreturn.filename = ""; }

			return toreturn;
		}

	}
}
