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
    private string _winGetStatus = "...";

    [ObservableProperty]
    private string _chocoStatus = "...";

    public MainWindowViewModel(
        PackageManagerService packageManagerService, 
        SettingsService settingsService,
        LocalizationManager localizer)
    {
        _packageManagerService = packageManagerService;
        _settingsService = settingsService;
        _localizer = localizer;
        
        _ = CheckStatusAsync();
    }

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
