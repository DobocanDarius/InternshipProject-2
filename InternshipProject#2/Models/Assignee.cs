namespace InternshipProject_2.Models;

public partial class Assignee
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int TicketId { get; set; }

    public virtual Ticket Ticket { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
