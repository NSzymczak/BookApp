using BookApp.Converter;
using BookApp.Database;
using BookApp.Models;

namespace BookApp.Services.Shelf;

public class ShelfService(BookContext bookContext) : IShelfService
{
    /// <summary>
    /// Get all readed books from database
    /// </summary>
    /// <returns>List of books</returns>
    public async Task<List<Book>> GetReadedBooks()
    {
        var dbBooks = await bookContext.GetReadedBook();
        if (dbBooks is null)
        {
            return [];
        }
        return dbBooks.Select(BooksConverter.ConvertToBooks).ToList();
    }

    /// <summary>
    /// Get all opinions about book from database
    /// </summary>
    /// <param name="isbn"> Book ISBN</param>
    /// <returns>List of books</returns>
    public async Task<List<Opinion>> GetOpinionsAboutBook(string isbn)
    {
        var opinions = await bookContext.GetOpinions(isbn);
        if (opinions is null)
        {
            return [];
        }
        return opinions.Select(OpinionConverter.ConvertToOpinion).ToList();
    }

    /// <summary>
    /// Add book to database table
    /// </summary>
    /// <param name="opinion"> Opinion about book</param>
    /// <param name="book"></param>
    /// <returns></returns>
    public async Task AddBookToRead(Opinion opinion, Book book)
    {
        var dbBook = BooksConverter.ConvertToDbBook(book);
        var dbOpinion = OpinionConverter.ConvertToDbOpinion(opinion);
        if (dbBook is null || dbOpinion is null)
        {
            return;
        }

        await bookContext.AddBookToReaded(dbBook, dbOpinion);
    }

    /// <summary>
    /// Delate book with given isbn from readed books
    /// </summary>
    /// <param name="isbn">Book ISBN</param>
    /// <returns></returns>
    public async Task DeleteReadedBook(string isbn)
    {
        if (string.IsNullOrEmpty(isbn))
        {
            return;
        }

        await bookContext.DeleteReadedBook(isbn);
    }

    /// <summary>
    /// Check if book with given isbn is in database
    /// </summary>
    /// <param name="isbn">Book ISBN</param>
    /// <returns></returns>
    public async Task<bool> IsBookReaded(string isbn)
    {
        if (string.IsNullOrEmpty(isbn))
        {
            return false;
        }

        return await bookContext.IsReaded(isbn);
    }

    /// <summary>
    /// Delete opinion with given id
    /// </summary>
    /// <param name="id">Opinion ID's</param>
    /// <returns></returns>
    public async Task DeleteOpinion(int id)
    {
        await bookContext.DeleteOpinion(id);
    }

    /// <summary>
    /// Add opinion about book to database
    /// </summary>
    /// <param name="opinion">New opinions</param>
    /// <param name="book"></param>
    /// <returns></returns>
    public async Task AddOpinion(Opinion opinion, Book book)
    {
        var dbOpinion = OpinionConverter.ConvertToDbOpinion(opinion);
        var dbBook = BooksConverter.ConvertToDbBook(book);

        if (dbOpinion is null || dbBook is null)
        {
            return;
        }

        await bookContext.AddOpinion(dbOpinion, dbBook);
    }
}