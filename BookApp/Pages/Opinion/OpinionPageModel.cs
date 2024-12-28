using AsyncAwaitBestPractices;
using AsyncAwaitBestPractices.MVVM;
using BookApp.Models;
using BookApp.Pages.AddOpinion;
using BookApp.Resources.Language;
using BookApp.Services.Navigation;
using BookApp.Services.Shelf;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BookApp.Pages.Opinion;

public class OpinionPageModel(IShelfService shelfService, INavigationService navigationService) : ObservableObject, IQueryAttributable
{
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query is null)
        {
            return;
        }

        if (query.TryGetValue(NavigationConstants.SelectedBook, out object? value) && value is Book book)
        {
            this.book = book;
            GetOpinionsAboutBook(book.ISBN).SafeFireAndForget();
        }
        query.Clear();
    }

    private Book book = null!;
    private ObservableCollection<Models.Opinion> _opinions = [];

    public ObservableCollection<Models.Opinion> Opinions
    {
        get => _opinions;
        set => SetProperty(ref _opinions, value);
    }

    public async Task GetOpinionsAboutBook(string isbn)
    {
        var list = await shelfService.GetOpinionsAboutBook(isbn);
        Opinions = new ObservableCollection<Models.Opinion>(list);
    }

    public ICommand DeleteOpinionCommand => new AsyncCommand<Models.Opinion>(async (opinion) =>
    {
        if (opinion is null)
        {
            return;
        }
        if (await Shell.Current.DisplayAlert(AppResource.Delete, AppResource.WantToDelete, AppResource.Yes, AppResource.No))
        {
            await shelfService.DeleteOpinion(opinion.Id);
            await GetOpinionsAboutBook(book.ISBN);
        }
    });

    public ICommand AddOpinionCommand => new AsyncCommand(async () =>
    {
        var parameters = new Dictionary<string, object> { { NavigationConstants.SelectedBook, book } };
        await navigationService.NavigateToAsync(nameof(AddOpinionPage), parameters);
    });

    public ICommand BackCommand => new Command(() =>
    {
        navigationService.PopToRootAync();
    });
}