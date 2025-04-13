using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Domain.Commons.Response;

public class LastOrderDTO
{
    public int Number { get; set; }
    public string Status { get; set; }
    public int Price { get; set; }
    public string? ReadyTime { get; set; }
}
