#include <iostream>
#include <string>
#include <Windows.h>

// std::wcout DOES NOT WORK out of the box with UNICODE. Use this code snippet.
// https://stackoverflow.com/a/49622513
// IF THIS IS ENABLED, CANNOT USE std::cout, stick to one.

// Also, CP932 is known to be fucking janky as fuck. Converting from jap to UTF8 and back will not be the same.
// https://www.sljfaq.org/afaq/encodings.html#encodings-Conversion-issues
// Solution: Fuck CP932. I changed my entire computer to UTF8, so it's on 65001 now
// NEED TO ENABLE THE THINGY THAT SAYS "Beta" SINCE FUCKING WINDOWS 7
// Downside: Native JP games won't display correctly anymore. Those don't support Unicode.
// Solution: LocaleEmulator.

// Forward declarations
std::wstring ConvertUTF8ToWString(const std::string& str);
std::string ConvertWStringToUTF8(const std::wstring& wstr);
ULONG64 GetUniqueFileID(LPCWSTR path);

int main() {

	//init_locale();

	UINT consoleOutputCodeType = GetConsoleOutputCP();

	if (SetConsoleOutputCP(consoleOutputCodeType)) std::cout << "aaaaaaaaaaaaaaaaaaaaaa" << '\n';;

	std::cout << consoleOutputCodeType << '\n';

	
	std::wstring WIDESTRING{ L"あbitch" };
	std::string NORMALSTRING{ "あbitch" };

	std::cout << "WIDESTRING VALUES:\n";
	for (size_t i = 0; i < WIDESTRING.size(); i++) {
		std::cout << (int)WIDESTRING[i] << ", ";
	}
	std::cout << "\n";
	std::cout << "STRING VALUES:\n";
	for (size_t i = 0; i < NORMALSTRING.size(); i++) {
		std::cout << (int)NORMALSTRING[i] << ", ";
	}
	std::cout << "\n";

	std::cout << "\nSTRING: " << NORMALSTRING << '\n';

	std::cout << "\nConverting Str to Wstr\n";
	std::wstring oo = ConvertUTF8ToWString(NORMALSTRING);

	std::cout << "Wstr VALUES:\n";
	for (size_t i = 0; i < oo.size(); i++) {
		std::cout << (int)oo[i] << ", ";
	}
	std::cout << "\n";

	std::cout << "\nConverting Wstr back to Str\n";
	NORMALSTRING = ConvertWStringToUTF8(oo);

	std::cout << "Str VALUES:\n";
	for (size_t i = 0; i < NORMALSTRING.size(); i++) {
		std::cout << (int)NORMALSTRING[i] << ", ";
	}
	std::cout << "\n";
	std::cout << "Converted str: " << NORMALSTRING << "\n";
	std::cin >> NORMALSTRING;

	//std::cout << GetUniqueFileID(&ConvertUTF8ToWString("C:/PERSONAL FILES/WORK/APP/Icarus/SyncTest/TEST/test/あ.mp3")[0]) << "\n";

	//std::cout << GetUniqueFileID(L"C:/PERSONAL FILES/WORK/APP/Icarus/SyncTest/TEST/test/あ.mp3") << "\n";
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
	return fileIndex;
}

std::wstring ConvertUTF8ToWString(const std::string& str) {
	if (str.empty()) return std::wstring();
	int size_needed = MultiByteToWideChar(CP_UTF8, 0, &str[0], -1, NULL, 0);
	std::wstring wstrTo(size_needed, 0);
	MultiByteToWideChar(CP_UTF8, 0, &str[0], (int)str.size(), &wstrTo[0], size_needed);
	return wstrTo;
}

std::string ConvertWStringToUTF8(const std::wstring& wstr) {
	if (wstr.empty()) return std::string();
	int size_needed = WideCharToMultiByte(CP_UTF8, 0, &wstr[0], -1, NULL, 0, NULL, NULL);
	std::string strTo(size_needed, 0);
	WideCharToMultiByte(CP_UTF8, 0, &wstr[0], (int)wstr.size(), &strTo[0], size_needed, NULL, NULL);
	return strTo;
}