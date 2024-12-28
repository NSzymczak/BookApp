using CommunityToolkit.Maui.Views;

namespace BookApp.Pages.AddOpinion;

public partial class AddOpinionPage : ContentPage
{
    public AddOpinionPage(AddOpinionPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }
}