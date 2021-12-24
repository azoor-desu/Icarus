using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using Un4seen.Bass;

namespace SyncTest {
	class Program {
		static void Main(string[] args) {
			SyncButton();
		}

		enum ChangeType { Delete, Add, Rename, MetaChange }

		class SyncEvent {

			//bob the builder
			public SyncEvent(string _syncEventNumber, string _machineID, string _timeStamp) {
				syncEventNumber = _syncEventNumber;
				machineID = _machineID;
				timeStamp = _timeStamp;
				changes = new List<string[]>();
			}

			public string syncEventNumber;
			public string machineID;
			public string timeStamp;
			public List<string[]> changes;
		}

		const string clientFolder = @"C:\PERSONAL FILES\WORK\APP\ArientMusicPlayer\AMP\Assets\TEST";
		const string serverFolder = @"Z:\TestSync\";

		//Entry point: Sync Button pressed!
		static void SyncButton() {


			// ====================== Check if directories exist. ==============================

			if (!Directory.Exists(serverFolder)) {
				Console.WriteLine("server folder \'" + serverFolder + "\' does not exist. Aborting sync!");
				return;
			}

			if (!Directory.Exists(clientFolder)) {
				Console.WriteLine("client folder \'" + clientFolder + "\' does not exist. Aborting sync!");
				return;
			}

			// ====================== Check if files exist. ==============================

			//Check if server .data exists.
			//if not exist, create new .data and .sync.
			Console.WriteLine("Checking data files for server...");
			if (!File.Exists(Path.Join(serverFolder,".data"))) {

				//fuck it, assuming .data is parasable.
				Console.WriteLine("server .data file not exist, building one!");
				CreateDataFile(serverFolder);

			}

			if (!File.Exists(Path.Join(serverFolder, ".sync"))) {

				//fuck it, assuming .data is parasable.
				Console.WriteLine("server .sync file not exist, building one!");
				CreateSyncFile(serverFolder);

			}

			//Check if client .data exists.
			//if not exist, create new .data and .sync.
			Console.WriteLine("Checking data files for client...");
			if (!File.Exists(Path.Join(clientFolder, ".data"))) {

				//fuck it, assuming .data is parasable.
				Console.WriteLine("server .data file not exist, building one!");
				CreateDataFile(clientFolder);

			}

			if (!File.Exists(Path.Join(clientFolder, ".sync"))) {

				//fuck it, assuming .data is parasable.
				Console.WriteLine("server .sync file not exist, building one!");
				CreateSyncFile(clientFolder);

			}

			// ====================== Load in from .data and .sync files ==============================

			// !!!!!!! Save all parsed results somewhere to be used until the whole sync process is over !!!!!!

			Console.WriteLine("Loading in .data and .sync files...");

			//.data files
			//KEY: relativeFilepath, VALUE: string array [LocalID, LastModified]
			Dictionary<string, string[]> serverData = new Dictionary<string, string[]>();
			Dictionary<string, string[]> clientData = new Dictionary<string, string[]>();

			//.sync files
			//Refer to SyncEvent class
			List<SyncEvent> serverSyncs = new List<SyncEvent>();
			List<SyncEvent> clientSyncs = new List<SyncEvent>();

			LoadDataFile(serverFolder, ref serverData);
			LoadDataFile(clientFolder, ref clientData);

			LoadSyncFile(serverFolder, ref serverSyncs);
			LoadSyncFile(clientFolder, ref clientSyncs);


			#region //TEST PRINT SERVER .sync and .data
			//foreach (SyncEvent ev in serverSyncs) {
			//	Console.WriteLine("synceventnumber: " + ev.syncEventNumber + " machineid: " + ev.machineID + " time: " + ev.timeStamp);
			//	foreach (string[] str in ev.changes) {
			//		for (int i = 0; i < str.Length; i++) {
			//			Console.Write(str[i] + "|");
			//		}
			//		Console.Write('\n');
			//	}
			//}

			//foreach (KeyValuePair<string, string[]> entry in serverData) {
			//	Console.WriteLine(entry.Key + "|" + entry.Value[0] + "|" + entry.Value[1]);
			//}
			#endregion

			// ====================== Check for sync updates ==============================

			//Compare server .data file to server files on disk.
			Console.WriteLine("Checking file changes for server...");
			//GetChangesFromDisk(serverFolder); //WIP

			//if changes are NOT NULL, write to .sync, as a new entry IMMEDIATELY.
			Console.WriteLine("Writing file changes for server...");


			//Compare client .data file to client files on disk.
			Console.WriteLine("Checking file changes for client...");

			// ====================== Merging & Conflict Management ===========================

			// Compare client changes to server changes, cancel out actions that have alr been done.

			// Handle any conflicts in the merge. Use rules on the paper. Merging conflicts and Logical conflicts.

			// Finally, the merged client changes can be appended to server's .sync (but don't write).

			// ====================== File IO Actions =====================

			// After merge, check current .sync versions of both server and client. Get the sync updates to perform.

			// Do update on client. Rename/delete/copy over files. Also update .data file entries.

			// Do update on server. Rename/delete/copy over files. Also update .data file entries.

			// ====================== Updating .sync files ===================

			// Trim old entries of .sync. Those older than 6 months will be deleted.

			// Write the entirety of the serverside .sync file into a new .sync file, overwrite.

			// Write the missing sync entries to the client .sync file. Append, latest entry at the top.

		}


