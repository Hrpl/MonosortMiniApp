using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Domain.Commons.Request;

public class CreateUserRequest
{
    public string Name { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
}
