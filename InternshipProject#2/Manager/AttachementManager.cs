using InternshipProject_2.Models;

namespace InternshipProject_2.Manager;

public class AttachementManager : IAttachementManager
{
    readonly Project2Context _DbContext;

    public AttachementManager(Project2Context dbContext)
    {
        _DbContext = dbContext;
    }
    public async Task AddAsync(Attachement entity)
    {
        _DbContext.Attachements.Add(entity);
        await _DbContext.SaveChangesAsync();
    }
}
