using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form.Domain.Options;

public class SmtpOptions
{
    public const string Key = "SmtpClient";

    public string Name { get; set; }
    public string Email { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
    public string Password { get; set; }
}
