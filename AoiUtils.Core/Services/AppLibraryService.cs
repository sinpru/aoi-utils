using AoiUtils.Core.Models;

namespace AoiUtils.Core.Services;

public class AppLibraryService
{
    public List<AppItem> GetApps() => new()
    {
        // Browsers
        new("chrome", "Google Chrome", "Fast, secure, and free web browser.", "Browsers", "Google.Chrome", "googlechrome"),
        new("firefox", "Firefox", "Open-source web browser.", "Browsers", "Mozilla.Firefox", "firefox"),
        new("edge", "Microsoft Edge", "Chromium-based browser from Microsoft.", "Browsers", "Microsoft.Edge", "microsoft-edge"),

        // Utilities
        new("7zip", "7-Zip", "File archiver with a high compression ratio.", "Utilities", "7zip.7zip", "7zip"),
        new("powertoys", "PowerToys", "System utilities for power users.", "Utilities", "Microsoft.PowerToys", "powertoys"),
        new("everything", "Everything", "Locate files and folders by name instantly.", "Utilities", "voidtools.Everything", "everything"),

        // Media
        new("vlc", "VLC Media Player", "Multi-platform multimedia player.", "Media", "VideoLAN.VLC", "vlc"),
        new("spotify", "Spotify", "Digital music service.", "Media", "Spotify.Spotify", "spotify"),
        new("obs", "OBS Studio", "Free and open source software for video recording.", "Media", "OBSProject.OBSStudio", "obs-studio"),

        // Dev
        new("vscode", "VS Code", "Code editing. Redefined.", "Dev", "Microsoft.VisualStudioCode", "vscode"),
        new("git", "Git", "Distributed version control system.", "Dev", "Git.Git", "git"),
        new("terminal", "Windows Terminal", "The new Windows Terminal.", "Dev", "Microsoft.WindowsTerminal", "microsoft-windows-terminal"),
    };
}
