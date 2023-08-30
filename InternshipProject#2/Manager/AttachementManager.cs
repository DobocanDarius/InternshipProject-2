using InternshipProject_2.Models;

namespace InternshipProject_2.Manager
{
    public class AttachementManager : IAttachementManager
    {
        private readonly Project2Context _context;

        public AttachementManager(Project2Context context)
        {
            _context = context;
        }
        public async Task AddAsync(Attachement entity)
        {
            _context.Attachements.Add(entity);
            await _context.SaveChangesAsync();
        }
    }
}
