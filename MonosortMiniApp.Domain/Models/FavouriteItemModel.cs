using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Domain.Models;

public class FavouriteItemModel
{
    public int UserId { get; set; }
    public string Photo { get; set; }
    public int DrinkId { get; set; }
    public int VolumeId { get; set; }
    public int SugarCount { get; set; }
    public int SiropId { get; set; }
    public int ExtraShot { get; set; }
    public int MilkId { get; set; }
    public int Sprinkling { get; set; }
    public int Price { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    public bool? IsDeleted { get; set; } = false;
}
