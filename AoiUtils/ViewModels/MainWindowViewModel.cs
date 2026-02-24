using System.Threading.Tasks;
using AoiUtils.Core.Services;
using AoiUtils.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia;
using Avalonia.Styling;

namespace AoiUtils.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly PackageManagerService _packageManagerService;
    private readonly SettingsService _settingsService;
    
    [ObservableProperty]
    private LocalizationManager _localizer;

    [ObservableProperty]
    private ViewModelBase _currentPage;

    [ObservableProperty]
    private string _winGetStatus = "...";

    [ObservableProperty]
    private string _chocoStatus = "...";

    private readonly InstallViewModel _installViewModel;
    private readonly DebloatViewModel _debloatViewModel;
    private readonly TweaksViewModel _tweaksViewModel;

    public MainWindowViewModel(
        PackageManagerService packageManagerService, 
        SettingsService settingsService,
        LocalizationManager localizer,
        InstallViewModel installViewModel,
        DebloatViewModel debloatViewModel,
        TweaksViewModel tweaksViewModel)
    {
        _packageManagerService = packageManagerService;
        _settingsService = settingsService;
        _localizer = localizer;
        _installViewModel = installViewModel;
        _debloatViewModel = debloatViewModel;
        _tweaksViewModel = tweaksViewModel;
        
        _currentPage = this; // Default to Dashboard (self)
        _ = CheckStatusAsync();
    }

    [RelayCommand]
    private void NavigateToDashboard() => CurrentPage = this;

    [RelayCommand]
    private void NavigateToInstall() => CurrentPage = _installViewModel;

    [RelayCommand]
    private void NavigateToDebloat() => CurrentPage = _debloatViewModel;

    [RelayCommand]
    private void NavigateToTweaks() => CurrentPage = _tweaksViewModel;

    [RelayCommand]
    private async Task CheckStatusAsync()
    {
        WinGetStatus = await _packageManagerService.IsWinGetInstalledAsync() ? "Installed" : "Not Found";
        ChocoStatus = await _packageManagerService.IsChocolateyInstalledAsync() ? "Installed" : "Not Found";
    }

    [RelayCommand]
    private void ToggleTheme()
    {
        var app = Application.Current;
        if (app != null)
        {
            app.RequestedThemeVariant = app.RequestedThemeVariant == ThemeVariant.Dark 
                ? ThemeVariant.Light 
                : ThemeVariant.Dark;
        }
    }

    [RelayCommand]
    private void ToggleLanguage()
    {
        if (_settingsService.CurrentLanguage == AppLanguage.English)
        {
            _settingsService.CurrentLanguage = AppLanguage.Vietnamese;
            Localizer.SetLanguage("vi-VN");
        }
        else
        {
            _settingsService.CurrentLanguage = AppLanguage.English;
            Localizer.SetLanguage("en-US");
        }
    }
}
