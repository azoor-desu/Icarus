using System.Collections.Generic;
using System.IO;
using Un4seen.Bass.AddOn.Tags;

namespace ArientMusicPlayer {
	public class FileManager {


		//Loads in a Playlist file from disk and overwrites a passed-as-reference Playlist.
		public static Arient.Playlist ImportPlaylist(string pathofile) {
			List<string> temp = new List<string>();

			if (File.Exists(pathofile)) {
				string[] lines = File.ReadAllLines(pathofile);
				//BUILT FOR .m38u FILES.

				//Loop thru and ignore all arrays starting with a # or if total characters > 8 (C:/a.mp3 is 8 chars)
				//TODO: Do a regex that validates filepaths.
				//See lowest reply: https://stackoverflow.com/questions/12947405/string-path-validation
				//Then add them to the refPlaylist.

				foreach (string line in lines) {
					if (line[0] != '#' && line.Length > 8) {
						temp.Add(line);
					}
				}

				//temp now holds all (hopefully) valid filepaths. Transfer them into a new Playlist Object!

				if (temp.Count != 0) { //If ther isn't any valid filepaths, Log an error and return null.
					
					//Create a new Playlist, loop through the new TAG_INFO in Playlist, and write the TAGS.

					Arient.Playlist tempPlaylist = new Arient.Playlist(temp.Count,true);
					for (int i = 0; i < tempPlaylist.songs.Length; i++) {
						tempPlaylist.songs[i] = GetTag(temp[i]);
					}
					//Add the name of the file to the name var in tempPlaylist
					tempPlaylist.name = Path.GetFileName(pathofile).Split('.')[1];
					return tempPlaylist;
				} else  {
					Logger.Error("Loading Playlist: No valid songs found in playlist! " + pathofile);
					return null;
				}

			} else {
				Logger.Error("Loading Playlist: File path of Playlist does not exist: " + pathofile);
				return null;
			}
		}


		public static TAG_INFO GetTag(string path) {
			TAG_INFO toreturn = null;
			try {
				toreturn = BassTags.BASS_TAG_GetFromFile(path);
			} catch {
				Logger.Error("Unable to load tag from file: " + path);
			}

			return toreturn;
		}

	}

}
