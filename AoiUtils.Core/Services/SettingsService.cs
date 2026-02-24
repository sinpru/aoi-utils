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
    public AppTheme CurrentTheme { get; set; } = AppTheme.Dark;
    public AppLanguage CurrentLanguage { get; set; } = AppLanguage.English;

    public string GetLanguageCode() => CurrentLanguage switch
    {
        AppLanguage.Vietnamese => "vi-VN",
        _ => "en-US"
    };
}
