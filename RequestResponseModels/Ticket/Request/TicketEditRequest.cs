
namespace RequestResponseModels.Ticket.Request
{
    public class TicketEditRequest
    {
        public string Title { get; set; } = null!;

        public string Body { get; set; } = null!;

        public string Priority { get; set; } = null!;
    }
}