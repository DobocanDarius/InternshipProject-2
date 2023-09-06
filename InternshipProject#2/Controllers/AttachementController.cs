using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using FileSystem;
using FileSystem.Repository.Interface;

using InternshipProject_2.Manager;
using InternshipProject_2.Models;

using RequestResponseModels.Attachement.Request;

namespace InternshipProject_2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AttachementController : ControllerBase
{
    readonly IFileProvider _FileProvider;
    readonly IAttachementManager _Attachement;
    public AttachementController(IFileProvider fileProvider, ITicketManager ticket, IAttachementManager attachement)
    {
        _FileProvider = fileProvider;
        _Attachement = attachement;
    }

    [HttpPost("/attachement/post/{ticketId}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IResult> UploadFile([FromForm(Name = "file")] IFormFile formFile,int ticketId)
    {
        System.Security.Claims.Claim? idclaim = User.Claims.FirstOrDefault(x => x.Type.Equals("userId"));
        if (idclaim == null) return Results.Unauthorized();
        AttachementRequest fileRequest = new AttachementRequest(formFile, ticketId);
        string blobFileName = $"{formFile.FileName}";
        FileSystem.Models.File file = new FileSystem.Models.File(ticketId.ToString())
        {
            Name = formFile.FileName,
            BlobName = blobFileName,
            Content = formFile.OpenReadStream(),
            SizeKb = formFile.Length / 1024,
            Type = formFile.ContentType,
            UserId = Convert.ToInt32(idclaim.Value),
            Extension = Path.GetExtension(formFile.FileName) 
        };
        Attachement fileModel = new Attachement
        {
            TicketId = ticketId,
            Name = formFile.FileName,
            Link = "http://127.0.0.1:10000/devstoreaccount1/azureblobstorage/" + formFile.FileName
        };
        await _FileProvider.UploadAsync(file);
        try
        {
            await _Attachement.AddAsync(fileModel);
            return Results.Ok();
        }
        catch (FileSystemException ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

}

