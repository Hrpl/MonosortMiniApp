using MonosortMiniApp.Domain.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Domain.Entities;

[Table("Drinks", Schema = EntityInformation.Dictionary.Scheme)]
public class Drink : BaseEntity
{
    public int MenuId { get; set; }
    public Menu? Menu { get; set; }
    public string Name { get; set; }
    public bool IsExistence { get; set; } = true;
    public string Photo {  get; set; }
    public List<PriceDrink> PriceDrinks { get; set; }
    public List<OrderItem> OrderItems { get; set; }
    public List<CartItem> CartItems { get; set; }
}
