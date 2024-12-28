namespace GooglBookApiLib.Models;

public class ListPrice
{
    public double Amount { get; set; }
    public string? CurrencyCode { get; set; }
    public int AmountInMicros { get; set; }
}