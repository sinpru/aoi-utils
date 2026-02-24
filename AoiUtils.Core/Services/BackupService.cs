using System.Threading.Tasks;
using AoiUtils.Core.Models;

namespace AoiUtils.Core.Services;

public class BackupService
{
    private readonly SystemRunner _runner;

    public BackupService(SystemRunner runner)
    {
        _runner = runner;
    }

    public async Task<ProcessResult> CreateRestorePointAsync(string description)
    {
        // Use a verbatim string to avoid escaping issues
        string script = $@"Checkpoint-Computer -Description ""{description}"" -RestorePointType ""MODIFY_SETTINGS""";
        return await _runner.RunCommandAsync("powershell.exe", $"-Command \"{script}\"");
    }

    public async Task<ProcessResult> OpenSystemRestoreUIAsync()
    {
        return await _runner.RunCommandAsync("rstrui.exe", "");
    }
}
