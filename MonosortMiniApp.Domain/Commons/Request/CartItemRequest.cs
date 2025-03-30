using MonosortMiniApp.Domain.Commons.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Domain.Commons.Request;

public class CartItemRequest
{
    public int DrinkId { get; set; }
    public int VolumeId { get; set; }
    public int Price { get; set; }
    public AdditivesDTO Additives { get; set; }
}
