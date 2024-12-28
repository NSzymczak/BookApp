using BookApp.Database;
using BookApp.Pages.AddOpinion;
using BookApp.Pages.BookDetails;
using BookApp.Pages.Opinion;
using BookApp.Pages.Search;
using BookApp.Pages.Shelf;
using BookApp.Services.Navigation;
using BookApp.Services.Search;
using BookApp.Services.Shelf;
using CommunityToolkit.Maui;
using GooglBookApiLib;
using Microsoft.Extensions.Logging;

namespace BookApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddHttpClient("rideTo Login", client =>
        {
            client.BaseAddress = new Uri("https://www.googleapis.com/books/v1/volumes?");
        });

        //ApiClients
        builder.Services.AddSingleton<IGoogleBooksApiClient, GoogleBooksApiClient>();

        //Services
        builder.Services.AddSingleton<BookContext>();
        builder.Services.AddSingleton<ISearchService, SearchService>();
        builder.Services.AddSingleton<IShelfService, ShelfService>();
        builder.Services.AddSingleton<INavigationService, NavigationService>();

        //Pages
        builder.Services.AddTransient<SearchPage, SearchPageModel>();
        builder.Services.AddTransient<ShelfPage, ShelfPageModel>();
        builder.Services.AddTransient<BookDetailsPage, BookDetailsPageModel>();
        builder.Services.AddTransient<OpinionPage, OpinionPageModel>();
        builder.Services.AddTransient<AddOpinionPage, AddOpinionPageModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        var app = builder.Build();
        ServiceHelper.Initialize(app.Services);

        return app;
    }
}