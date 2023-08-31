using System;
using System.Collections.Generic;

namespace InternshipProject_2.Models;

public partial class Watcher
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? TicketId { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Ticket? Ticket { get; set; }

    public virtual User? User { get; set; }
}
