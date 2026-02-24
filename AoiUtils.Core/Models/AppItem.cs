namespace AoiUtils.Core.Models;

public record AppItem(
    string Id, 
    string Name, 
    string Description, 
    string Category, 
    string WinGetId = "", 
    string ChocoId = "")
{
    public bool IsSelected { get; set; }
}
