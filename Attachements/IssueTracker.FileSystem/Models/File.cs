namespace FileSystem.Models;
public class File
{
    public string Id 
    { get; set; } = string.Empty;

    public string? Name 
    { get; set; }

    public string? BlobName 
    { get; set; }

    public string Extension 
    { get; set; } = string.Empty;

    public Stream? Content 
    { get; set; }

    public string? Link 
    { get; set; }

    public string Type 
    { get; set; } = string.Empty;

    public double SizeKb 
    { get; set; }

    public int UserId 
    { get; set; }

    public File()
    {

    }
    public File(string id)
    {
        Id = id;
    }
    public File(string id, string extension, string name, string type, double sizeKb)
    {
        Id = id;
        Name = name;
        Extension = extension;
        Type = type;
        SizeKb = sizeKb;
    }  
}
