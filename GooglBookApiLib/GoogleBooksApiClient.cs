using GooglBookApiLib.Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GooglBookApiLib;

public class GoogleBooksApiClient(HttpClient httpClient) : IGoogleBooksApiClient
{
    private readonly RestClient restClient = new(
            httpClient: httpClient,
            disposeHttpClient: true,
            configureSerialization: s => s.UseSystemTextJson(new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
            }),
            configureRestClient: c => c.ThrowOnDeserializationError = true);

    public const string ApiKey = "xxx";

    public async Task<List<VolumeInfo>?> SearchBooksAsync(string searchText)
    {
        string requestUrl = $"https://www.googleapis.com/books/v1/volumes?q={searchText}&key={ApiKey}";

        var response = await restClient.GetAsync(new RestRequest(requestUrl));
        if (response != null && response.IsSuccessful && response.Content != null)
        {
            var result = JsonConvert.DeserializeObject<Root>(response.Content) ?? throw new Exception("Something goes wrong");
            var itemsWithVolumes = result.Items.Where(x => x.VolumeInfo is not null);
            var volumes = itemsWithVolumes.Select(x => x.VolumeInfo!).ToList();
            if (volumes is not null)
            {
                return volumes;
            }
        }
        return null;
    }
}