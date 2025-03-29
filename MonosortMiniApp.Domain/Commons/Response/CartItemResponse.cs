using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Domain.Commons.Response;

public class CartItemResponse
{
    public int Id { get; set; }
    public int DrinkId { get; set; }
    public int VolumeId { get; set; }
    public int SiropId { get; set; }
    public int SugarCount { get; set; }
    public int MilkId { get; set; }
    public bool ExtraShot { get; set; }
    public int Price { get; set; }
}
