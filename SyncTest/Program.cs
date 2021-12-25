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

		//Hack for quickly converting strings into enums
		//https://stackoverflow.com/questions/16100/convert-a-string-to-an-enum-in-c-sharp/38711#38711
		static Dictionary<string, ChangeType> strToChangeType = new Dictionary<string, ChangeType>() {
			{"Delete", ChangeType.Delete },
			{"Add", ChangeType.Add },
			{"Rename", ChangeType.Rename },
			{"MetaChange", ChangeType.MetaChange },
		};

		class SyncEvent {

			//bob the builder
			public SyncEvent(string _syncEventNumber, string _machineID, string _timeStamp) {
				syncEventNumber = _syncEventNumber;
				machineID = _machineID;
				timeStamp = _timeStamp;
				changes = new List<Change>();
			}

			public void AddNewChange(string fileName, string localId, ChangeType changeType, string newFileName) {
				Change e = new Change();
				e.fileName = fileName;
				e.localId = localId;
				e.changeType = changeType;
				e.newFileName = newFileName;
				changes.Add(e);
			}

			public string syncEventNumber;
			public string machineID;
			public string timeStamp;
			public List<Change> changes;

			//used in SyncEvent only
			public struct Change {
				public string fileName;
				public string localId;
				public ChangeType changeType;
				public string newFileName;
			}
		}

		const string clientFolder = @"C:\PERSONAL FILES\WORK\APP\ArientMusicPlayer\SyncTest\TEST";
		const string serverFolder = @"Z:\TestSync\";

		static int serverLatestSyncEvent = 0;
		static int clientLatestSyncEvent = 0;

		//Entry point: Sync Button pressed!
		static void SyncButton() {


			// ====================== Check if directories exist. ==============================

			if (!Directory.Exists(serverFolder)) {
				Console.WriteLine("server folder \'" + serverFolder + "\' does not exist. Attempting to create directory!");
				Directory.CreateDirectory(serverFolder);
				if (!Directory.Exists(serverFolder)) {
					Console.WriteLine("server folder \'" + serverFolder + "\' not able to be created! Aborting");
					return;
				}
			}

			if (!Directory.Exists(clientFolder)) {
				Console.WriteLine("client folder \'" + clientFolder + "\' does not exist. Aborting sync!");
				Directory.CreateDirectory(clientFolder);
				if (!Directory.Exists(clientFolder)) {
					Console.WriteLine("client folder \'" + clientFolder + "\' not able to be created! Aborting");
					return;
				}
			}

			// ====================== Check if files exist. ==============================

			//Check if server .data exists.
			//if not exist, create new .data and .sync.
			Console.WriteLine("Checking data files for server...");
			if (!File.Exists(Path.Join(serverFolder,".data"))) {

				//TODO: Acutally check if .data is parasable. Rebuild if not parsable.
				//Todo: Maybe attempt a repair on the file?
				Console.WriteLine("server .data file not exist, building one!");
				CreateDataFile(serverFolder);

			}

			if (!File.Exists(Path.Join(serverFolder, ".sync"))) {

				//TODO: Acutally check if .data is parasable. Rebuild if not parsable.
				//Todo: Maybe attempt a repair on the file?
				Console.WriteLine("server .sync file not exist, building one!");
				CreateSyncFile(serverFolder);

			}

			//Check if client .data exists.
			//if not exist, create new .data and .sync.
			Console.WriteLine("Checking data files for client...");
			if (!File.Exists(Path.Join(clientFolder, ".data"))) {

				//TODO: Acutally check if .data is parasable. Rebuild if not parsable.
				//Todo: Maybe attempt a repair on the file?
				Console.WriteLine("server .data file not exist, building one!");
				CreateDataFile(clientFolder);

			}

			if (!File.Exists(Path.Join(clientFolder, ".sync"))) {

				//TODO: Acutally check if .data is parasable. Rebuild if not parsable.
				//Todo: Maybe attempt a repair on the file?
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
			SyncEvent clientSync;
			

			LoadDataFile(serverFolder, ref serverData);
			LoadDataFile(clientFolder, ref clientData);

			LoadSyncFile(serverFolder, ref serverSyncs);
			LoadLatsetSyncEvent(serverFolder, ref serverLatestSyncEvent);
			LoadLatsetSyncEvent(clientFolder, ref clientLatestSyncEvent);


			#region //TEST PRINT SERVER .sync and .data
			//foreach (SyncEvent ev in serverSyncs) {
			//	Console.WriteLine("synceventnumber: " + ev.syncEventNumber + " machineid: " + ev.machineID + " time: " + ev.timeStamp);
			//	foreach (SyncEvent.Change chng in ev.changes) {
			//		Console.WriteLine("filename: " + chng.fileName + " localId: " + chng.localId + " changetype: " + chng.changeType + " newfilename: " + chng.newFileName);
			//	}
			//}

			//foreach (KeyValuePair<string, string[]> entry in serverData) {
			//	Console.WriteLine(entry.Key + "|" + entry.Value[0] + "|" + entry.Value[1]);
			//}
			#endregion

			// ====================== Check for sync updates ==============================

			//Compare server .data file to server files on disk.
			Console.WriteLine("Checking file changes for server...");

			#region //testing GetChangesFromDisk!
			//SyncEvent e = GetChangesFromDisk(serverFolder, serverData);
			//Console.WriteLine("\nServer Changes! " + e.syncEventNumber + "|Machine: " + e.machineID + "|No: " + e.syncEventNumber);
			//foreach (SyncEvent.Change r in e.changes) {
			//	Console.WriteLine("Name: " + r.fileName + "|Type: " + r.changeType + "|ID: " + r.localId + "|NewPath: " + r.newFileName);
			//}
			#endregion

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
				sw.WriteLine("0");
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
				//skip the first 3 lines
				if (skipcount < 3) {
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
					list[^1].AddNewChange(temp[0],temp[1], strToChangeType[temp[2]],temp[3]); //list[^1] is the last element in the list
				}

			}

		}

		//Reads the SyncEventNumber from the 3rd line in the .sync file
		static void LoadLatsetSyncEvent(string path, ref int latestSyncEvent) {
			if (!File.Exists(Path.Join(path, ".sync"))) {
				Console.WriteLine("ERROR: .sync file not found at path: " + path + ", unable to load. Did program not manage to create file at this path location?");
				return;
			}

			int skipcount = 0;
			foreach (var line in File.ReadLines(Path.Join(path, ".sync"))) {
				//skip the first 2 lines
				if (skipcount < 2) {
					skipcount++;
					continue;
				}
				//skip everything, go straight to third line, read the number and return.
				latestSyncEvent = int.Parse(line);
				return;
			}
		}

		//Compares current serverData/clientData to what's on the disk right now, and returns a SyncEvent
		//dataFile is the .data parsed prior, belonging to either client or server.
		static SyncEvent GetChangesFromDisk(string path, Dictionary<string, string[]> dataFile) {

			//SyncNumber does not matter here! Will override next time!
			//Create a new SyncEvent, this will be returned and later used to compare against other things.
			SyncEvent newUpdate = new SyncEvent((serverLatestSyncEvent + 1).ToString(),path == serverFolder?"server": Environment.MachineName, DateTime.Now.ToString("yyyyMMddHHmmssFF"));

			//Parse the disk files, and compare to dataFile!
			//Browse each and every one of the song files in this dir, and search on the respective .data file
			foreach (string file in Directory.GetFiles(path, "*", SearchOption.AllDirectories)) {

				string fileR = file.Replace(path,"");

				if (IsMusicFileExtension(Path.GetExtension(fileR))) {

					// 1. Check for ADD/REMOVE.
					// Find the filepath. If found filepath, move straight to check Metachange/last modified.
					if (!dataFile.ContainsKey(fileR)) {
						//if NOT found, means this fileR on disk may be a new fileR OR renamed. Check against ALL LocalIDs.

						// If LocalID match, then is rename. Check for any MetaChange/Last modified. Add rename to changes, then remove the element from dataFile
						// Else, no match, is a new fileR. Add "add" to changes
						// SKIP to next fileR on disk.

						//Get fileR LocalID
						string localID = GetUniqueFileID(file);
						string toRemove = "";

						//Loop all dict items
						foreach (KeyValuePair<string, string[]> item in dataFile) {
							//check lastModified against value
							if (item.Value[0] == localID) {

								//fileR found, this fileR is renamed. Add to changes
								newUpdate.AddNewChange(item.Key, item.Value[0],ChangeType.Rename,fileR);

								//Remove dataFile entry
								toRemove = item.Key;

								//Check for metachange/lastmodified
								if (CheckMetaChange(file,item.Value[1])) {
									newUpdate.AddNewChange(fileR, item.Value[0], ChangeType.MetaChange, "");
								}

								break;
							}
						}

						//if found a LocalID match, remove an element then move to next fileR on disk
						if (toRemove != "") {
							dataFile.Remove(toRemove); //cannot remove element while still in loop.
							continue; 
						}


						// No LocalID match, this fileR on disk is new. Add to change.
						newUpdate.AddNewChange(fileR, GetUniqueFileID(file), ChangeType.Add, "");

					}
					else {
						//If fileR is found, check for MetaChange. Check this fileR's lastModified date with dataFile.
						if (CheckMetaChange(file,dataFile[fileR][1])) {
							newUpdate.AddNewChange(fileR, dataFile[fileR][0], ChangeType.MetaChange, "");
						}

						//Regardless of match or not, remove entry from dataFile.
						dataFile.Remove(fileR);
					}

					//At this point, all files should have been removed from dataFiles, save for the Added/Removed ones. Only those that are missing (Removed) should be leftover in dataFiles.
				}
			}

			//After the loop, check if there is any files leftover in dataFile.
			//Leftovers mean those files have been DELEETED.
			//Add those to chagnes too.

			foreach (KeyValuePair<string, string[]> item in dataFile) {
				newUpdate.AddNewChange(item.Key, item.Value[0], ChangeType.Delete, "");
			}

			//Finally, return
			return newUpdate;

			//To check for metachange/ last modified. Reusable, only within this method.
			static bool CheckMetaChange(string filePath, string dataLastModified) {
				if (long.Parse(File.GetLastWriteTime(filePath).ToString("yyyyMMddHHmmssFF")) > long.Parse(dataLastModified)) return true; else return false;
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
