using Microsoft.AspNetCore.Http;

namespace RequestResponseModels.Attachement.Request;
public class AttachementRequest
{
    public IFormFile FormFile
    { get; set; }

    public int TicketId
    { get; set; }

    public AttachementRequest(IFormFile formFile, int ticketId)
    {
        FormFile = formFile;
        TicketId = ticketId;
    }
}
