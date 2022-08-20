#include "SyncLogic.h"
#include "framework.h"
#include <fstream>

void StartSync(){
	
}



//See: https://stackoverflow.com/questions/1866454/unique-file-identifier-in-windows
//Things to note: ID will never change on NTFS systems, unless deleted.
//If moved from drive to drive, ID will change. If file is deleted and replaced, ID will change.
//If used on FAT system, ID MAY change over time due to how the system stores bytes.
//So far, not sure if ID will change on ext4 on linux.
//FOR USE ON LOCAL SYSTEM ONLY. so the ext4 problem dosen't matter.
ULONG64 GetUniqueFileID(LPCWSTR path) {

	// FILE_FLAG_BACKUP_SEMANTICS is required to get a handle to the file.
	HANDLE handle1 = CreateFileW(path, 0, 0, NULL, OPEN_EXISTING, FILE_FLAG_BACKUP_SEMANTICS, NULL);

	BY_HANDLE_FILE_INFORMATION info;
	if (!GetFileInformationByHandle(handle1, &info)) {
		CloseHandle(handle1);
		std::cout << "ERROR\n";
		return 0; // On not able to get, return empty.
	}
	CloseHandle(handle1);

	ULONG64 fileIndex = ((ULONG64)info.nFileIndexHigh << 32) + (ULONG64)info.nFileIndexLow;
	std::cout << fileIndex << '\n';
	return fileIndex;
}