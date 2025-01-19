using MonosortMiniApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Domain.Commons.Request;

public class OrderRequest
{
    public int UserId { get; set; }
    public int WaitingTime { get; set; }
    public int SummaryPrice { get; set; }
    public string? Status { get; set; }
    public List<PositionModel> Positions { get; set; }
}
