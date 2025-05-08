using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Domain.Commons.Request;

public class CreateFavouriteItemRequest
{
    public int DrinkId { get; set; }
    public int VolumeId { get; set; }
    public int SugarCount { get; set; }
    public int SiropId { get; set; }
    public int ExtraShot { get; set; }
    public int MilkId { get; set; }
    public int Sprinkling { get; set; }
    public int Price { get; set; }
}
