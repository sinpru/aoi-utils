using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AoiUtils.Core.Models;
using AoiUtils.Core.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AoiUtils.ViewModels;

public partial class InstallViewModel : ViewModelBase
{
    private readonly AppLibraryService _appLibrary;
    private readonly PackageManagerService _packageManager;
    private readonly List<AppItem> _allApps;

    [ObservableProperty]
    private ObservableCollection<AppItem> _filteredApps;

    [ObservableProperty]
    private string _searchText = string.Empty;

    [ObservableProperty]
    private bool _isInstalling;

    [ObservableProperty]
    private string _progressText = "";

    public InstallViewModel(AppLibraryService appLibrary, PackageManagerService packageManager)
    {
        _appLibrary = appLibrary;
        _packageManager = packageManager;
        _allApps = _appLibrary.GetApps();
        _filteredApps = new ObservableCollection<AppItem>(_allApps);
    }

    partial void OnSearchTextChanged(string value)
    {
        FilterApps();
    }

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

        IsInstalling = true;
        
        foreach (var app in selected)
        {
            ProgressText = $"Installing {app.Name}...";
            var result = await _packageManager.InstallWithWinGetAsync(app.WinGetId);
            
            if (!result.IsSuccess && !string.IsNullOrEmpty(app.ChocoId))
            {
                ProgressText = $"WinGet failed for {app.Name}, trying Chocolatey...";
                await _packageManager.InstallWithChocoAsync(app.ChocoId);
            }
        }

        ProgressText = "Installation complete!";
        IsInstalling = false;
    }
}
