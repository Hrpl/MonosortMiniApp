using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Domain.Commons.DTO;

public class OrderItemDescriptionDTO
{
    public int OrderId { get; set; }    
    public string DrinkName { get; set; }
    public string VolumeName { get; set; }
    public string Price { get; set; }
    public int SugarCount { get; set; }
    public bool ExtraShot { get; set; }
    public string SiropName { get; set; }
    public string MilkName { get; set; }
    public string Sprinkling { get; set; }
}
