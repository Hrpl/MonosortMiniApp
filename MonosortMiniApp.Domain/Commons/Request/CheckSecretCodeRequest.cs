using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Domain.Commons.Request;

public class CheckSecretCodeRequest
{
    public string PhoneNumber { get; set; }
    public string Code { get; set; }
}
