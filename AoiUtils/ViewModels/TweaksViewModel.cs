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
    
    [ObservableProperty]
    private LocalizationManager _localizer;

    [ObservableProperty]
    private ObservableCollection<Tweak> _tweaks;

    [ObservableProperty]
    private bool _isRunning;

    [ObservableProperty]
    private string _statusText = "";

    public TweaksViewModel(TweakService tweakService, LocalizationManager localizer)
    {
        _tweakService = tweakService;
        _localizer = localizer;
        _tweaks = new ObservableCollection<Tweak>(_tweakService.GetTweaks());
    }

    [RelayCommand]
    private async Task RunSelectedTweaksAsync()
    {
        var selected = Tweaks.Where(t => t.IsSelected).ToList();
        if (!selected.Any()) return;

        IsRunning = true;
        
        foreach (var tweak in selected)
        {
            StatusText = string.Format(Localizer["Applying"], tweak.Name);
            await _tweakService.RunTweakAsync(tweak);
        }

        StatusText = Localizer["TweaksApplied"];
        IsRunning = false;
    }
}
