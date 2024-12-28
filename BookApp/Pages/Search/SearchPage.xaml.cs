namespace BookApp.Pages.Search;

public partial class SearchPage : ContentPage
{
    public SearchPage(SearchPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }
}