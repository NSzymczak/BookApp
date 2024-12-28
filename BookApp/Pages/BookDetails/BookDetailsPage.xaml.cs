namespace BookApp.Pages.BookDetails;

public partial class BookDetailsPage : ContentPage
{
    public BookDetailsPage(BookDetailsPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }
}