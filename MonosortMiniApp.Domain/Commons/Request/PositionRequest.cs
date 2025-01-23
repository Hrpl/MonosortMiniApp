using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Domain.Commons.Request;

public class PositionRequest
{
    public int DrinkId { get; set; }
    public int VolumeId { get; set; }
    public int SugarCount { get; set; }
    public int MilkId { get; set; }
    public int[] Sirops { get; set; }
    public bool ExtraShot { get; set; }
    public int Price { get; set; }
}
