using SQLite;

namespace BookApp.Database.Models;

[Table("Opinion")]
public class DbOpinion
{
    [PrimaryKey]
    [AutoIncrement]
    [Column("Id")]
    public int Id { get; set; }

    [Column("Rate")]
    public int Rate { get; set; }

    [Column("ReadDate")]
    public DateTime ReadDate { get; set; }

    [Column("Comment")]
    public string? Comment { get; set; }

    [Column("BookId")]
    public int BookId { get; set; }

    [Ignore]
    public DbBook Book { get; set; } = null!;
}