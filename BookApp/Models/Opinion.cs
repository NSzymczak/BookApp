namespace BookApp.Models;

public class Opinion
{
    public int Id { get; set; }

    public int Rate { get; set; }

    public DateTime ReadDate { get; set; }

    public string? Comment { get; set; }

    public Book Book { get; set; } = null!;
}