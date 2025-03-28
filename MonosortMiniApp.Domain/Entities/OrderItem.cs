using MonosortMiniApp.Domain.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Domain.Entities;

[Table("OrderItems", Schema = EntityInformation.Dictionary.Scheme)]
public class OrderItem : BaseEntity
{
    public int OrderId { get; set; }
    public int DrinkId { get; set; }
    public int VolumeId { get; set; }
    public int SugarCount { get; set; }
    public int MilkId { get; set; }
    public bool ExtraShot { get; set; }
    public int Price { get; set; }
}
