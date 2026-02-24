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

    public async Task<ProcessResult> InstallChocolateyAsync()
    {
        string script = "Set-ExecutionPolicy Bypass -Scope Process -Force; [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072; iex ((New-Object System.Net.WebClient).DownloadString('https://community.chocolatey.org/install.ps1'))";
        return await _runner.RunCommandAsync("powershell.exe", $"-Command \"{script}\"");
    }

    public async Task<ProcessResult> InstallWithWinGetAsync(string packageId)
    {
        return await _runner.RunCommandAsync("winget", $"install --id {packageId} --silent --accept-package-agreements --accept-source-agreements");
    }

    public async Task<ProcessResult> InstallWithChocoAsync(string packageId)
    {
        return await _runner.RunCommandAsync("choco", $"install {packageId} -y");
    }

    public async Task<ProcessResult> UninstallWithWinGetAsync(string packageId)
    {
        return await _runner.RunCommandAsync("winget", $"uninstall --id {packageId} --silent");
    }

    public async Task<ProcessResult> UninstallWithChocoAsync(string packageId)
    {
        return await _runner.RunCommandAsync("choco", $"uninstall {packageId} -y");
    }
}
