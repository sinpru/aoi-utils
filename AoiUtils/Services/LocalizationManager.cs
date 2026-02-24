using System.ComponentModel;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AoiUtils.Services;

public class LocalizationManager : INotifyPropertyChanged
{
    private readonly ResourceManager _resourceManager;
    private CultureInfo _currentCulture = CultureInfo.CurrentUICulture;

    public event PropertyChangedEventHandler? PropertyChanged;

    public LocalizationManager()
    {
        // Use the full name of the resource file (Namespace.Folder.BaseName)
        _resourceManager = new ResourceManager("AoiUtils.Resources.Resources", Assembly.GetExecutingAssembly());
    }

    public string this[string key] => _resourceManager.GetString(key, _currentCulture) ?? $"[{key}]";

    public void SetLanguage(string languageCode)
    {
        _currentCulture = new CultureInfo(languageCode);
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null)); // Refresh all bindings
    }
}
