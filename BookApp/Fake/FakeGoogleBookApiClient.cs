using GooglBookApiLib;
using GooglBookApiLib.Models;
using Type = GooglBookApiLib.Models.Type;

namespace BookApp.Fake;

public class FakeGoogleBookApiClient : IGoogleBooksApiClient
{
    public async Task<List<VolumeInfo>?> SearchBooksAsync(string searchText)
    {
        await Task.Delay(1000);
        return
        [
            new()
                {
                    Title = "Book 1",
                    Authors = ["Author 1","Author 2","Author 3",],
                    Description = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of \"de Finibus Bonorum et Malorum\" (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, \"Lorem ipsum dolor sit amet..\", comes from a line in section 1.10.32.\r\n\r\nThe standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from \"de Finibus Bonorum et Malorum\" by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.",
                    PublishedDate = "2021-01-01",
                    IndustryIdentifiers=
                    [
                        new()
                        {
                            Type = Type.ISBN_13,
                            Identifier = "978-3-16-148410-0"
                        }
                    ],
                    ImageLinks = new ImageLinks()
                    {
                        Thumbnail = "https://picsum.photos/200/300"
                    }
            },
        ];
    }
}