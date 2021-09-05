using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace ArientMusicPlayer {


	public static class SyncManager {
		public static string localSyncFolder = "\\sync\\";
		public static string hostSyncFolder = @"Z:\TestSync\";

		public static void WriteSyncFile() { 
			
		}

		public static LibraryPlaylist ReadSyncFile(bool localFolder) {
			string path;
			if (localFolder) {
				path = Directory.GetCurrentDirectory() + localSyncFolder + "\\Library.arientpl";
			} else {
				path = Directory.GetCurrentDirectory() + hostSyncFolder + "\\Library.arientpl";
			}


			LibraryPlaylist oo = (LibraryPlaylist)FileManager.LoadPlaylistFromDisk(path,PlaylistType.LibraryPlaylistLocal);


			return null;
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


		#region IO stuff
		public static void AppendSyncData(SyncData[] data) {
			string path = Directory.GetCurrentDirectory() + localSyncFolder;
			Directory.CreateDirectory(path);
			path += ".sync";

			string[] rawlines = File.ReadAllLines(path);

			string newFileText = "";
			int linesWritten = 0;
			int syncDataIndex = 0;
			int oldFileIndex = 0;

			while (linesWritten < 30) {
				if (syncDataIndex < data.Length) {
					newFileText += data[syncDataIndex].filePath + "|" +
					data[syncDataIndex].fileID + "|" + data[syncDataIndex].fileChangeEvent.ToString() + "|" +
					data[syncDataIndex].timeModified;

					syncDataIndex++;
					continue;
				}

				if (oldFileIndex < rawlines.Length) {
					newFileText += rawlines[oldFileIndex];
				}

				linesWritten++;
			}
			File.WriteAllText(path, newFileText);

		}

		//Returns sync data from local foplder into a list of SyncData.
		public static List<SyncData> LoadSyncData() {
			string path = Directory.GetCurrentDirectory() + localSyncFolder + ".sync";
			if (File.Exists(path)) {

				//if .sync file exists, read it and return a list.
				string[] rawlines = File.ReadAllLines(path);
				List<SyncData> syncDatas = new List<SyncData>();

				foreach (string line in rawlines) {
					if (line != "") {
						string[] data = line.Split('|');
						SyncData syncData = new SyncData();
						syncData.filePath = data[0];
						syncData.fileID = data[1];
						switch (data[2]) {
							case "MetaChange":
								syncData.fileChangeEvent = FileChangeType.MetaChange;
								break;
							case "Add":
								syncData.fileChangeEvent = FileChangeType.Add;
								break;
							case "Delete":
								syncData.fileChangeEvent = FileChangeType.Delete;
								break;
							case "Rename":
								syncData.fileChangeEvent = FileChangeType.Rename;
								break;
						}
						syncData.timeModified = data[3];
						syncDatas.Add(syncData);
					}
				}
				if (syncDatas.Count == 0) {
					return null;
				}
				return syncDatas;
			} else {
				//if .sync file does not exist, return null.
				return null;
			}
		}
		#endregion
	}

	public struct SyncData {
		public string filePath { get; set; }
		public string fileID { get; set; }
		public FileChangeType fileChangeEvent { get; set; }
		public string timeModified { get; set; }
	}

	public enum FileChangeType {MetaChange, Add, Delete, Rename}

}