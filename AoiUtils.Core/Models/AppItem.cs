namespace AoiUtils.Core.Models;

public record AppItem(
    string Id, 
    string Name, 
    string Description, 
    string Category, 
    string WinGetId = "", 
    string ChocoId = "",
    string Website = "")
{
    public bool IsSelected { get; set; }
    
    public string IconUrl => !string.IsNullOrEmpty(Website) 
        ? $"https://www.google.com/s2/favicons?sz=64&domain={Website}" 
        : "";
}
