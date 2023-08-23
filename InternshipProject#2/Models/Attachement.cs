using System;
using System.Collections.Generic;

namespace InternshipProject_2.Models;

public partial class Attachement
{
    public int Id { get; set; }

    public int TicketId { get; set; }

    public string AttachementName { get; set; } = null!;

    public byte[] Attachements { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;
}
