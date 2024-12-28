namespace BookApp.Converter;

using System;
using System.Net;

public class ImageConverter
{
    public static byte[] DownloadImageAsByteArray(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("URL nie może być pusty", nameof(url));

        using (WebClient webClient = new WebClient())
        {
            try
            {
                return webClient.DownloadData(url);
            }
            catch (WebException e)
            {
                throw new Exception($"Błąd podczas pobierania obrazu z URL: {url}", e);
            }
        }
    }
}