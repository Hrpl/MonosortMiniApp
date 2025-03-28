using MonosortMiniApp.Domain.Constant;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonosortMiniApp.Domain.Entities;
[Table("PriceDrink", Schema = EntityInformation.Dictionary.Scheme)]
public class PriceDrink : BaseEntity
{
    public int DrinkId { get; set; }
    public int VolumeId { get; set; }
    public int Price { get; set; }
}
