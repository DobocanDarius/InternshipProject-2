using Microsoft.AspNetCore.Http;

namespace RequestResponseModels.Attachement.Request;
public class AttachementRequest
{
    public AttachementRequest(IFormFile formFile, int ticketId)
    {
        FormFile = formFile;
        TicketId = ticketId;
    }

    public IFormFile FormFile { get; set; }
    public int TicketId { get; set; }
}
