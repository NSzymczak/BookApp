using AsyncAwaitBestPractices;
using BookApp.Converter;
using BookApp.Models;
using BookApp.Pages.AddOpinion;
using BookApp.Pages.Opinion;
using BookApp.Services.Navigation;
using BookApp.Services.Shelf;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace BookApp.Pages.BookDetails;

public class BookDetailsPageModel(
    IShelfService shelfService,
    INavigationService navigationService
    ) : ObservableObject, IQueryAttributable
{
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query is null)
        {
            return;
        }

        if (query.TryGetValue(NavigationConstants.SelectedBook, out object? value) && value is Book book)
        {
            if (book.CoverImageUrl is not null)
            {
                book.CoverImage = ImageConverter.DownloadImageAsByteArray(book.CoverImageUrl);
            }
            Book = book;

            CheckIfReaded().SafeFireAndForget();
        }
        query.Clear();
    }

    private Book? _book;
    private bool _isBookReaded = false;

    public Book? Book
    {
        get => _book;
        set => SetProperty(ref _book, value);
    }

    public bool IsBookReaded
    {
        get => _isBookReaded;
        set => SetProperty(ref _isBookReaded, value);
    }

    public ICommand CheckReadedCommand => new Command(async () =>
    {
        if (Book is null)
        {
            return;
        }
        var parameters = new Dictionary<string, object> { { NavigationConstants.SelectedBook, Book } };

        if (IsBookReaded)
        {
            await navigationService.NavigateToAsync(nameof(OpinionPage), parameters);
        }
        else
        {
            await navigationService.NavigateToAsync(nameof(AddOpinionPage), parameters);
        }
    });

    private async Task CheckIfReaded()
    {
        if (Book is null)
        {
            return;
        }
        IsBookReaded = await shelfService.IsBookReaded(Book.ISBN!);
    }
}