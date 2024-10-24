;------------------------------------------------------------------------------
;
;       Установочный скрипт для Inno Setup 6.2.2
;       24.10.2024
;
;------------------------------------------------------------------------------

;------------------------------------------------------------------------------
;   Определяем некоторые константы
;------------------------------------------------------------------------------
; Имя приложения
#define   Name       "Cherry Math"
; Версия приложения
#define   Version    "1.0.0"
; Фирма-разработчик
#define   Publisher  "andreiAK"
; Сафт фирмы разработчика
#define   URL        "https://github.com/andreiAK-42"
; Имя исполняемого модуля
#define   ExeName    "Cherry Math.exe"

;------------------------------------------------------------------------------
;   Параметры установки
;------------------------------------------------------------------------------
[Setup]

; Уникальный идентификатор приложения
AppId={{31B4CF2B-F5A9-4918-8059-578F7DC2F0A0}

; Прочая информация, отображаемая при установке
AppName={#Name}
AppVersion={#Version}
AppPublisher={#Publisher}
AppPublisherURL={#URL}
AppSupportURL={#URL}
AppUpdatesURL={#URL}

; Путь установки по-умолчанию
DefaultDirName={pf}\{#Name}
; Имя группы в меню "Пуск"
DefaultGroupName={#Name}

; Каталог, куда будет записан собранный setup и имя исполняемого файла
OutputDir=D:\
OutputBaseFileName=CherryMath-setup

; Файл иконки
SetupIconFile=C:\Users\XLEB_YSHEK\Downloads\cherryIcon.ico

; Параметры сжатия
Compression=lzma
SolidCompression=yes

;------------------------------------------------------------------------------
;   Опционально - некоторые задачи, которые надо выполнить при установке
;------------------------------------------------------------------------------
[Tasks]
; Создание иконки на рабочем столе
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

;------------------------------------------------------------------------------
;   Файлы, которые надо включить в пакет установщика
;------------------------------------------------------------------------------
[Files]

; Исполняемый файл
Source: "C:\Users\XLEB_YSHEK\source\repos\Cherry Math\Cherry Math\bin\Debug\net6.0-windows\Cherry Math.exe"; DestDir: "{app}"; Flags: ignoreversion

; Прилагающиеся ресурсы
Source: "C:\Users\XLEB_YSHEK\source\repos\Cherry Math\Cherry Math\bin\Debug\net6.0-windows\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

;------------------------------------------------------------------------------
;   Указываем установщику, где он должен взять иконки
;------------------------------------------------------------------------------ 
[Icons]

Name: "{group}\{#Name}"; Filename: "{app}\{#ExeName}"

Name: "{commondesktop}\{#Name}"; Filename: "{app}\{#ExeName}"; Tasks: desktopicon