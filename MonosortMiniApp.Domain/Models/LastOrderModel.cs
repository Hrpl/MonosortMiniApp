using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Domain.Models;

public class LastOrderModel
{
    public int Number { get; set; }
    public string Status { get; set; }
    public int Price { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int WaitingTime { get; set; }
}
