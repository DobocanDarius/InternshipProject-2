﻿using InternshipProject_2.Helpers;
using System;
using System.Collections.Generic;

namespace InternshipProject_2.Models;

public partial class Ticket
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Body { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Priority { get; set; } = null!;

    public string Component { get; set; } = null!;

    public int ReporterId { get; set; }
    public TicketLifecycle Status { get; set; }
    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public byte[]? Attachements { get; set; }

    public virtual ICollection<Assignee> Assignees { get; set; } = new List<Assignee>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<History> Histories { get; set; } = new List<History>();

    public virtual User Reporter { get; set; } = null!;

    public virtual ICollection<TicketLifeCycle> TicketLifeCycles { get; set; } = new List<TicketLifeCycle>();

    public virtual ICollection<Watcher> Watchers { get; set; } = new List<Watcher>();
}
