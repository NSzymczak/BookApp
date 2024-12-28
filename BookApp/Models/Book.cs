namespace BookApp.Models;

public class Book(string isbn)
{
    public int Id { get; set; }
    public string ISBN { get; set; } = isbn;
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime PublishedOn { get; set; }
    public string? CoverImageUrl { get; set; }
    public byte[]? CoverImage { get; set; }
    public List<Author> Authors { get; set; } = [];

    public string AuthorsName => string.Join(", ", Authors.Select(a => a.Name));
}