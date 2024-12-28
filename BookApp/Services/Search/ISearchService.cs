using BookApp.Models;

namespace BookApp.Services.Search;

public interface ISearchService
{
    Task<List<Book>?> SearchForBook(string text);
}