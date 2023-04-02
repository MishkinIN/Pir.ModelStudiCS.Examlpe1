// Pir.ModelStudio.CS.Wraper.h: основной файл заголовка для библиотеки DLL Pir.ModelStudio.CS.Wraper
//

#pragma once

#ifndef __AFXWIN_H__
	#error "включить pch.h до включения этого файла в PCH"
#endif

#include "resource.h"		// основные символы

#include "CSGraphicsInterfaces.h"

// CPirModelStudioCSWraperApp
// Реализацию этого класса см. в файле Pir.ModelStudio.CS.Wraper.cpp
//

class CPirModelStudioCSWraperApp : public CWinApp
{
public:
	CPirModelStudioCSWraperApp();

// Переопределение
public:
	virtual BOOL InitInstance();

	DECLARE_MESSAGE_MAP()
};
