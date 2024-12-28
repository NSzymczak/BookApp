using BookApp.Models;

namespace BookApp.Services.Shelf;

public interface IShelfService
{
    Task<List<Book>> GetReadedBooks();

    Task AddBookToRead(Opinion opinion, Book book);

    Task<bool> IsBookReaded(string isbn);

    Task<List<Opinion>> GetOpinionsAboutBook(string isbn);

    Task DeleteReadedBook(string isbn);

    Task DeleteOpinion(int id);

    Task AddOpinion(Opinion opinion, Book book);
}