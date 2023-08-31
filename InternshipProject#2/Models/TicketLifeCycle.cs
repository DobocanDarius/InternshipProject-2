using System;
using System.Collections.Generic;

namespace InternshipProject_2.Models;

public partial class TicketLifeCycle
{
    public int Id { get; set; }

    public int? TicketId { get; set; }

    public bool? ToDo { get; set; }

    public bool Assigned { get; set; }

    public bool Solving { get; set; }

    public bool CodeReview { get; set; }

    public bool TestingDev { get; set; }

    public bool TestingUat { get; set; }

    public bool Closed { get; set; }

    public virtual Ticket? Ticket { get; set; }
}
