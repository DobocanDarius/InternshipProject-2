using System;
using System.Collections.Generic;

namespace InternshipProject_2.Models;

public partial class InactiveToken
{
    public int Id { get; set; }

    public string Token { get; set; } = null!;
}
