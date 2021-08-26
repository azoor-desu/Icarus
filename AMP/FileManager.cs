using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ArientMusicPlayer {
	public class FileManager {


		//Loads in a Playlist file from disk and overwrites a passed List.
		public static bool ImportPlaylist(string pathofile, ref List<string> refPlaylist) {

			//pathofile = pathofile.Replace("\\","\\\\");
			if (File.Exists(pathofile)) {
				string[] lines = File.ReadAllLines(pathofile);
				//BUILT FOR .m38u FILES.

				//Loop thru and ignore all arrays starting with a # or if total characters > 8 (C:/a.mp3 is 8 chars)
				//TODO: Do a regex that validates filepaths.
				//See lowest replay: https://stackoverflow.com/questions/12947405/string-path-validation
				//Then add them to the refPlaylist.

				refPlaylist.Clear();

				foreach (string line in lines) {
					if (line[0] != '#' && line.Length > 8) {
						refPlaylist.Add(line);
					} else {
						//Logging.Warning("Line when loading Playlist is invalid path: " + line);
					}
				}

				if (refPlaylist.Count != 0) {
					return true;
				} else  {
					Logging.Error("Loading Playlist: No valid songs found in playlist! " + pathofile);
					refPlaylist.Add("ERROR NO VALID FILES"); //prevents a null playlist from breaking the design window.
					return false;
				}


			} else {
				Logging.Error("Loading Playlist: File path of Playlist does not exist: " + pathofile);
				return false;
			}
		}

	}

}
