using FileSystem;
using FileSystem.Repository.Interface;
using RequestResponseModels.Attachement.Response;

namespace InternshipProject_2.Helpers;

public class AttachementsHelper
{
    private readonly IFileProvider _fileProvider;
    public AttachementsHelper(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
    }
    //public async Task<AttachementResponse> GetAttachements(Models.Attachement? file)
    //{
        //var response = new AttachementResponse();
        //if (file != null)
        //{
        //    var fileModel = new FileSystem.Models.File(file.Id);
        //    List<FileSystem.Models.File> files = new List<FileSystem.Models.File>();
        //    files.Add(fileModel);
        //    try
        //    {
        //        var result = (await _fileProvider.GetAsync(files)).FirstOrDefault();
        //        if (result != null)
        //        {
        //            response.Id = result.Id;
        //            response.Name = result.Name!;
        //            response.Link = result.Link!;
        //        }
        //    }
        //    catch (FileSystemException ex)
        //    {
        //        throw ex;
        //    }
        //}
        //return response;
    //}
}
