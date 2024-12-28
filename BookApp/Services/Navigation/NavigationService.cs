namespace BookApp.Services.Navigation;

public class NavigationService : INavigationService
{
    public Task NavigateToAsync(string route, IDictionary<string, object>? routeParameters = null)
    {
        return routeParameters is not null
            ? Shell.Current.GoToAsync(route, routeParameters)
            : Shell.Current.GoToAsync(route);
    }

    public Task PopAsync() => Shell.Current.GoToAsync("..");

    public Task PopToRootAync() => Shell.Current.Navigation.PopToRootAsync();
}