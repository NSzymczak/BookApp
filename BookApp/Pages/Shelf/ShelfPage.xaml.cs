namespace BookApp.Pages.Shelf;

public partial class ShelfPage : ContentPage
{
    public ShelfPage(ShelfPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }
}