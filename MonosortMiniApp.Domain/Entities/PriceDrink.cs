namespace MonosortMiniApp.Domain.Entities;

public class PriceDrink : BaseEntity
{
    public int DrinkId { get; set; }
    public int VolumeId { get; set; }
    public int Price { get; set; }
}
