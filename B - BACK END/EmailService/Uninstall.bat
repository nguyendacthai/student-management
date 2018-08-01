@ECHO OFF

REM The following directory is for .NET 4.5
set DOTNETFX=%SystemRoot%\Microsoft.NET\Framework64\v4.0.30319
set PATH=%PATH%;%DOTNETFX%

echo Uninstalling EmailSenderService...
echo ---------------------------------------------------
InstallUtil /u  "EmailService.exe"
echo ---------------------------------------------------
echo Done.
