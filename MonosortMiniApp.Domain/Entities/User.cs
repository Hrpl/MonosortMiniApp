using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Domain.Entities;

public class User : BaseEntity
{
    public string Login { get; set; }
    public string Password { get; set; }
    public bool IsConfirmed { get; set; }
}
