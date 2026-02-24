using AoiUtils.Core.Models;
using System.Collections.Generic;

namespace AoiUtils.Core.Services;

public class AppLibraryService
{
    public List<AppItem> GetApps() => new()
    {
        new("chrome", "Google Chrome", "Fast, secure, and free web browser.", "Browsers", "Google.Chrome", "googlechrome", "google.com"),
        new("firefox", "Firefox", "Open-source web browser.", "Browsers", "Mozilla.Firefox", "firefox", "mozilla.org"),
        new("edge", "Microsoft Edge", "Chromium-based browser from Microsoft.", "Browsers", "Microsoft.Edge", "microsoft-edge", "microsoft.com"),

        new("7zip", "7-Zip", "File archiver with a high compression ratio.", "Utilities", "7zip.7zip", "7zip", "7-zip.org"),
        new("powertoys", "PowerToys", "System utilities for power users.", "Utilities", "Microsoft.PowerToys", "powertoys", "microsoft.com"),
        new("everything", "Everything", "Locate files and folders by name instantly.", "Utilities", "voidtools.Everything", "everything", "voidtools.com"),

        new("vlc", "VLC Media Player", "Multi-platform multimedia player.", "Media", "VideoLAN.VLC", "vlc", "videolan.org"),
        new("spotify", "Spotify", "Digital music service.", "Media", "Spotify.Spotify", "spotify", "spotify.com"),
        new("obs", "OBS Studio", "Free and open source software for video recording.", "Media", "OBSProject.OBSStudio", "obs-studio", "obsproject.com"),

        new("vscode", "VS Code", "Code editing. Redefined.", "Dev", "Microsoft.VisualStudioCode", "vscode", "visualstudio.com"),
        new("git", "Git", "Distributed version control system.", "Dev", "Git.Git", "git", "git-scm.com"),
        new("terminal", "Windows Terminal", "The new Windows Terminal.", "Dev", "Microsoft.WindowsTerminal", "microsoft-windows-terminal", "microsoft.com"),
    };
}
