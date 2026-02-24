namespace AoiUtils.Core.Models;

public record AppItemDto(
    string Id, 
    string Name, 
    string Description, 
    string Category, 
    string WinGetId = "", 
    string ChocoId = "",
    string Website = "");
