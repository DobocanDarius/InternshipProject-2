
namespace RequestResponseModels.Attachement.Response;

public class AttachementResponse
{
    public int Id 
    { get; set; }

    public int TicketId 
    { get; set; }

    public string Name 
    { get; set; }

    public string Link 
    { get; set; }

    public AttachementResponse()
    {
        Name = string.Empty;
        Link = string.Empty;
    }
}
