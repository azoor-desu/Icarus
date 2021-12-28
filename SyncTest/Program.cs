using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using Un4seen.Bass;

namespace SyncTest {
	class Program {

		static void Main(string[] args) {

			try {
				//rename back
				File.Move(@"C:\PERSONAL FILES\WORK\APP\ArientMusicPlayer\SyncTest\TEST\CLIENT\69 two.mp3", @"C:\PERSONAL FILES\WORK\APP\ArientMusicPlayer\SyncTest\TEST\CLIENT\69.mp3");
				File.Move(@"C:\PERSONAL FILES\WORK\APP\ArientMusicPlayer\SyncTest\TEST\SERVER\69 three.mp3", @"C:\PERSONAL FILES\WORK\APP\ArientMusicPlayer\SyncTest\TEST\SERVER\69.mp3");
				//Delete file
				File.Delete(Path.Join(clientFolder, ".sync"));
				File.Delete(Path.Join(serverFolder, ".sync"));
				File.Delete(Path.Join(clientFolder, ".data"));
				File.Delete(Path.Join(serverFolder, ".data"));

				SyncButton();
				Console.WriteLine("================================================================================\n\n");
				//rename
				File.Move(@"C:\PERSONAL FILES\WORK\APP\ArientMusicPlayer\SyncTest\TEST\CLIENT\69.mp3", @"C:\PERSONAL FILES\WORK\APP\ArientMusicPlayer\SyncTest\TEST\CLIENT\69 two.mp3");
				File.Move(@"C:\PERSONAL FILES\WORK\APP\ArientMusicPlayer\SyncTest\TEST\SERVER\69.mp3", @"C:\PERSONAL FILES\WORK\APP\ArientMusicPlayer\SyncTest\TEST\SERVER\69 three.mp3");
				SyncButton();

			} catch (Exception e) {
				Console.WriteLine(e.Message);
			}
			//SyncButton();
		}

		enum ChangeType { Delete, Add, Rename, Modified }

		//Hack for quickly converting strings into enums
		//https://stackoverflow.com/questions/16100/convert-a-string-to-an-enum-in-c-sharp/38711#38711
		static Dictionary<string, ChangeType> strToChangeType = new Dictionary<string, ChangeType>() {
			{"Delete", ChangeType.Delete },
			{"Add", ChangeType.Add },
			{"Rename", ChangeType.Rename },
			{"Modified", ChangeType.Modified },
		};

		class SyncEvent {

			//bob the builder
			public SyncEvent(string _syncEventNumber, string _machineID, string _timeStamp) {
				syncEventNumber = _syncEventNumber;
				machineID = _machineID;
				timeStamp = _timeStamp;
				changes = new List<Change>();
			}

			public void AddNewChange(string fileName, ChangeType changeType, string newFileName) {
				Change e = new Change();
				e.rFileName = fileName;
				e.changeType = changeType;
				e.renamedRFileName = newFileName;
				changes.Add(e);
			}

			public string syncEventNumber;
			public string machineID;
			public string timeStamp;
			public List<Change> changes;

			//used in SyncEvent only
			public struct Change {
				public string rFileName;
				public ChangeType changeType;
				public string renamedRFileName;
			}
		}

		static bool debug = true;

		static string clientFolder = @"C:\PERSONAL FILES\WORK\APP\ArientMusicPlayer\SyncTest\TEST\CLIENT\";
		static string serverFolder = @"C:\PERSONAL FILES\WORK\APP\ArientMusicPlayer\SyncTest\TEST\SERVER\";

		static int globalNextSENumber = 0; //global SE Number for use to give new updates that are to be added to serverSyncs. Increments when new updates are added.
		static int clientNextSENumber = 0; //Client's next SE number. Will not change throughout SyncButton().

