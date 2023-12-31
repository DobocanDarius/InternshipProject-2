﻿namespace InternshipProject_2.Models;

public partial class Attachement
{
    public int Id { get; set; }

    public int TicketId { get; set; }

    public string Name { get; set; } = null!;

    public string Link { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;
}
