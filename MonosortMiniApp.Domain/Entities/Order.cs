using MonosortMiniApp.Domain.Constant;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonosortMiniApp.Domain.Entities;

[Table("Orders", Schema = EntityInformation.Dictionary.Scheme)]
public class Order : BaseEntity
{
    public int UserId { get; set; }
    public int WaitingTime { get; set; }
    public int StatusId { get; set; }
    public int SummaryPrice { get; set; }
    public string Comment { get; set; }
}
