
﻿using RequestResponseModels.User.Request;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RequestResponseModels.User.Response;

public class LoginResponse
{
    public string? Token { get; set; }
    public IEnumerable<LoginRequest> Users { get; set; }
    public LoginResponse()
    {
        Users = new List<LoginRequest>();
        Token = string.Empty;
    }
}

