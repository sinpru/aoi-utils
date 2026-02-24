namespace AoiUtils.Core.Models;

public record ProcessResult(int ExitCode, string Output, string Error)
{
    public bool IsSuccess => ExitCode == 0;
}
