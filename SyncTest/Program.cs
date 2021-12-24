using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Un4seen.Bass;

namespace SyncTest {
	class Program {
		static void Main(string[] args) {
			SyncButton();
		}

		enum ChangeType { Delete, Add, Rename, MetaChange }

		const string clientFolder = @"C:\PERSONAL FILES\WORK\APP\ArientMusicPlayer\AMP\Assets\TEST";
		const string serverFolder = @"Z:\TestSync\";

		// On first start: Look at server folder and see if it has been init'd yet.
		// 1. is the .data folder there? is the .sync folder there? Is the .data folder parsable?
		// if any of those are NO, REBUILD.

		// 2. Start checking for changes on the server side, and the client side. Parse ALL files on server, check for their "last modified" date.
		// And also, for extra/missing files.

		// 3. For those files, add them to the server sync change as a new entry. 

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
				BuildDataFile(serverFolder);

			}

			if (!File.Exists(Path.Join(serverFolder, ".sync"))) {

				//fuck it, assuming .data is parasable.
				Console.WriteLine("server .sync file not exist, building one!");
				BuildSyncFile(serverFolder);

			}

			//Check if client .data exists.
			//if not exist, create new .data and .sync.
			Console.WriteLine("Checking data files for client...");
			if (!File.Exists(Path.Join(clientFolder, ".data"))) {

				//fuck it, assuming .data is parasable.
				Console.WriteLine("server .data file not exist, building one!");
				BuildDataFile(clientFolder);

			}

			if (!File.Exists(Path.Join(clientFolder, ".sync"))) {

				//fuck it, assuming .data is parasable.
				Console.WriteLine("server .sync file not exist, building one!");
				BuildSyncFile(clientFolder);

			}

			// ====================== Check for sync updates ==============================

			//Compare .data file to database.
			Console.WriteLine("Checking file changes for server...");

			List <string[] > serverChanges = new List<string[]>();


			Console.WriteLine("Checking file changes for client...");

		}


		// ================= Helper Methods ===========================

		//Takes in target folder path and builds a .data file right there.
		//builds using whatever music files exists in that folder and subfolder.
		static void BuildDataFile(string path) {
			using (StreamWriter sw = File.CreateText(Path.Join(path, ".data"))) {
				sw.WriteLine("#1.0 Data File");
				sw.WriteLine(path);
				sw.WriteLine("");

				//Browse each and every one of the song files in this dir, and put in the info.
				foreach (string file in Directory.GetFiles(path, "*", SearchOption.AllDirectories)) {
					if (IsMusicFileExtension(Path.GetExtension(file))) {
						sw.WriteLine(file.Replace(path, "") + "|" + GetUniqueFileID(file));
					}
				}
			}
		}

		//Takes in target folder path and builds a .sync file right there.
		static void BuildSyncFile(string path) {
			using (StreamWriter sw = File.CreateText(Path.Join(path, ".sync"))) {
				sw.WriteLine("#1.0 Sync File");
				sw.WriteLine("");
			}
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
