using System;
using System.Collections.Generic;

namespace InternshipProject_2.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Role { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Assignee> Assignees { get; set; } = new List<Assignee>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<History> Histories { get; set; } = new List<History>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual ICollection<Watcher> Watchers { get; set; } = new List<Watcher>();
}
