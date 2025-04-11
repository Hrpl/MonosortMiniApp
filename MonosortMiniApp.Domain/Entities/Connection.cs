using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Domain.Entities;

public class Connection : BaseEntity
{
    public string ConnectionId { get; set; }
    public int UserId { get; set; }
}
