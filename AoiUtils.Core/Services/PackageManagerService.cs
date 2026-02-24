using AoiUtils.Core.Models;

namespace AoiUtils.Core.Services;

public class PackageManagerService
{
    private readonly SystemRunner _runner;

    public PackageManagerService(SystemRunner runner)
    {
        _runner = runner;
    }

    public async Task<bool> IsWinGetInstalledAsync()
    {
        try
        {
            var result = await _runner.RunCommandAsync("winget", "--version");
            return result.IsSuccess;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> IsChocolateyInstalledAsync()
    {
        try
        {
            var result = await _runner.RunCommandAsync("choco", "--version");
            return result.IsSuccess;
        }
        catch
        {
            return false;
        }
    }
}
