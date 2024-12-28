namespace GooglBookApiLib.Models;

public class Root
{
    public string? Kind { get; set; }
    public int TotalItems { get; set; }
    public List<Item> Items { get; set; } = [];
}