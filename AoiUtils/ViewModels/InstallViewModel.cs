using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AoiUtils.Models;
using AoiUtils.Core.Models;
using AoiUtils.Core.Services;
using AoiUtils.Services;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AoiUtils.ViewModels;

public partial class InstallViewModel : ViewModelBase
{
    private readonly AppLibraryService _appLibrary;
    private readonly PackageManagerService _packageManager;
    private readonly NotificationService _notificationService;
    private readonly List<AppItem> _allApps;
    private static readonly HttpClient _httpClient = new HttpClient();
    
    [ObservableProperty]
    private LocalizationManager _localizer;

    [ObservableProperty]
    private ObservableCollection<AppItem> _filteredApps;

    [ObservableProperty]
    private string _searchText = string.Empty;

    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private string _progressText = "";

    [ObservableProperty]
    private string _preferredManager = "WinGet";

    public InstallViewModel(
        AppLibraryService appLibrary, 
        PackageManagerService packageManager,
        LocalizationManager localizer,
        NotificationService notificationService)
    {
        _appLibrary = appLibrary;
        _packageManager = packageManager;
        _localizer = localizer;
        _notificationService = notificationService;
        
        // Map DTOs to UI models
        _allApps = _appLibrary.GetApps().Select(dto => new AppItem(
            dto.Id, dto.Name, dto.Description, dto.Category, 
            dto.WinGetId, dto.ChocoId, dto.Website)).ToList();
            
        _filteredApps = new ObservableCollection<AppItem>(_allApps);

        _ = LoadIconsAsync();
    }

    private async Task LoadIconsAsync()
    {
        foreach (var app in _allApps)
        {
            if (string.IsNullOrEmpty(app.IconUrl)) continue;

            try
            {
                var bytes = await _httpClient.GetByteArrayAsync(app.IconUrl);
                using var ms = new System.IO.MemoryStream(bytes);
                app.IconBitmap = new Bitmap(ms);
                app.IsIconLoaded = true;
            }
            catch { /* Fallback to letter icon */ }
        }
    }

    partial void OnSearchTextChanged(string value) => FilterApps();

    private void FilterApps()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            FilteredApps = new ObservableCollection<AppItem>(_allApps);
        }
        else
        {
            var filtered = _allApps.Where(a => 
                a.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) || 
                a.Category.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                a.Description.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
            FilteredApps = new ObservableCollection<AppItem>(filtered);
        }
    }

    [RelayCommand]
    private async Task InstallSelectedAsync()
    {
        var selected = _allApps.Where(a => a.IsSelected).ToList();
        if (!selected.Any()) return;

        IsBusy = true;
        foreach (var app in selected)
        {
            ProgressText = string.Format(Localizer["Installing"], app.Name);
            ProcessResult result;

            if (PreferredManager == "Choco")
                result = await _packageManager.InstallWithChocoAsync(app.ChocoId);
            else
                result = await _packageManager.InstallWithWinGetAsync(app.WinGetId);

            if (!result.IsSuccess)
                _notificationService.Show($"Failed to install {app.Name}");
        }

        _notificationService.Show(Localizer["InstallationComplete"]);
        ProgressText = "";
        IsBusy = false;
    }

    [RelayCommand]
    private async Task UninstallSelectedAsync()
    {
        var selected = _allApps.Where(a => a.IsSelected).ToList();
        if (!selected.Any()) return;

        IsBusy = true;
        foreach (var app in selected)
        {
            ProgressText = $"Uninstalling {app.Name}...";
            if (PreferredManager == "Choco")
                await _packageManager.UninstallWithChocoAsync(app.ChocoId);
            else
                await _packageManager.UninstallWithWinGetAsync(app.WinGetId);
        }

        _notificationService.Show("Uninstall complete!");
        ProgressText = "";
        IsBusy = false;
    }

    [RelayCommand]
    private async Task InstallChocoAsync()
    {
        IsBusy = true;
        ProgressText = "Installing Chocolatey...";
        var result = await _packageManager.InstallChocolateyAsync();
        
        if (result.IsSuccess)
            _notificationService.Show("Chocolatey installed successfully!");
        else
            _notificationService.Show("Failed to install Chocolatey. Run as Admin.");

        ProgressText = "";
        IsBusy = false;
    }
}
