using AsyncAwaitBestPractices.MVVM;
using BookApp.Models;
using BookApp.Services.Navigation;
using BookApp.Services.Shelf;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace BookApp.Pages.AddOpinion;

public class AddOpinionPageModel(
    IShelfService shelfService,
    INavigationService navigationService) : ObservableObject, IQueryAttributable
{
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query is null)
        {
            return;
        }

        if (query.TryGetValue(NavigationConstants.SelectedBook, out object? value) && value is Book book)
        {
            Book = book;
        }
        query.Clear();
    }

    private DateTime _dateOfReading = DateTime.Now;
    private int _rate;
    private string _comment = string.Empty;
    private Book? _book;

    public Book? Book
    {
        get => _book;
        set => SetProperty(ref _book, value);
    }

    public DateTime DateOfReading
    {
        get => _dateOfReading;
        set => SetProperty(ref _dateOfReading, value);
    }

    public int Rate
    {
        get => _rate;
        set => SetProperty(ref _rate, value);
    }

    public string Comment
    {
        get => _comment;
        set => SetProperty(ref _comment, value);
    }

    public ICommand AddOpinionCommand => new AsyncCommand(async () =>
    {
        if (Book is null)
        {
            return;
        }

        var opinion = new Models.Opinion()
        {
            ReadDate = DateOfReading,
            Rate = Rate,
            Comment = Comment
        };

        await shelfService.AddOpinion(opinion, Book);
        await navigationService.PopToRootAync();
    });

    public ICommand BackCommand => new Command(() =>
    {
        navigationService.PopToRootAync();
    });
}