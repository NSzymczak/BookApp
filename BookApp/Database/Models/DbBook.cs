using SQLite;

namespace BookApp.Database.Models;

[Table("Book")]
public class DbBook
{
    [PrimaryKey]
    [AutoIncrement]
    [Column("id")]
    public int Id { get; set; }

    [Column("isbn")]
    public string Isbn { get; set; } = null!;

    [Column("Title")]
    public string? Title { get; set; }

    [Column("Authors")]
    public string Authors { get; set; } = null!;

    [Column("Description")]
    public string? Description { get; set; }

    [Column("PublishOn")]
    public DateTime PublishOn { get; set; }

    [Column("Image")]
    public byte[]? Image { get; set; }
}