using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AoiUtils.Models;

public partial class AppItem : ObservableObject
{
    public string Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public string Category { get; init; }
    public string WinGetId { get; init; }
    public string ChocoId { get; init; }
    public string Website { get; init; }

    [ObservableProperty]
    private bool _isSelected;

    [ObservableProperty]
    private Bitmap? _iconBitmap;

    [ObservableProperty]
    private bool _isIconLoaded;

    public AppItem(string id, string name, string description, string category, string winGetId = "", string chocoId = "", string website = "")
    {
        Id = id;
        Name = name;
        Description = description;
        Category = category;
        WinGetId = winGetId;
        ChocoId = chocoId;
        Website = website;
    }

    public string DisplayLetter => Name.StartsWith(".") && Name.Length > 1 
        ? Name[1].ToString().ToUpper() 
        : Name[0].ToString().ToUpper();

    public string IconUrl => !string.IsNullOrEmpty(Website) 
        ? $"https://www.google.com/s2/favicons?sz=64&domain={Website}" 
        : "";
}
