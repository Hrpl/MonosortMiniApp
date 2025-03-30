using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Domain.Models;

public class VolumePriceModel
{
    public int VolumeId { get; set; }
    public string Name { get; set; }
    public string Size { get; set; }
    public int Price { get; set; }
}
