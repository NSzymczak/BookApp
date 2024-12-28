using BookApp.Fake;
using BookApp.Services.Search;
using GooglBookApiLib;

namespace BookAppTest.Service;

public class SearchServiceTest
{
    public IGoogleBooksApiClient googleBooksApiClient = null!;

    [Fact]
    public async Task SearchForExistingBook()
    {
        // Arrange
        googleBooksApiClient = new FakeGoogleBookApiClient();
        var searchService = new SearchService(googleBooksApiClient);

        // Act
        var list = await searchService.SearchForBook("Book");

        // Assert
        Assert.NotNull(list);
        Assert.NotEmpty(list);
        Assert.True(list.Count == 1);
        var book = list[0];
        Assert.Equal("Book 1", book.Title);
        Assert.Equal("Author 1, Author 2, Author 3", book.AuthorsName);
        Assert.Equal("2021", book.PublishedOn.Date.Year.ToString());
        Assert.Equal("1", book.PublishedOn.Date.Month.ToString());
        Assert.Equal("1", book.PublishedOn.Date.Day.ToString());
    }
}