using MonosortMiniApp.Domain.Commons.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Domain.Commons.Response;

public class OrderDescriptionResponse
{
    public int OrderId { get; set; }
    public int WaitingTime { get; set; }
    public string Status { get; set; }
    public int SummaryPrice { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedTime { get; set; }
    public IEnumerable<OrderItemDescriptionDTO> OrderItems { get; set; }
}
