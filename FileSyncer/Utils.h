#pragma once

#include "framework.h"

// ANSI to UTF8
std::wstring ConvertStrToWstr(const std::string& str);

// UTF8 to ANSI
std::string ConvertWstrToStr(const std::wstring& wstr);