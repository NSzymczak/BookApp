using AsyncAwaitBestPractices.MVVM;
using BookApp.Models;
using BookApp.Pages.BookDetails;
using BookApp.Services.Navigation;
using BookApp.Services.Search;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BookApp.Pages.Search;

public class SearchPageModel(ISearchService searchService, INavigationService navigationService) : ObservableObject
{
    private string _searchText = string.Empty;
    private bool _isBusy = false;
    private ObservableCollection<Book> _books;

    public string SearchText
    {
        get => _searchText;
        set => SetProperty(ref _searchText, value);
    }

    public ObservableCollection<Book> Books
    {
        get => _books;
        set => SetProperty(ref _books, value);
    }

    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }

    public ICommand SearchCommand => new AsyncCommand(async () =>
    {
        if (string.IsNullOrEmpty(SearchText))
        {
            return;
        }
        IsBusy = true;
        var list = await searchService.SearchForBook(SearchText);
        if (list is not null)
        {
            Books = new ObservableCollection<Book>(list);
        }
        IsBusy = false;
    });

    public ICommand BookDetails => new AsyncCommand<Book>(async (book) =>
    {
        if (book is null)
        {
            return;
        }
        var parameters = new Dictionary<string, object> { { NavigationConstants.SelectedBook, book } };
        await navigationService.NavigateToAsync(nameof(BookDetailsPage), parameters);
    });
}