
using MonosortMiniApp.Domain.Constant;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonosortMiniApp.Domain.Entities;

[Table("OrderStatus", Schema = EntityInformation.Dictionary.Scheme)]
public class OrderStatus : BaseEntity
{
    public string Name { get; set; }
    public List<Order> Orders { get; set; }
}
