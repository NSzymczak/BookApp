namespace GooglBookApiLib.Models;

public class Offer
{
    public int FinskyOfferType { get; set; }
    public ListPrice? ListPrice { get; set; }
    public RetailPrice? RetailPrice { get; set; }
    public bool Giftable { get; set; }
    public RentalDuration? RentalDuration { get; set; }
}