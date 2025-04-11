using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Domain.Commons.Response;

public class GetAllOrders
{
    public string Id { get; set; }
    public string Price { get; set; }
    public string Status { get; set; }
}
