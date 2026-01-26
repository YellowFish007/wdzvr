set WORKSPACE=..
set LUBAN_DLL=%WORKSPACE%\Tools\Luban\Luban.dll
set CONF_ROOT=.

dotnet %LUBAN_DLL% ^
    -t all ^
    -d json ^
    -c cs-simple-json ^
    --conf %CONF_ROOT%\luban.conf ^
    -x outputCodeDir=../../Assets/Game/Scripts/HotFix/_generate/LubanExcel ^
    -x outputDataDir=../../Assets/Game/Resources/RawAssets/Text/Excel

pause