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
    private readonly BackupService _backupService;
    
    [ObservableProperty]
    private LocalizationManager _localizer;

    [ObservableProperty]
    private ViewModelBase _currentPage;

    [ObservableProperty]
    private string _winGetStatus = "...";

    [ObservableProperty]
    private string _chocoStatus = "...";

    [ObservableProperty]
    private bool _isGlobalBusy;

    [ObservableProperty]
    private string _dashboardStatusText = "";

    private readonly InstallViewModel _installViewModel;
    private readonly DebloatViewModel _debloatViewModel;
    private readonly TweaksViewModel _tweaksViewModel;

    public MainWindowViewModel(
        PackageManagerService packageManagerService, 
        SettingsService settingsService,
        LocalizationManager localizer,
        InstallViewModel installViewModel,
        DebloatViewModel debloatViewModel,
        TweaksViewModel tweaksViewModel,
        BackupService backupService)
    {
        _packageManagerService = packageManagerService;
        _settingsService = settingsService;
        _localizer = localizer;
        _installViewModel = installViewModel;
        _debloatViewModel = debloatViewModel;
        _tweaksViewModel = tweaksViewModel;
        _backupService = backupService;

        // Sync busy states
        _installViewModel.PropertyChanged += (s, e) => { if (e.PropertyName == nameof(InstallViewModel.IsBusy)) UpdateGlobalBusy(); };
        _debloatViewModel.PropertyChanged += (s, e) => { if (e.PropertyName == nameof(DebloatViewModel.IsBusy)) UpdateGlobalBusy(); };
        _tweaksViewModel.PropertyChanged += (s, e) => { if (e.PropertyName == nameof(TweaksViewModel.IsBusy)) UpdateGlobalBusy(); };
        
        _currentPage = this; // Default to Dashboard (self)
        _ = CheckStatusAsync();
    }

    private void UpdateGlobalBusy()
    {
        IsGlobalBusy = _installViewModel.IsBusy || _debloatViewModel.IsBusy || _tweaksViewModel.IsBusy;
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
    private async Task CreateRestorePointAsync()
    {
        IsGlobalBusy = true;
        DashboardStatusText = Localizer["CreatingRestorePoint"];
        
        var result = await _backupService.CreateRestorePointAsync("AoiUtils Auto-Backup");
        
        DashboardStatusText = result.IsSuccess 
            ? Localizer["RestorePointCreated"] 
            : Localizer["RestorePointFailed"];
            
        IsGlobalBusy = false;
    }

    [RelayCommand]
    private async Task RestoreSystemAsync()
    {
        await _backupService.OpenSystemRestoreUIAsync();
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
