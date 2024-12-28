using AsyncAwaitBestPractices.MVVM;
using BookApp.Models;
using BookApp.Pages.Opinion;
using BookApp.Services.Navigation;
using BookApp.Services.Shelf;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BookApp.Pages.Shelf;

public class ShelfPageModel(
    IShelfService shelfService,
    INavigationService navigationService) : ObservableObject
{
    private ObservableCollection<Book> _readBooks = [];

    public ObservableCollection<Book> ReadBooks
    {
        get => _readBooks;
        set => SetProperty(ref _readBooks, value);
    }

    public ICommand LoadCommand => new AsyncCommand(async () =>
    {
        var readBooks = await shelfService.GetReadedBooks();
        if (readBooks is null)
        {
            return;
        }
        ReadBooks = new ObservableCollection<Book>(readBooks);
    });

    public ICommand OpenOpinionsCommand => new AsyncCommand<Book>(async (book) =>
    {
        if (book is null)
        {
            return;
        }
        var parameters = new Dictionary<string, object>
        {
            { NavigationConstants.SelectedBook, book }
        };

        await navigationService.NavigateToAsync(nameof(OpinionPage), parameters);
    });
}