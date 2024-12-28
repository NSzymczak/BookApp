using GooglBookApiLib.Models;

namespace GooglBookApiLib;

public interface IGoogleBooksApiClient
{
    Task<List<VolumeInfo>?> SearchBooksAsync(string searchText);
}