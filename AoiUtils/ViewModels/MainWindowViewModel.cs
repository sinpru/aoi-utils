using System.Threading.Tasks;
using AoiUtils.Core.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AoiUtils.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly PackageManagerService _packageManagerService;

    [ObservableProperty]
    private string _winGetStatus = "Checking...";

    [ObservableProperty]
    private string _chocoStatus = "Checking...";

    public MainWindowViewModel(PackageManagerService packageManagerService)
    {
        _packageManagerService = packageManagerService;
        _ = CheckStatusAsync();
    }

    [RelayCommand]
    private async Task CheckStatusAsync()
    {
        WinGetStatus = await _packageManagerService.IsWinGetInstalledAsync() ? "Installed" : "Not Found";
        ChocoStatus = await _packageManagerService.IsChocolateyInstalledAsync() ? "Installed" : "Not Found";
    }
}
