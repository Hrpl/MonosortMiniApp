using MonosortMiniApp.Domain.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Domain.Entities;

[Table("SiropsPosition", Schema = EntityInformation.Dictionary.Scheme)]
public class SiropPosition : BaseEntity
{
    public int SiropId { get; set; }
    public int OrderItemId { get; set; }
    public OrderItem? OrderItem {  get; set; } 
}
