using AoiUtils.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AoiUtils.Core.Services;

public class TweakService
{
    private readonly SystemRunner _runner;

    public TweakService(SystemRunner runner)
    {
        _runner = runner;
    }

    public List<Tweak> GetTweaks() => new()
    {
        // Performance
        new("ultimate_performance", "Ultimate Performance Power Plan", "Enables the hidden Ultimate Performance power scheme.", TweakCategory.Performance,
            PowerShellCommand: "powercfg -duplicatescheme e9a42b02-d5df-448d-aa00-03f14749eb61"),
        
        new("disable_hibernation", "Disable Hibernation", "Disables hibernation and deletes the hiberfil.sys file to save space.", TweakCategory.Performance,
            PowerShellCommand: "powercfg -h off"),

        // Visuals
        new("disable_animations", "Disable Window Animations", "Speeds up the UI by disabling window minimize/maximize animations.", TweakCategory.Visuals,
            PowerShellCommand: @"reg add ""HKCU\Control Panel\Desktop\WindowMetrics"" /v MinAnimate /t REG_SZ /d 0 /f"),

        new("disable_transparency", "Disable Transparency Effects", "Disables transparency effects to reduce GPU usage.", TweakCategory.Visuals,
            PowerShellCommand: @"reg add ""HKCU\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize"" /v EnableTransparency /t REG_DWORD /d 0 /f"),

        // Explorer
        new("show_extensions", "Show File Extensions", "Configures Explorer to always show file extensions.", TweakCategory.Explorer,
            PowerShellCommand: @"reg add ""HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced"" /v HideFileExt /t REG_DWORD /d 0 /f"),

        new("hide_shortcuts", "Hide 'Shortcut' Text", "Prevents Windows from adding ' - Shortcut' to new shortcuts.", TweakCategory.Explorer,
            PowerShellCommand: @"reg add ""HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer"" /v link /t REG_BINARY /d 00000000 /f"),

        // Gaming
        new("enable_game_mode", "Enable Game Mode", "Ensures Windows Game Mode is enabled for better gaming performance.", TweakCategory.Gaming,
            PowerShellCommand: @"reg add ""HKCU\Software\Microsoft\GameBar"" /v AllowAutoGameMode /t REG_DWORD /d 1 /f")
    };

    public async Task<ProcessResult> RunTweakAsync(Tweak tweak)
    {
        if (!string.IsNullOrEmpty(tweak.PowerShellCommand))
        {
            // Most tweaks are registry-based, cmd /c is fine for these
            return await _runner.RunCommandAsync("cmd.exe", $"/c {tweak.PowerShellCommand}");
        }
        
        return new ProcessResult(0, "No action required", "");
    }
}
