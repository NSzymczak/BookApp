using BookApp.Converter;
using BookApp.Models;
using GooglBookApiLib;

namespace BookApp.Services.Search;

public class SearchService(IGoogleBooksApiClient googleBooksApiClient) : ISearchService
{
    /// <summary>
    /// Search for books from google database using google api
    /// </summary>
    /// <param name="text">Text entered by user</param>
    /// <returns>List of books</returns>
    public async Task<List<Book>?> SearchForBook(string text)
    {
        var volumeInfos = await googleBooksApiClient.SearchBooksAsync(text);

        if (volumeInfos == null)
        {
            return null;
        }

        return volumeInfos.Select(BooksConverter.ConvertToBooks).ToList();
    }
}