		// ================= Helper Methods ===========================

		//Takes in target folder path and builds a .data file right there.
		//builds using whatever music files exists in that folder and subfolder.
		static void CreateDataFile(string path) {
			using (StreamWriter sw = File.CreateText(Path.Join(path, ".data"))) {
				sw.WriteLine("#1.0 Data File");
				sw.WriteLine(path);
				sw.WriteLine("");

				//Browse each and every one of the song files in this dir, and put in the info.
				foreach (string file in Directory.GetFiles(path, "*", SearchOption.AllDirectories)) {
					if (IsMusicFileExtension(Path.GetExtension(file))) {
						sw.WriteLine(file.Replace(path, "") + "|" + GetUniqueFileID(file) + "|" + File.GetLastWriteTime(file).ToString("yyyyMMddHHmmssFF"));
					}
				}
			}
		}

		//Takes in target folder path and builds a .sync file right there.
		static void CreateSyncFile(string path) {
			using (StreamWriter sw = File.CreateText(Path.Join(path, ".sync"))) {
				sw.WriteLine("#1.0 Sync File");
				sw.WriteLine("");
			}
		}

		//Loads the .data file in the specific folder.
		//KEY: relativeFilepath, VALUE: string array [LocalID, LastModified]
		static void LoadDataFile(string path, ref Dictionary<string, string[]> dict) {
			if (!File.Exists(Path.Join(path, ".data"))) {
				Console.WriteLine("ERROR: .data file not found at path: " + path + ", unable to load. Did program not manage to create file at this path location?");
				return;
			}

			dict.Clear();

			int skipcount = 0;
			foreach (var line in File.ReadLines(Path.Join(path, ".data"))) {
				//skip the first 3 lines
				if (skipcount < 3) {
					skipcount++;
					continue;
				}

				//each readline is an entry now. Add them to the dictionary.
				//parts[0]: relative path
				//parts[1]: LocalID
				//parts[2]: last modified
				string[] parts = line.Split('|');
				dict.Add(parts[0], new string[] {parts[1], parts[2]});
			}
		}

		//Loads the .sync file in the specific folder.
		//An array of SyncEvent objects, each with a List of changes.
		static void LoadSyncFile(string path, ref List<SyncEvent> list) {
			if (!File.Exists(Path.Join(path, ".sync"))) {
				Console.WriteLine("ERROR: .sync file not found at path: " + path + ", unable to load. Did program not manage to create file at this path location?");
				return;
			}

			list.Clear();

			int skipcount = 0;
			foreach (var line in File.ReadLines(Path.Join(path, ".sync"))) {
				//skip the first 2 lines
				if (skipcount < 2) {
					skipcount++;
					continue;
				}
				//If first char starts with #, new sync event!
				if (line[0] == '#') { //add sync event
					string[] temp = line.Remove(0, 1).Split('|'); //remove the # and split
					list.Add(new SyncEvent(temp[0], temp[1], temp[2]));

				}
				else { //add individual sync change entries
					string[] temp = line.Split('|');
					list[^1].changes.Add(temp);
				}

			}

		}

