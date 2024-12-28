namespace GooglBookApiLib.Models;

public class RetailPrice
{
    public double Amount { get; set; }
    public string? CurrencyCode { get; set; }
    public int AmountInMicros { get; set; }
}