namespace BookApp;

public static class ServiceHelper
{
    public static IServiceProvider ServiceProvider { get; private set; }

    public static void Initialize(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    public static T GetService<T>()
    {
        try
        {
            var service = ServiceProvider.GetService(typeof(T));
            if (service is T)
            {
                return (T)service;
            }
            else
            {
                throw new InvalidCastException($"Service of type {typeof(T)} not found");
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}