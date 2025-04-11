using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Domain.Models;

public class ConnectionModel
{
    public string ConnectionId { get; set; }
    public int UserId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdateAt { get; set; }
    public bool? IsDeleted { get; set; }
}
