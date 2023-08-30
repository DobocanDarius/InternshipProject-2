namespace FileSystem.Repository.Interface;
public interface IFileProvider
{
    Task UploadAsync(Models.File file);
    Task<IEnumerable<Models.File>> GetAsync(IEnumerable<Models.File> files);
    Task<bool> DeleteAsync(Models.File file);
}