		//Entry point: Sync Button pressed!
		static void SyncButton() {


			// ====================== Check if directories exist. ==============================
			#region Check Directories Exist
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
			#endregion
			// ====================== Check if files exist. ==============================
			#region Check Files Exist
			//Check if server .data exists.
			//if not exist, create new .data and .sync.
			Console.WriteLine("Checking data files for server...");
			if (!File.Exists(Path.Join(serverFolder, ".data"))) {

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
			#endregion
			// ====================== Load in from .data and .sync files ==============================
			#region Load Data
			// !!!!!!! Save all parsed results somewhere to be used until the whole sync process is over !!!!!!

			Console.WriteLine("Loading in .data and .sync files...");

			//.data files
			//KEY: relativeFilepath, VALUE: string array [LocalID, LastModified]
			Dictionary<string, string[]> serverData = new Dictionary<string, string[]>();
			Dictionary<string, string[]> clientData = new Dictionary<string, string[]>();

			//.sync files
			//Refer to SyncEvent class. Oldest top, newest bottom. .sync file shld follow this format too.
			List<SyncEvent> serverSyncs = new List<SyncEvent>();

			LoadDataFile(serverFolder, ref serverData);
			LoadDataFile(clientFolder, ref clientData);

			if (debug) {
				Console.WriteLine("\nSERVER LOADED:");
				foreach (KeyValuePair<string, string[]> item in serverData) {
					Console.WriteLine(item.Key + "|" + item.Value[0] + "|" + item.Value[1]);
				}

				Console.WriteLine("\nCLIENT LOADED:");
				foreach (KeyValuePair<string, string[]> item in clientData) {
					Console.WriteLine(item.Key + "|" + item.Value[0] + "|" + item.Value[1]);
				}
			}
			
			LoadSyncFile(serverFolder, ref serverSyncs);
			LoadDiskNextSENumber(serverFolder, ref globalNextSENumber);
			LoadDiskNextSENumber(clientFolder, ref clientNextSENumber);
			int serverNextSENumber = globalNextSENumber; //for use later in FileIO, where i need the original number before the number got changed.

			//Used in Merging & Conflict, and FileIO.
			int SENumberOffset = 0;
			if (serverSyncs.Count > 0) {
				SENumberOffset = int.Parse(serverSyncs[0].syncEventNumber);
			}

			#endregion
			// ====================== Check for sync updates ==============================
			#region Get Sync Changes
			//Compare server .data file to server files on disk.
			Console.WriteLine("Checking file changes for server...");
			SyncEvent serverChanges = GetChangesFromDisk(serverFolder, ref serverData);

			//if changes are NOT NULL, write to .sync, as a new entry IMMEDIATELY.
			if (serverChanges != null) {
				AddToServerSyncEvents(ref serverSyncs, ref serverChanges);
				if (debug) {
					Console.WriteLine("\nServer Changes! " + serverChanges.syncEventNumber + "|Machine: " + serverChanges.machineID + "|No: " + serverChanges.syncEventNumber);
					foreach (SyncEvent.Change r in serverChanges.changes) {
						Console.WriteLine("Name: " + r.rFileName + "|Type: " + r.changeType + "|RenamedPath: " + r.renamedRFileName);
					}
				}
			} else {
				Console.WriteLine("No server changes found!");
			}

			//Compare client .data file to client files on disk.
			Console.WriteLine("Checking file changes for client...");
			SyncEvent clientChanges = GetChangesFromDisk(clientFolder, ref clientData);
			#endregion
			// ====================== Merging & Conflict Management ===========================
			// Compare client changes to server changes, cancel out actions that have alr been done.
			#region Merging and Conflict Management
			if (clientChanges != null) {
				//Grab the offset of index to be used. Assuming the serverSyncs is in order of oldest at index 0 and newest at bottom
				//use offset to grab the relevant SyncEvent.

				List<SyncEvent> outstandingEvents = new List<SyncEvent>();
				// Grab the whole list of SyncEvents the client is missing out on
				for (int i = clientNextSENumber - SENumberOffset; i < serverSyncs.Count; i++) {
					outstandingEvents.Add(serverSyncs[i]);
				}

				// Handle any conflicts in the merge.
				SyncEventConflictResolver(ref outstandingEvents, ref clientChanges);

				// Finally, the merged client changes can be appended to server's .sync (but don't write).
				if (clientChanges.changes.Count > 0) AddToServerSyncEvents(ref serverSyncs, ref clientChanges);
			} else {
				Console.WriteLine("No client changes found!");
			}
			#endregion
			// ====================== File IO Actions =====================
			#region File IO
			Console.WriteLine("Preparing to perform file IO changes!");
			List<SyncEvent.Change> changesToDo = new List<SyncEvent.Change>();

			//Do client file IO.
			Console.WriteLine("\nDoing Client file IO...");
			//serverChanges is already the perfect block to directly carry out actions on Client. Just do actions as is.
			if (serverChanges != null)
				foreach (SyncEvent.Change change in serverChanges.changes) {
					PerformFileIO(serverFolder, clientFolder, change, ref clientData);
				}


			//Do server file IO.
			Console.WriteLine("\nDoing Server file IO...");
			//clientChanges is already the perfect block to directly carry out actions on Client. Just do actions as is.
			if (clientChanges != null)
				foreach (SyncEvent.Change change in clientChanges.changes) {
					PerformFileIO(clientFolder, serverFolder, change, ref serverData);
				}

			#endregion
			// ====================== Updating .sync & .data files ===================
			#region Updating .sync & .data files
			// Trim old entries of .sync. Those older than 6 months will be deleted.
			TrimOldSyncEvents(ref serverSyncs, 31 * 6);

			// Write the entirety of the serverside .sync file into a new .sync file, overwrite.
			Console.WriteLine("Writing server .sync file...");
			WriteSyncFile(serverFolder, ref serverSyncs, globalNextSENumber);
			Console.WriteLine("Writing client .sync file...");
			serverSyncs = null;
			WriteSyncFile(clientFolder, ref serverSyncs, globalNextSENumber);

			// Write the entirety of .data to both server and client.
			Console.WriteLine("Writing server .data file...");
			WriteDataFile(serverFolder, ref serverData);

			Console.WriteLine("Writing client .data file...");
			WriteDataFile(clientFolder, ref clientData);
			#endregion

			Console.WriteLine("\n Songs synchronized!");
		}

		// ================= Helper Methods ===========================

		//Takes in target folder path and builds a .data file right there.
		//builds using whatever music files exists in that folder and subfolder.
		static void CreateDataFile(string path) {
			Directory.CreateDirectory(path);
			using (StreamWriter sw = File.CreateText(Path.Join(path, ".data"))) {
				sw.WriteLine("#1.0 Data File");
				sw.WriteLine(path);
				sw.WriteLine("");
			}
		}

		//Takes in target folder path and builds a .sync file right there.
		static void CreateSyncFile(string path) {
			Directory.CreateDirectory(path);
			using (StreamWriter sw = File.CreateText(Path.Join(path, ".sync"))) {
				sw.WriteLine("#1.0 Sync File");
				sw.WriteLine("");
				sw.WriteLine("0"); //Next sync number
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
				dict.Add(parts[0], new string[] { parts[1], parts[2] });
			}
		}

		//Loads the .sync file in the specific folder.
		//An array of SyncEvent objects, each with a List of changes. Oldest at top, newest at bottom
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

				} else { //add individual sync change entries
					string[] temp = line.Split('|');
					list[^1].AddNewChange(temp[0], strToChangeType[temp[1]], temp[2]); //list[^1] is the last element in the list
				}

			}

		}

		//Reads the SyncEventNumber from the 3rd line in the .sync file.
		//If number is -1, floor it to 0 to prevent array indexes from going out of range.
		static void LoadDiskNextSENumber(string path, ref int nextSENumber) {
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
				nextSENumber = int.Parse(line);
				return;
			}
		}

		//Compares current serverData/clientData to what's on the disk right now, and returns a SyncEvent
		//dataFile is the .data parsed prior, belonging to either client or server.
		static SyncEvent GetChangesFromDisk(string path, ref Dictionary<string, string[]> dataFile) {

			//A list of "entries" to keep. Will use this later after searching thru whole directory
			//to see which entries are leftover. Leftovers are files that are DELETED.
			List<string> dataFileTemp = new List<string>(dataFile.Keys);

			//SyncNumber does not matter here! Will override next time!
			//Create a new SyncEvent, this will be returned and later used to compare against other things.
			SyncEvent newUpdate = new SyncEvent(globalNextSENumber.ToString(), path == serverFolder ? "server" : Environment.MachineName, DateTime.Now.ToString("yyyyMMddHHmmss"));

			//Parse the disk files, and compare to dataFile!
			//Browse each and every one of the song files in this dir, and search on the respective .data file
			foreach (string filepathFull in Directory.GetFiles(path, "*", SearchOption.AllDirectories)) {

				//If not a music file, skip.
				if (!IsMusicFileExtension(Path.GetExtension(filepathFull))) continue;

				//removes the front part of the filepath to give the relative filepath.
				string fileName = filepathFull.Replace(path, "");

				//Get fileName LocalID, used for both FOUND and NOT FOUND.
				string localID = GetUniqueFileID(filepathFull);

				// 1. Check for ADD/REMOVE.
				// Find the filepath. If found filepath, move straight to check Metachange/last modified.
				if (!dataFile.ContainsKey(fileName)) {
					//if NOT found, means this fileName on disk may be a new fileName OR renamed. Check against ALL LocalIDs.

					// If LocalID match, then is rename. Check for any Modified/Last modified. Add rename to changes, then remove the element from dataFile
					// Else, no match, is a new fileName. Add "add" to changes
					// SKIP to next fileName on disk.

					bool skip = false;

					//Loop all remaining items in list
					for (int i = 0; i < dataFileTemp.Count; i++) {

						//check lastModified against value
						if (dataFile[dataFileTemp[i]][0] == localID) {
							//fileName found, this fileName is renamed. Add to changes
							newUpdate.AddNewChange(dataFileTemp[i], ChangeType.Rename, fileName);

							//Check for modify/lastmodified
							if (dataFileTemp.Count > 0 && CheckMetaChange(filepathFull, dataFile[dataFileTemp[i]][1])) {
								newUpdate.AddNewChange(fileName, ChangeType.Modified, "");
							}

							//Remove dataFile entry
							dataFileTemp.RemoveAt(i);
							//At this point, because i am removing an element from a list, the list indexes changes.
							//i should change too, to keep synchronized. BUT, i'm breaking the code right after this so i'm not concerned about i anymore.
							break;
						}
					}

					if (skip) continue;

					// No LocalID match, this fileName on disk is new. Add to change.
					newUpdate.AddNewChange(fileName, ChangeType.Add, "");

					//Also add to .data file since it's gonna be empty
					dataFile.Add(fileName,new string[] { GetUniqueFileID(filepathFull), File.GetLastWriteTime(filepathFull).ToString("yyyyMMddHHmmss") });

				} else {
					//If fileName is found, check for Modified. Check this fileName's lastModified date with dataFile.
					if (CheckMetaChange(filepathFull, dataFile[fileName][1])) {
						newUpdate.AddNewChange(fileName, ChangeType.Modified, "");
					}

					// At this point, LocalId is not checked. There exists a certain scenario:
					// An identical file is drag-dropped into the folder, changing its LocalId.
					// There is no way to tell if the contents are the same or not. The only thing we are sure of is that the
					// filename is exactly the same. So Modified aside, this is essentially still the same file. Force update the LocalId.

					if (dataFile[fileName][0] != localID) {
						dataFile[fileName][0] = localID;
					}

					//Regardless of match or not, remove entry from dataFileTemp.
					dataFileTemp.Remove(fileName);
				}

				//At this point, all files should have been removed from dataFiles, save for the Added/Removed ones. Only those that are missing (DELETED) should be leftover in dataFiles.

			}

			//After the loop, check if there is any files leftover in dataFile.
			//Leftovers mean those files have been DELETED.
			//Add those to changes too.

			foreach (string item in dataFileTemp) {
				newUpdate.AddNewChange(item, ChangeType.Delete, "");
			}

			//if there were no changes, return null to indicate nothing to update.
			if (newUpdate.changes.Count == 0) {
				return null;
			} else {
				//Attempt a Logic check to ensure newUpdate isn't giving garbage updates
				// Rules:
				// 1. ADD must not have ANY PRIOR ACTION other than DELETE on the same file
				// 2. RENAME/MODIFIED/DELETE must not have a prior DELETE on the same file
				// BY RIGHT, NO ERRORS SHOULD APPEAR. IF ERRORS, IS A MAJOR BUG.

				//Loop each item against every item below it down the list.
				//If a logical error is found, uhhhhhhhhhhhhhh idk man return null, whole thing is borked!
				for (int i = 0; i < newUpdate.changes.Count; i++) {
					string newFileName = newUpdate.changes[i].rFileName; //name to change if a rename happens
					for (int j = i + 1; j < newUpdate.changes.Count; j++) {
						if (newFileName == newUpdate.changes[j].rFileName || newFileName == newUpdate.changes[j].renamedRFileName) {

							//check the changeType of the sync change after this current guy.
							if (newUpdate.changes[j].changeType == ChangeType.Add) {
								//Rule 1: if current guy is NOT delete, error
								if (newUpdate.changes[i].changeType != ChangeType.Delete) {
									Console.WriteLine("ERROR: GetChangesFromDisk did not pass Logic Check! (ADD must not have ANY PRIOR ACTION other than DELETE on the same file)");
									Console.WriteLine("Error'd Sync Number: " + newUpdate.syncEventNumber + "|Machine: " + newUpdate.machineID + "|Time: " + newUpdate.timeStamp);
									foreach (SyncEvent.Change r in newUpdate.changes) {
										Console.WriteLine("Name: " + r.rFileName + "|Type: " + r.changeType + "|RenamedPath: " + r.renamedRFileName);
									}
									Console.WriteLine("Offending pair: Current: " + newUpdate.changes[j].rFileName + "|" + newUpdate.changes[j].changeType +
									" Slave: " + newUpdate.changes[i].rFileName + "|" + newUpdate.changes[j].changeType);
									return null;
								}
							} else { //current guy type is RENAME/MODIFIED/DELETE
									 //Rule 2: if current guy is delete, error
								if (newUpdate.changes[i].changeType == ChangeType.Delete) {
									Console.WriteLine("ERROR: GetChangesFromDisk did not pass Logic Check! (RENAME/MODIFIED/DELETE must not have a prior DELETE on the same file)");
									Console.WriteLine("Error'd Sync Number: " + newUpdate.syncEventNumber + "|Machine: " + newUpdate.machineID + "|Time: " + newUpdate.timeStamp);
									foreach (SyncEvent.Change r in newUpdate.changes) {
										Console.WriteLine("Name: " + r.rFileName + "|Type: " + r.changeType + "|RenamedPath: " + r.renamedRFileName);
									}
									Console.WriteLine("Offending pair: Current: " + newUpdate.changes[j].rFileName + "|" + newUpdate.changes[j].changeType +
									" Slave: " + newUpdate.changes[i].rFileName + "|" + newUpdate.changes[j].changeType);
									return null;
								}
							}
						}
					}
				}
			}

			//Finally, return
			return newUpdate;

			//To check for modify/ last modified. Reusable, only within this method.
			static bool CheckMetaChange(string filePath, string dataLastModified) {
				Console.WriteLine("Checkign file:" + filePath + "\nNewFile: " + File.GetLastWriteTime(filePath).ToString("yyyyMMddHHmmss") + " lastSaved: " + dataLastModified +
				(long.Parse(File.GetLastWriteTime(filePath).ToString("yyyyMMddHHmmss")) > long.Parse(dataLastModified) ? " tru" : " false"));
				if (long.Parse(File.GetLastWriteTime(filePath).ToString("yyyyMMddHHmmss")) > long.Parse(dataLastModified)) return true; else return false;
			}

		}

		//Adds a new SyncEvent to serverSyncs, after performing parity checks with the rest of the data in serverSyncs.
		static void AddToServerSyncEvents(ref List<SyncEvent> serverSyncs, ref SyncEvent newSyncEvent) {

			if (newSyncEvent == null || newSyncEvent.changes.Count == 0) return;

			SyncEventConflictResolver(ref serverSyncs, ref newSyncEvent);

			if (newSyncEvent == null || newSyncEvent.changes.Count == 0) return;

			Console.WriteLine("Adding server file changes as new SyncEvent entry...");
			serverSyncs.Add(newSyncEvent);
			globalNextSENumber++;

			if (debug) {
				Console.WriteLine("\n ========== Current serverSyncs =============");
				foreach (SyncEvent r in serverSyncs) {
					Console.WriteLine(r.syncEventNumber + "|Machine: " + r.machineID + "|Time: " + r.timeStamp);
					foreach (SyncEvent.Change c in r.changes) {
						Console.WriteLine(c.rFileName + "|" + c.changeType + "|" + c.renamedRFileName);

					}
					Console.WriteLine("");
				}
			}

		}

		//Compares current newSyncEvent to a list of existing SyncEvent, and edit newSyncEvent so it dosen't conflict with the existing SyncEvent.
		//Used in AddToServerSyncEvents() and main SyncButton() logic for merging & conflict management
		static void SyncEventConflictResolver(ref List<SyncEvent> oldEvents, ref SyncEvent newSyncEvent) {
			//scan thru all the changes in newSyncEvent.
			// Rules:
			// 1. ADD must not have ANY PRIOR ACTION other than DELETE on the same file (use filePath as ID). Discard ADD if so, and warn user.
			// 2. RENAME/MODIFIED/DELETE must not have a prior DELETE on the same file (use filePath as ID). Discard RENAME/MODIFIED/DELETE if so, and warn user.

			//Stores a temp array of who to remove AFTER the loop, cos cannot delete elements while iterating.
			//0 means no delete, 1 means delete.
			int[] toRemove = new int[newSyncEvent.changes.Count];
			bool skip = false; //to stop the loop

			for (int i = 0; i < newSyncEvent.changes.Count; i++) {
				string newFileName = newSyncEvent.changes[i].rFileName; //to update this as it scans in case there has been a rename
				skip = false;

				//scan thru the newSyncEvent for any renames, so we use the correct name that is in parity with the old list.
				//only scan from current position upwards, as renames that happen that will affect parity with old list will be ABOVE.
				for (int j = i; j >= 0; j--) {
					if (newSyncEvent.changes[j].changeType == ChangeType.Rename) {
						newFileName = newSyncEvent.changes[j].rFileName;
						//DO NOT BREAK, as there may be multiple renames after the first one.
					}
				}

				//Scan thru the entire list of oldEvents and try to find any parity issues.
				foreach (SyncEvent syncEvent in oldEvents) {
					foreach (SyncEvent.Change oldChange in syncEvent.changes) {
						if (newFileName == oldChange.rFileName || newFileName == oldChange.renamedRFileName) {

							switch (newSyncEvent.changes[i].changeType) {

								case ChangeType.Add:
									// 1. MUST NOT HAVE ANY PRIOR ACTIONS other then DELETE. Mark for deletion and move on.
									if (oldChange.changeType != ChangeType.Delete) {
										toRemove[i] = 1;
										skip = true;
										Console.WriteLine("WARNING: Existing file added in new SyncEvent! Discarding! " +
										newSyncEvent.changes[i].rFileName +
										"|" + newSyncEvent.changes[i].changeType);
									}
									break;

								case ChangeType.Delete:
									if (oldChange.changeType == ChangeType.Delete) {
										// 2. DELETED FILE MUST NOT HAVE DELETE. Mark for deletion and move on.
										toRemove[i] = 1;
										skip = true;
										Console.WriteLine("WARNING: Deleted file deleted again in new SyncEvent! Discarding! " +
										newSyncEvent.changes[i].rFileName +
										"|" + newSyncEvent.changes[i].changeType);
									}
									break;

								case ChangeType.Rename:

									if (oldChange.changeType == ChangeType.Delete) {
										// 2. DELETED FILE MUST NOT HAVE RENAME. Mark for deletion and move on.
										toRemove[i] = 1;
										skip = true;
										Console.WriteLine("WARNING: Deleted file renamed in new SyncEvent! Discarding! " + newSyncEvent.changes[i].rFileName +
										"|" + newSyncEvent.changes[i].changeType +
										"|" + newSyncEvent.changes[i].renamedRFileName);
									}

									//if file was renamed, update file name to the old one.
									if (oldChange.changeType == ChangeType.Rename && newFileName == oldChange.renamedRFileName) {
										newFileName = oldChange.rFileName;
									}
									break;

								case ChangeType.Modified:
									if (oldChange.changeType == ChangeType.Delete) {
										// 2. DELETED FILE MUST NOT HAVE MODIFIED. Mark for deletion and move on.
										toRemove[i] = 1;
										skip = true;
										Console.WriteLine("WARNING: Deleted file modified in new SyncEvent! Discarding! " + newSyncEvent.changes[i].rFileName +
										"|" + newSyncEvent.changes[i].changeType);
									}
									break;
							}
							if (skip) break;
						}
					}
					if (skip) break;
				}
			}

			//Delete entries in newSyncEvent
			//delete from the back, so it won't affect the stuff at thr front.
			for (int i = newSyncEvent.changes.Count - 1; i >= 0; i--) {
				if (toRemove[i] == 1) {
					newSyncEvent.changes.RemoveAt(i);
				}
			}

			//OK done, newSyncEvent has been parity checked. Conflicts have been discarded!
		}

		//CURRENTLY NOT IN USE.
		// Goes through a list of SyncEvents, and returns a compressed list of SyncEvent.Changes (to save on FILE IO operations)
		// Start index and end index are inclusive.
		// e.g. File is renamed, modified then deleted. Compress to just deleting the file. Or rename twice, change to renaming just once.
		static void GetCompresssedChanges(ref List<SyncEvent> listEvents, int startIndex, int endIndex, ref List<SyncEvent.Change> listCompressedChanges) {
			// Rules: 
			// 1. If DELETE, remove ALL ACTIONS prior.
			// 2. If RENAME, remove all prior RENAMES. Update current RENAME to use the removed ORIGINAL NAME.
			// 3. If MODIFIED, remove all prior MODIFIES.

			//Clear the list and add all items in the range into the list so i can carry out the rules.
			//Getting them all into a single list also makes life easier.
			listCompressedChanges.Clear();
			for (int i = startIndex; i <= endIndex; i++) {
				foreach (SyncEvent.Change item in listEvents[i].changes) {
					listCompressedChanges.Add(item);
				}
			}
			

			//Start working from newest entry to oldest so Rename logic will work.
			for (int i = listCompressedChanges.Count - 1; i >= 0; i--) {

				string currentFileName = listCompressedChanges[i].rFileName; //to change as rename happens in logic

				//If is ADD, then skip (should NOT have any events/changes prior to this).
				if (listCompressedChanges[i].changeType == ChangeType.Add) continue;
				
				//Else, start another loop backwards, this time from this item's -1 index to 0.
				for (int j = i - 1; j >= 0; j--) {

					//check for fileName match
					if (listCompressedChanges[j].rFileName == currentFileName || listCompressedChanges[j].renamedRFileName == currentFileName) {
						//switch the original guy's type.
						switch (listCompressedChanges[i].changeType) {
							case ChangeType.Delete:
								// 1. DELETE ANY ACTIONS PRIOR.
								//Delete current j guy.
								listCompressedChanges.RemoveAt(j);
								j--;
								i--;
								break;

							case ChangeType.Modified:
								// 3. DELETE ANY METAHCANGES PRIOR.
								if (listCompressedChanges[j].changeType == ChangeType.Modified) {
									//Delete current j guy.
									listCompressedChanges.RemoveAt(j);
									j--;
									i--;
								}
								break;

							case ChangeType.Rename:
								// 2. DELETE ANY RENAMES PRIOR.
								if (listCompressedChanges[j].changeType == ChangeType.Rename) {
									//Rename the current name
									currentFileName = listCompressedChanges[j].rFileName;

									//Delete current j guy.
									listCompressedChanges.RemoveAt(j);
									j--;
									i--;
								}
								break;
						}
					}
				}
				
				//Update renamed name
				if (listCompressedChanges[i].rFileName != currentFileName) {
					SyncEvent.Change ee = new SyncEvent.Change();
					ee.rFileName = currentFileName;
					ee.changeType = listCompressedChanges[i].changeType;
					ee.renamedRFileName = listCompressedChanges[i].renamedRFileName;
					listCompressedChanges[i] = ee;
				}
			}

			//OK, listCompressedChanges has been filtered and compressed!

			if (debug) {
				Console.WriteLine("COMPRESSING from " + startIndex + " to " + endIndex);
				foreach (SyncEvent.Change change in listCompressedChanges) {
					Console.WriteLine(change.rFileName + "|" + change.changeType + "|" + change.renamedRFileName);
				}
			}
		}

		//Perform a singular File IO Operation, from sourceFolder to targetFolder.
		//sourceFolder is ignored for IO that only happens on targetFolder.
		static void PerformFileIO(string hostFolder, string targetFolder, SyncEvent.Change change, ref Dictionary<string, string[]> targetData) {
			// On ADD, copy file to target, add new .data entry
			// On DELETE, remove file from target, remove .data entry
			// On RENAME, rename file at target, update fileName in .data entry
			// On MODIFIED, copy & replace file to target, update LocalId

			Console.WriteLine(change.rFileName + "|" + change.changeType);

			string hostFile = Path.Join(hostFolder, change.rFileName);
			string destFile = Path.Join(targetFolder, change.rFileName);
			string destDir = Path.GetDirectoryName(destFile);

			switch (change.changeType) {
				case ChangeType.Add:
					//Check if source file exists
					if (!File.Exists(hostFile)) {
						Console.WriteLine("ERROR: On add: File not found - " + hostFile);
						break;
					}

					//Check if destination file exists
					if (File.Exists(destFile)) {
						Console.WriteLine("Warning: File exists! Skipping. " + hostFile);
						break;
					}

					//Create dir
					Directory.CreateDirectory(destDir);

					//Copy file
					try {
						File.Copy(hostFile, destFile, false);
					} catch (Exception e) {
						Console.WriteLine("Error copying file: " + hostFile + "\nError Msg: " + e.Message);
						break;
					}

					//add new .data entry to data
					if (!targetData.ContainsKey(change.rFileName)) {
						targetData.Add(change.rFileName, new string[] { GetUniqueFileID(destFile), File.GetLastWriteTime(destFile).ToString("yyyyMMddHHmmss") });
					} else {
						Console.WriteLine("WARNING: Could not add new entry to .data, entry already exists: " + change.rFileName + "\n.data File: " + targetFolder);
						targetData[change.rFileName] = new string[] { GetUniqueFileID(destFile), File.GetLastWriteTime(destFile).ToString("yyyyMMddHHmmss") };
					}
					break;

				case ChangeType.Delete:
					//Check if target file exists
					if (!File.Exists(destFile)) {
						Console.WriteLine("ERROR: On delete: File not found - " + destFile);
						break;
					}

					//Delete file
					try {
						File.Delete(destFile);
					} catch (Exception e) {
						Console.WriteLine("Error deleting file: " + destFile + "\nError Msg: " + e.Message);
						break;
					}

					//remove .data entry to data
					if (targetData.ContainsKey(change.rFileName)) {
						targetData.Remove(change.rFileName);
					} else {
						Console.WriteLine("WARNING: Could not delete entry in .data, entry not found: " + change.rFileName + "\n.data File: " + targetFolder);
					}
					break;

				case ChangeType.Rename:
					//Check if target file exists
					if (!File.Exists(destFile)) {
						Console.WriteLine("ERROR: On rename: File not found - " + destFile);
						break;
					}

					//rename file
					try {
						File.Move(destFile,Path.Join(targetFolder,change.renamedRFileName));
					} catch (Exception e) {
						Console.WriteLine("Error renaming file: " + destFile + "\nError Msg: " + e.Message);
						break;
					}

					// update .data entry
					if (targetData.ContainsKey(change.rFileName)) {
						targetData.Add(change.renamedRFileName, targetData[change.rFileName]);
						targetData.Remove(change.rFileName);
					} else {
						Console.WriteLine("WARNING: Could not edit entry in .data, entry not found: " + change.rFileName + "\n.data File: " + targetFolder);
					}
					break;

				case ChangeType.Modified:
					//Check if source file exists
					if (!File.Exists(hostFile)) {
						Console.WriteLine("ERROR: On modify: File not found - " + hostFile);
						break;
					}

					//Copy file
					try {
						File.Copy(hostFile, destFile, true);
					} catch (Exception e) {
						Console.WriteLine("Error copying(modify) file: " + hostFile + "\nError Msg: " + e.Message);
						break;
					}

					//update .data entry to data
					if (targetData.ContainsKey(change.rFileName)) {
						targetData[change.rFileName][0] = GetUniqueFileID(destFile);
					} else {
						Console.WriteLine("WARNING: Could not edit entry in .data, entry not found: " + change.rFileName + "\n.data File: " + targetFolder);
					}
					break;
			}

		}

		//Deletes entries older than current date + daysToTrim.
		//Also stop trimming if there's less than 10 entries.
		static void TrimOldSyncEvents(ref List<SyncEvent> toTrim, int daysToTrim) {
			long cutoffDate = long.Parse(DateTime.Now.AddDays(-daysToTrim).ToString("yyyyMMddHHmmss"));

			//Start at i = 9 to skip the first 10 entries.
			for (int i = 9; i < toTrim.Count; i++) {
				Console.WriteLine("DOING " + toTrim[i].syncEventNumber + " TIMESPAMP: " + toTrim[i].timeStamp + " CUTOFF: " + cutoffDate);
				if (long.Parse(toTrim[i].timeStamp) < cutoffDate) {
					Console.WriteLine("TOO OLD, REMOVED: " + toTrim[i].syncEventNumber);
					toTrim.RemoveAt(i);
					i--; //removing element in the middle of a loop
				} else return; //assume the rest are in order. if this guy is bigger, no need to check the rest.
			}
		}

		//Writes a new sync file to disk. Accepts null sync file. Used to write client .sync
		static void WriteSyncFile(string path, ref List<SyncEvent> syncs, int nextSENumber) {
			Directory.CreateDirectory(path);
			using (StreamWriter sw = File.CreateText(Path.Join(path, ".sync"))) {
				sw.WriteLine("#1.0 Sync File");
				sw.WriteLine("");
				sw.WriteLine(nextSENumber); //Next sync number

				if (syncs != null) {
					foreach (SyncEvent ev in syncs) {
						sw.WriteLine("#" + ev.syncEventNumber + "|" + ev.machineID + "|" + ev.timeStamp);
						foreach (SyncEvent.Change chng in ev.changes) {
							sw.WriteLine(chng.rFileName + "|" + chng.changeType + "|" + chng.renamedRFileName);
						}
					}
				}
			}
		}

		//Writes a new data file to disk.
		static void WriteDataFile(string path, ref Dictionary<string, string[]> data) {
			Directory.CreateDirectory(path);
			using (StreamWriter sw = File.CreateText(Path.Join(path, ".data"))) {
				sw.WriteLine("#1.0 Data File");
				sw.WriteLine(path);
				sw.WriteLine("");

				if (data != null && data.Count > 0) {
					foreach (KeyValuePair<string, string[]> item in data) {
						sw.WriteLine(item.Key + "|" + item.Value[0] + "|" + item.Value[1]);
					}
				}
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
