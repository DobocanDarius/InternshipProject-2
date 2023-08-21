using System;
using System.Collections.Generic;

namespace InternshipProject_2.Models;

public partial class Comment
{
    public int Id { get; set; }

    public string Body { get; set; } = null!;

    public int UserId { get; set; }

    public int TicketId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Ticket Ticket { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