		//Compares current serverData/clientData to what's on the disk right now, and returns a SyncEvent
		//dataFile is the .data parsed prior, belonging to either client or server.
		static void GetChangesFromDisk(string path, ref Dictionary<string, string[]> dataFile) {

			//SyncNumber does not matter here!
			//Create a new SyncEvent, this will be returned and later used to compare against other things.
			SyncEvent newUpdate = new SyncEvent("nil",path == serverFolder?"server":"thisMachine", DateTime.Now.ToString("yyyyMMddHHmmssFF"));

			//Parse the disk files, and compare to dataFile!

		}

		//Gets the latest (biggest number) SyncEventNumber
		static int GetSyncEventNumber(List<SyncEvent> syncs) {

			int biggest = -1;
			foreach (SyncEvent ev in syncs) {
				if (int.Parse(ev.syncEventNumber) > biggest) biggest = int.Parse(ev.syncEventNumber);
			}

			return biggest;
		}

		//Takes a file extension. period dosen't matter.
		static bool IsMusicFileExtension(string ext) {
			if (ext.Length > 0 && ext[0] == '.') {
				ext = ext.Remove(0, 1);
			}

			ext = ext.ToUpper();

			switch (ext) {
				case "3GP":
				case "AA":
				case "AAX":
				case "ACT":
				case "AIFF":
				case "ALAC":
				case "AMR":
				case "APE":
				case "AU":
				case "AWB":
				case "DSS":
				case "DVF":
				case "FLAC":
				case "GSM":
				case "M4A":
				case "M4B":
				case "M4P":
				case "MMF":
				case "MIDI":
				case "MP1":
				case "MP2":
				case "MP3":
				case "MP4":
				case "MPC":
				case "OGG":
				case "OGA":
				case "MOGG":
				case "RAW":
				case "RF64":
				case "TTA":
				case "WAV":
				case "WEBM":
				case "CDA":
				case "OPUS":
				case "VIDEO":
				case "WINAMP":
				case "WMA":
				case "WV":
					return true;
				default:
					return false;
			}
		} 

		#region NTFS Unique Identifier Getter
		//See: https://stackoverflow.com/questions/1866454/unique-file-identifier-in-windows
		//Things to note: ID will never change on NTFS systems, unless deleted.
		//If moved from drive to drive, ID will change. If file is deleted and replaced, ID will change.
		//If used on FAT system, ID MAY change over time due to how the system stores bytes.
		//So far, not sure if ID will change on ext4 on linux.

		//FOR USE ON LOCAL SYSTEM ONLY. so the ext4 problem dosen't matter.

		[DllImport("kernel32.dll")]
		public static extern bool GetFileInformationByHandle(IntPtr hFile, out BY_HANDLE_FILE_INFORMATION lpFileInformation);

		public struct BY_HANDLE_FILE_INFORMATION {
			public uint FileAttributes;
			public System.Runtime.InteropServices.ComTypes.FILETIME CreationTime;
			public System.Runtime.InteropServices.ComTypes.FILETIME LastAccessTime;
			public System.Runtime.InteropServices.ComTypes.FILETIME LastWriteTime;
			public uint VolumeSerialNumber;
			public uint FileSizeHigh;
			public uint FileSizeLow;
			public uint NumberOfLinks;
			public uint FileIndexHigh;
			public uint FileIndexLow;
		}

		public static string GetUniqueFileID(string path) {
			BY_HANDLE_FILE_INFORMATION objectFileInfo = new BY_HANDLE_FILE_INFORMATION();

			FileInfo fi = new FileInfo(path);
			FileStream fs = fi.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

#pragma warning disable CS0618 // Type or member is obsolete
			GetFileInformationByHandle(fs.Handle, out objectFileInfo);
#pragma warning restore CS0618 // Type or member is obsolete

			fs.Close();

			ulong fileIndex = ((ulong)objectFileInfo.FileIndexHigh << 32) + (ulong)objectFileInfo.FileIndexLow;

			return fileIndex.ToString();

		}
		#endregion
	}
}
