using AoiUtils.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AoiUtils.Core.Services;

public class DebloatService
{
    private readonly SystemRunner _runner;

    public DebloatService(SystemRunner runner)
    {
        _runner = runner;
    }

    public List<DebloatTask> GetTasks() => new()
    {
        // Telemetry & Privacy
        new("disable_telemetry", "Disable Telemetry", "Stops Windows from sending usage data to Microsoft.", DebloatTaskCategory.Telemetry, 
            PowerShellCommand: @"reg add ""HKLM\SOFTWARE\Policies\Microsoft\Windows\DataCollection"" /v AllowTelemetry /t REG_DWORD /d 0 /f",
            IconKey: "telemetry_regular"),
        
        new("disable_cortana", "Disable Cortana", "Completely disables Cortana assistant.", DebloatTaskCategory.Privacy,
            PowerShellCommand: @"reg add ""HKLM\SOFTWARE\Policies\Microsoft\Windows\Windows Search"" /v AllowCortana /t REG_DWORD /d 0 /f",
            IconKey: "eye_off_regular"),

        new("disable_wifi_sense", "Disable Wi-Fi Sense", "Prevents sharing Wi-Fi credentials with contacts.", DebloatTaskCategory.Privacy,
            PowerShellCommand: @"reg add ""HKLM\SOFTWARE\Microsoft\WcmSvc\wifisense\Config"" /v AutoConnectAllowedOEM /t REG_DWORD /d 0 /f",
            IconKey: "eye_off_regular"),

        new("disable_location", "Disable Location Tracking", "Disables system-wide location services.", DebloatTaskCategory.Privacy,
            PowerShellCommand: @"reg add ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\location"" /v Value /t REG_SZ /d ""Deny"" /f",
            IconKey: "eye_off_regular"),

        new("disable_activity_history", "Disable Activity History", "Prevents Windows from collecting your activity timeline.", DebloatTaskCategory.Privacy,
            PowerShellCommand: @"reg add ""HKLM\SOFTWARE\Policies\Microsoft\Windows\System"" /v PublishUserActivities /t REG_DWORD /d 0 /f",
            IconKey: "eye_off_regular"),

        // Context Menus
        new("win11_classic_context", "Windows 11 Classic Context Menu", "Restores the old right-click menu without 'Show more options'.", DebloatTaskCategory.ContextMenus,
            PowerShellCommand: @"reg add ""HKCU\Software\Classes\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}\InprocServer32"" /ve /d """" /f",
            IconKey: "box_regular"),

        // UWP Bloatware Removal
        new("remove_solitaire", "Remove Solitaire Collection", "Uninstalls the pre-installed Solitaire game.", DebloatTaskCategory.UWP,
            PowerShellCommand: "Get-AppxPackage *SolitaireCollection* | Remove-AppxPackage",
            IconKey: "trash_regular"),

        new("remove_sticky_notes", "Remove Sticky Notes", "Uninstalls Microsoft Sticky Notes.", DebloatTaskCategory.UWP,
            PowerShellCommand: "Get-AppxPackage *MicrosoftStickyNotes* | Remove-AppxPackage",
            IconKey: "trash_regular"),

        new("remove_maps", "Remove Windows Maps", "Uninstalls the Windows Maps application.", DebloatTaskCategory.UWP,
            PowerShellCommand: "Get-AppxPackage *WindowsMaps* | Remove-AppxPackage",
            IconKey: "trash_regular"),

        new("remove_feedback", "Remove Feedback Hub", "Uninstalls the Feedback Hub.", DebloatTaskCategory.UWP,
            PowerShellCommand: "Get-AppxPackage *WindowsFeedbackHub* | Remove-AppxPackage",
            IconKey: "trash_regular"),

        // System
        new("disable_uac", "Disable UAC (Not Recommended)", "Disables User Account Control prompts.", DebloatTaskCategory.System,
            PowerShellCommand: @"reg add ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System"" /v EnableLUA /t REG_DWORD /d 0 /f",
            IconKey: "shield_regular"),

        new("disable_bing_search", "Disable Bing in Start Menu", "Removes web results from the Windows Start Menu.", DebloatTaskCategory.System,
            PowerShellCommand: @"reg add ""HKCU\Software\Policies\Microsoft\Windows\Explorer"" /v DisableSearchBoxSuggestions /t REG_DWORD /d 1 /f",
            IconKey: "globe_regular"),
        
        new("hide_meet_now", "Hide 'Meet Now'", "Removes the Meet Now icon from the taskbar.", DebloatTaskCategory.System,
            PowerShellCommand: @"reg add ""HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer"" /v HideSCAMeetNow /t REG_DWORD /d 1 /f",
            IconKey: "eye_off_regular")
    };

    public async Task<ProcessResult> RunTaskAsync(DebloatTask task)
    {
        if (!string.IsNullOrEmpty(task.PowerShellCommand))
        {
            return await _runner.RunCommandAsync("cmd.exe", $"/c {task.PowerShellCommand}");
        }
        return new ProcessResult(0, "No action required", "");
    }
}
