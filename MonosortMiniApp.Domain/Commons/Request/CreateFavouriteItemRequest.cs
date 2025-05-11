using MonosortMiniApp.Domain.Commons.DTO;
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
    public int Price { get; set; }
    public string Photo {  get; set; }
    public AdditivesDTO Additives { get; set; }
}
