namespace BookApp.Pages.Opinion;

public partial class OpinionPage : ContentPage
{
    public OpinionPage(OpinionPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }
}