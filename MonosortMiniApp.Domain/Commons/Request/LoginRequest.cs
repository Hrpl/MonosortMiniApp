﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Domain.Commons.Request;

public class LoginRequest
{
    public string Login { get; set; }
    public string Password { get; set; }
}
