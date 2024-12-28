using BookApp.Converter;
using BookApp.Database.Models;
using BookApp.Models;
using GooglBookApiLib.Models;

namespace BookAppTest.Conversion;

public class BooksConverterTests
{
    [Fact]
    public void ConvertToBooks_FromVolumeInfo_ShouldMapCorrectly()
    {
        // Arrange
        var volumeInfo = new VolumeInfo
        {
            Title = "Test Title",
            Description = "Test Description",
            PublishedDate = "2023-12-25",
            IndustryIdentifiers =
                [
                    new IndustryIdentifier { Type = GooglBookApiLib.Models.Type.ISBN_13, Identifier = "1234567890123" }
                ],
            ImageLinks = new ImageLinks { Thumbnail = "http://example.com/thumbnail.jpg" },
            Authors = ["Author One", "Author Two"]
        };

        // Act
        var book = BooksConverter.ConvertToBooks(volumeInfo);

        // Assert
        Assert.Equal("1234567890123", book.ISBN);
        Assert.Equal("Test Title", book.Title);
        Assert.Equal("Test Description", book.Description);
        Assert.Equal(new DateTime(2023, 12, 25), book.PublishedOn);
        Assert.Equal("http://example.com/thumbnail.jpg", book.CoverImageUrl);
        Assert.Equal(2, book.Authors.Count);
        Assert.Equal("Author One", book.Authors[0].Name);
        Assert.Equal("Author Two", book.Authors[1].Name);
    }

    [Fact]
    public void ConvertToBooks_FromDbBook_ShouldMapCorrectly()
    {
        // Arrange
        var dbBook = new DbBook
        {
            Isbn = "9876543210987",
            Title = "Database Title",
            Description = "Database Description",
            PublishOn = new DateTime(2022, 5, 15),
            Authors = "Author A,Author B"
        };

        // Act
        var book = BooksConverter.ConvertToBooks(dbBook);

        // Assert
        Assert.Equal("9876543210987", book.ISBN);
        Assert.Equal("Database Title", book.Title);
        Assert.Equal("Database Description", book.Description);
        Assert.Equal(new DateTime(2022, 5, 15), book.PublishedOn);
        Assert.Equal(2, book.Authors.Count);
        Assert.Equal("Author A", book.Authors[0].Name);
        Assert.Equal("Author B", book.Authors[1].Name);
    }

    [Fact]
    public void ConvertToDbBook_FromBook_ShouldMapCorrectly()
    {
        // Arrange
        var book = new Book("5678901234567")
        {
            Title = "Book Title",
            Description = "Book Description",
            PublishedOn = new DateTime(2021, 10, 10),
            CoverImageUrl = "http://example.com/bookimage.jpg",
            Authors =
            [
                new Author { Name = "Author X" },
                new Author { Name = "Author Y" }
            ]
        };

        // Act
        var dbBook = BooksConverter.ConvertToDbBook(book);

        // Assert
        Assert.Equal("5678901234567", dbBook.Isbn);
        Assert.Equal("Book Title", dbBook.Title);
        Assert.Equal("Book Description", dbBook.Description);
        Assert.Equal(new DateTime(2021, 10, 10), dbBook.PublishOn);
        Assert.Equal("Author X, Author Y", dbBook.Authors);
    }

    [Fact]
    public void ConvertAuthor_ShouldReturnAuthorWithCorrectName()
    {
        // Arrange
        var authorName = "Test Author";

        // Act
        var author = BooksConverter.ConvertAuthor(authorName);

        // Assert
        Assert.Equal(authorName, author.Name);
    }

    [Fact]
    public void ConvertToBooks_FromVolumeInfo_WithEmptyAuthors_ShouldSkipNullOrEmptyAuthors()
    {
        // Arrange
        var volumeInfo = new VolumeInfo
        {
            Authors = ["Author One", null, "", "Author Two"]
        };

        // Act
        var book = BooksConverter.ConvertToBooks(volumeInfo);

        // Assert
        Assert.Equal(2, book.Authors.Count);
        Assert.Equal("Author One", book.Authors[0].Name);
        Assert.Equal("Author Two", book.Authors[1].Name);
    }
}