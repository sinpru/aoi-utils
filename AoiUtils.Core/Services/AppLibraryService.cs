using AoiUtils.Core.Models;
using System.Collections.Generic;

namespace AoiUtils.Core.Services;

public class AppLibraryService
{
    public List<AppItemDto> GetApps() => new()
    {
        // AIO Libraries & Runtimes
        new("cpp_redist", "C++ Redistributables AIO", "All Microsoft Visual C++ Redistributable packages.", "Libraries", "Microsoft.VCRedist.2015+.x64", "vcredist-all", "microsoft.com"),
        new("dotnet_runtime", ".NET Desktop Runtime", "Required to run many Windows desktop apps.", "Libraries", "Microsoft.DotNet.DesktopRuntime.8", "dotnet-desktopruntime", "microsoft.com"),
        new("directx", "DirectX Runtime", "Legacy DirectX End-User Runtime.", "Libraries", "Microsoft.DirectX", "directx", "microsoft.com"),

        // Browsers
        new("chrome", "Google Chrome", "Fast, secure, and free web browser.", "Browsers", "Google.Chrome", "googlechrome", "google.com"),
        new("firefox", "Firefox", "Open-source web browser.", "Browsers", "Mozilla.Firefox", "firefox", "mozilla.org"),
        new("edge", "Microsoft Edge", "Chromium-based browser from Microsoft.", "Browsers", "Microsoft.Edge", "microsoft-edge", "microsoft.com"),
        new("brave", "Brave Browser", "Privacy-focused browser.", "Browsers", "Brave.Brave", "brave", "brave.com"),

        // Social & Communication
        new("discord", "Discord", "All-in-one voice and text chat for gamers.", "Social", "Discord.Discord", "discord", "discord.com"),
        new("telegram", "Telegram", "Cloud-based messaging app.", "Social", "Telegram.TelegramDesktop", "telegram", "telegram.org"),
        new("zoom", "Zoom", "Video conferencing and meeting software.", "Social", "Zoom.Zoom", "zoom", "zoom.us"),

        // Gaming
        new("steam", "Steam", "Digital distribution platform for video games.", "Gaming", "Valve.Steam", "steam", "steampowered.com"),
        new("epic", "Epic Games Launcher", "Storefront for Epic Games.", "Gaming", "EpicGames.EpicGamesLauncher", "epicgameslauncher", "epicgames.com"),

        // Creative & Media
        new("obs", "OBS Studio", "Free software for video recording and streaming.", "Creative", "OBSProject.OBSStudio", "obs-studio", "obsproject.com"),
        new("vlc", "VLC Media Player", "Multi-platform multimedia player.", "Creative", "VideoLAN.VLC", "vlc", "videolan.org"),
        new("spotify", "Spotify", "Digital music service.", "Creative", "Spotify.Spotify", "spotify", "spotify.com"),

        // Development
        new("vscode", "VS Code", "Code editing. Redefined.", "Dev", "Microsoft.VisualStudioCode", "vscode", "visualstudio.com"),
        new("git", "Git", "Distributed version control system.", "Dev", "Git.Git", "git", "git-scm.com"),
        new("nodejs", "Node.js (LTS)", "JavaScript runtime built on Chrome's V8 engine.", "Dev", "OpenJS.NodeJS.LTS", "nodejs-lts", "nodejs.org"),
        new("python", "Python", "High-level programming language.", "Dev", "Python.Python.3", "python", "python.org"),
        new("terminal", "Windows Terminal", "The new Windows Terminal.", "Dev", "Microsoft.WindowsTerminal", "microsoft-windows-terminal", "microsoft.com"),

        // Utilities & Tools
        new("7zip", "7-Zip", "File archiver with a high compression ratio.", "Utilities", "7zip.7zip", "7zip", "7-zip.org"),
        new("winrar", "WinRAR", "Powerful archiver and archive manager.", "Utilities", "RARLab.WinRAR", "winrar", "rarlab.com"),
        new("powertoys", "PowerToys", "System utilities for power users.", "Utilities", "Microsoft.PowerToys", "powertoys", "microsoft.com"),
        new("everything", "Everything", "Locate files and folders by name instantly.", "Utilities", "voidtools.Everything", "everything", "voidtools.com"),
        new("notepadplusplus", "Notepad++", "Free source code editor.", "Utilities", "Notepad++.Notepad++", "notepadplusplus", "notepad-plus-plus.org"),
    };
}
