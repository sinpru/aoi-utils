using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AoiUtils.Core.Models;
using AoiUtils.Core.Services;
using AoiUtils.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AoiUtils.ViewModels;

public partial class TweaksViewModel : ViewModelBase
{
    private readonly TweakService _tweakService;
    private readonly List<Tweak> _allTweaks;
    
    [ObservableProperty]
    private LocalizationManager _localizer;

    [ObservableProperty]
    private ObservableCollection<Tweak> _filteredTweaks;

    [ObservableProperty]
    private string _searchText = string.Empty;

    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private string _statusText = "";

    public TweaksViewModel(TweakService tweakService, LocalizationManager localizer)
    {
        _tweakService = tweakService;
        _localizer = localizer;
        _allTweaks = _tweakService.GetTweaks();
        _filteredTweaks = new ObservableCollection<Tweak>(_allTweaks);
    }

    partial void OnSearchTextChanged(string value)
    {
        FilterTweaks();
    }

    private void FilterTweaks()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            FilteredTweaks = new ObservableCollection<Tweak>(_allTweaks);
        }
        else
        {
            var filtered = _allTweaks.Where(t => 
                t.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) || 
                t.Category.ToString().Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                t.Description.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
            
            FilteredTweaks = new ObservableCollection<Tweak>(filtered);
        }
    }

    [RelayCommand]
    private async Task RunSelectedTweaksAsync()
    {
        var selected = _allTweaks.Where(t => t.IsSelected).ToList();
        if (!selected.Any()) return;

        IsBusy = true;
        
        foreach (var tweak in selected)
        {
            StatusText = string.Format(Localizer["Applying"], tweak.Name);
            await _tweakService.RunTweakAsync(tweak);
        }

        StatusText = Localizer["TweaksApplied"];
        IsBusy = false;
    }
}
