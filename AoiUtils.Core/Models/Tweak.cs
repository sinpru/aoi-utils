namespace AoiUtils.Core.Models;

public enum TweakCategory
{
    Performance,
    Visuals,
    Explorer,
    Gaming
}

public record Tweak(
    string Id,
    string Name,
    string Description,
    TweakCategory Category,
    string PowerShellCommand = "",
    string IconKey = "settings_regular")
{
    public bool IsSelected { get; set; }
}
