using BookApp.Pages.AddOpinion;
using BookApp.Pages.BookDetails;
using BookApp.Pages.Opinion;
using BookApp.Pages.Search;
using BookApp.Pages.Shelf;

namespace BookApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            InitializeRouting();
        }

        private static void InitializeRouting()
        {
            Routing.RegisterRoute(nameof(SearchPage), typeof(SearchPage));
            Routing.RegisterRoute(nameof(ShelfPage), typeof(ShelfPage));
            Routing.RegisterRoute(nameof(BookDetailsPage), typeof(BookDetailsPage));
            Routing.RegisterRoute(nameof(OpinionPage), typeof(OpinionPage));
            Routing.RegisterRoute(nameof(AddOpinionPage), typeof(AddOpinionPage));
        }
    }
}