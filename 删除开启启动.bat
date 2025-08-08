@echo off
>nul chcp 65001
:: 删除开机自启动脚本
:: 与 install_autostart.bat 配套使用

:: 设置程序名称（必须和 install_autostart.bat 中的 APP_NAME 一致）
set APP_NAME=CosyMonitor

echo.
echo 🛠 正在移除开机自启动项...
echo 名称: %APP_NAME%
echo.

:: 从注册表删除启动项
reg delete "HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run" /v "%APP_NAME%" /f >nul

:: 检查是否删除成功
if %errorlevel% equ 0 (
    echo ✅ 成功！已移除开机自启动。
    echo 重启后将不再自动启动。
) else (
    echo ⚠️ 未找到自启动项，可能已删除或从未添加。
    echo 请确认程序名称是否正确，或检查注册表。
)

echo.
pause