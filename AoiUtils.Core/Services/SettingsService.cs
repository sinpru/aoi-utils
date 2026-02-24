namespace AoiUtils.Core.Services;

public enum AppTheme
{
    Dark,
    Light
}

public enum AppLanguage
{
    English,
    Vietnamese
}

public class SettingsService
{
    // Default to Light and Vietnamese
    public AppTheme CurrentTheme { get; set; } = AppTheme.Light;
    public AppLanguage CurrentLanguage { get; set; } = AppLanguage.Vietnamese;

    public string GetLanguageCode() => CurrentLanguage switch
    {
        AppLanguage.Vietnamese => "vi-VN",
        _ => "en-US"
    };
}
