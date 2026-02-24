using AoiUtils.Core.Models;
using System.Collections.Generic;

namespace AoiUtils.Core.Services;

public class AppLibraryService
{
    public List<AppItem> GetApps() => new()
    {
        // Browsers
        new("chrome", "Google Chrome", "Fast, secure, and free web browser.", "Browsers", "Google.Chrome", "googlechrome", "google.com"),
        new("firefox", "Firefox", "Open-source web browser.", "Browsers", "Mozilla.Firefox", "firefox", "mozilla.org"),
        new("edge", "Microsoft Edge", "Chromium-based browser from Microsoft.", "Browsers", "Microsoft.Edge", "microsoft-edge", "microsoft.com"),
        new("brave", "Brave Browser", "Privacy-focused browser.", "Browsers", "Brave.Brave", "brave", "brave.com"),

        // Social & Communication
        new("discord", "Discord", "All-in-one voice and text chat for gamers.", "Social", "Discord.Discord", "discord", "discord.com"),
        new("telegram", "Telegram", "Cloud-based messaging app.", "Social", "Telegram.TelegramDesktop", "telegram", "telegram.org"),
        new("zoom", "Zoom", "Video conferencing and meeting software.", "Social", "Zoom.Zoom", "zoom", "zoom.us"),
        new("slack", "Slack", "Team communication and collaboration.", "Social", "SlackTechnologies.Slack", "slack", "slack.com"),

        // Gaming
        new("steam", "Steam", "Digital distribution platform for video games.", "Gaming", "Valve.Steam", "steam", "steampowered.com"),
        new("epic", "Epic Games Launcher", "Storefront for Epic Games.", "Gaming", "EpicGames.EpicGamesLauncher", "epicgameslauncher", "epicgames.com"),
        new("gog", "GOG Galaxy", "DRM-free game client.", "Gaming", "GOG.Galaxy", "goggalaxy", "gog.com"),
        new("ea", "EA App", "Electronic Arts game launcher.", "Gaming", "ElectronicArts.EADesktop", "ea-app", "ea.com"),

        // Creative & Media
        new("gimp", "GIMP", "Free & open source image editor.", "Creative", "GIMP.GIMP", "gimp", "gimp.org"),
        new("blender", "Blender", "3D creation suite.", "Creative", "BlenderFoundation.Blender", "blender", "blender.org"),
        new("inkscape", "Inkscape", "Vector graphics editor.", "Creative", "Inkscape.Inkscape", "inkscape", "inkscape.org"),
        new("paintnet", "Paint.NET", "Image and photo editing software.", "Creative", "dotPDN.PaintDotNet", "paint.net", "getpaint.net"),
        new("obs", "OBS Studio", "Free software for video recording and streaming.", "Creative", "OBSProject.OBSStudio", "obs-studio", "obsproject.com"),
        new("vlc", "VLC Media Player", "Multi-platform multimedia player.", "Creative", "VideoLAN.VLC", "vlc", "videolan.org"),
        new("spotify", "Spotify", "Digital music service.", "Creative", "Spotify.Spotify", "spotify", "spotify.com"),

        // Development
        new("vscode", "VS Code", "Code editing. Redefined.", "Dev", "Microsoft.VisualStudioCode", "vscode", "visualstudio.com"),
        new("visualstudio", "Visual Studio 2022", "Comprehensive IDE for .NET.", "Dev", "Microsoft.VisualStudio.2022.Community", "visualstudio2022community", "visualstudio.microsoft.com"),
        new("git", "Git", "Distributed version control system.", "Dev", "Git.Git", "git", "git-scm.com"),
        new("nodejs", "Node.js (LTS)", "JavaScript runtime built on Chrome's V8 engine.", "Dev", "OpenJS.NodeJS.LTS", "nodejs-lts", "nodejs.org"),
        new("python", "Python", "High-level programming language.", "Dev", "Python.Python.3", "python", "python.org"),
        new("docker", "Docker Desktop", "Containerization platform.", "Dev", "Docker.DockerDesktop", "docker-desktop", "docker.com"),
        new("terminal", "Windows Terminal", "The new Windows Terminal.", "Dev", "Microsoft.WindowsTerminal", "microsoft-windows-terminal", "microsoft.com"),

        // Utilities & Tools
        new("7zip", "7-Zip", "File archiver with a high compression ratio.", "Utilities", "7zip.7zip", "7zip", "7-zip.org"),
        new("winrar", "WinRAR", "Powerful archiver and archive manager.", "Utilities", "RARLab.WinRAR", "winrar", "rarlab.com"),
        new("powertoys", "PowerToys", "System utilities for power users.", "Utilities", "Microsoft.PowerToys", "powertoys", "microsoft.com"),
        new("everything", "Everything", "Locate files and folders by name instantly.", "Utilities", "voidtools.Everything", "everything", "voidtools.com"),
        new("notepadplusplus", "Notepad++", "Free source code editor.", "Utilities", "Notepad++.Notepad++", "notepadplusplus", "notepad-plus-plus.org"),
        new("cpuz", "CPU-Z", "System information software.", "Utilities", "CPUID.CPU-Z", "cpu-z", "cpuid.com"),
        new("hwmonitor", "HWMonitor", "Hardware monitoring program.", "Utilities", "CPUID.HWMonitor", "hwmonitor", "cpuid.com"),
        new("rufus", "Rufus", "Create bootable USB drives.", "Utilities", "Rufus.Rufus", "rufus", "rufus.ie")
    };
}
