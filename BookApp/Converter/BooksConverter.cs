using BookApp.Database.Models;
using BookApp.Models;
using GooglBookApiLib.Models;
using Type = GooglBookApiLib.Models.Type;

namespace BookApp.Converter;

public static class BooksConverter
{
    public static Book ConvertToBooks(VolumeInfo volumeInfo)
    {
        var isbn = volumeInfo.IndustryIdentifiers.FirstOrDefault(x => x.Type == Type.ISBN_13)?.Identifier;
        isbn ??= volumeInfo.IndustryIdentifiers.FirstOrDefault(x => x.Type == Type.Other)?.Identifier;
        var book = new Book(isbn)
        {
            Title = volumeInfo.Title,
            Description = volumeInfo.Description,
            PublishedOn = new DateTime(),
            CoverImageUrl = volumeInfo.ImageLinks?.Thumbnail,
        };

        if (DateTime.TryParse(volumeInfo.PublishedDate, out DateTime date))
        {
            book.PublishedOn = date;
        }
        var authors = new List<Author>();
        foreach (var author in volumeInfo.Authors)
        {
            if (string.IsNullOrEmpty(author))
            {
                continue;
            }
            authors.Add(ConvertAuthor(author));
        }

        book.Authors = authors;

        return book;
    }

    public static Book ConvertToBooks(DbBook dbBook)
    {
        return new Book(dbBook.Isbn)
        {
            Description = dbBook.Description,
            Authors = dbBook.Authors.Split(',').Select(x => ConvertAuthor(x)).ToList(),
            Title = dbBook.Title,
            PublishedOn = dbBook.PublishOn,
            CoverImage = dbBook.Image,
        };
    }

    public static DbBook ConvertToDbBook(Book book)
    {
        return new DbBook()
        {
            Isbn = book.ISBN,
            Authors = book.AuthorsName,
            Description = book.Description,
            PublishOn = book.PublishedOn,
            Title = book.Title,
            Image = book.CoverImage,
        };
    }

    public static Author ConvertAuthor(string? author)
    {
        return new Author
        {
            Name = author,
        };
    }
}