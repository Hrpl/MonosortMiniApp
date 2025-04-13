using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Domain.Models;

public class GetAllOrdersModel
{
    public int OrderId { get; set; }
    public int SummaryPrice { get; set; }
    public string Status { get; set; }
    public int WaitingTime { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? CreatedTime { get; set; }
    public string? ReadyTime { get; set; }
}
