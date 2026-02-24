namespace AoiUtils.Core.Models;

public enum DebloatTaskCategory
{
    Telemetry,
    Privacy,
    UWP,
    ContextMenus,
    System
}

public record DebloatTask(
    string Id,
    string Name,
    string Description,
    DebloatTaskCategory Category,
    string RegistryPath = "",
    string RegistryValue = "",
    object? RegistryData = null,
    string PowerShellCommand = "",
    string IconKey = "shield_regular")
{
    public bool IsSelected { get; set; }
}
