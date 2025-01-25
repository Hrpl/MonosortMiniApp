using MonosortMiniApp.Domain.Constant;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonosortMiniApp.Domain.Entities;

[Table("Orders", Schema = EntityInformation.Dictionary.Scheme)]
public class Order : BaseEntity
{
    public int UserId { get; set; }
    public User? User { get; set; }  
    public int WaitingTime { get; set; }
    public int SummaryPrice { get; set; }
    public int StatusId {  get; set; }
    public OrderStatus? OrderStatus { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}
