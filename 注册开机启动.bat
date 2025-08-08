@echo off
>nul chcp 65001
:: 开机自启动安装脚本
:: 作者：你的名字
:: 功能：将当前目录下的程序注册为开机自启动

:: 设置程序名称（注册表中显示的名称）
set APP_NAME=CosyMonitor

:: 设置程序主文件名（必须与实际 exe 名称一致）
set EXE_NAME=CosyMonitor.exe

:: 获取当前脚本所在目录（自动处理路径）
set APP_DIR=%~dp0

:: 转义反斜杠（替换 \ 为 \\，用于注册表）
set APP_DIR_ESCAPED=%APP_DIR:\=\\%

:: 构建完整的可执行路径（带引号，防空格）
set FULL_PATH="%APP_DIR%%EXE_NAME%"

:: 检查程序是否存在
if not exist %FULL_PATH% (
    echo.
    echo ❌ 错误：未找到程序文件 %FULL_PATH%
    echo 请确保 %EXE_NAME% 与本脚本在同一目录！
    echo.
    pause
    exit /b 1
)

:: 写入注册表（HKEY_CURRENT_USER，无需管理员权限）
reg add "HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run" /v "%APP_NAME%" /t REG_SZ /d %FULL_PATH% /f >nul

:: 检查是否成功
if %errorlevel% equ 0 (
    echo.
    echo ✅ 成功！程序已添加到开机自启动。
    echo 名称: %APP_NAME%
    echo 路径: %FULL_PATH%
    echo 重启电脑后生效。
    echo.
) else (
    echo.
    echo ❌ 写入注册表失败！可能是权限问题。
    echo 请以管理员身份运行？（本脚本不需要管理员权限，一般不会失败）
    echo.
)

pause