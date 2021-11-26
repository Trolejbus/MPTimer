using Microsoft.EntityFrameworkCore;
using MPSourceControl.Entities;
using MPSourceControl.Interfaces;

namespace MPSourceControl
{
    internal class SourceControlRepository : ISourceControlRepository
    {
        private readonly SourceControlContext _context;

        public SourceControlRepository(SourceControlContext context)
        {
            _context = context;
        }

        public async Task<SourceControl> GetById(Guid id)
        {
            var model = await _context.SourceControl.FirstAsync(t => t.Id == id);
            return model;
        }

        public async Task<IEnumerable<SourceControl>> GetAll()
        {
            var SourceControl = await _context.SourceControl
                .Include(s => s.Statuses!.Where(s => s.From > DateTime.UtcNow.Date && (s.To == null || s.To < DateTime.UtcNow.Date.AddDays(1))))
                .ToListAsync();
            return SourceControl;
        }

        public async Task<SourceControl> Add(SourceControl task)
        {
            var entity = _context.SourceControl.Add(task);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<SourceControl> Update(Guid id, SourceControl model)
        {
            var entity = await _context.SourceControl.FirstAsync(t => t.Id == id);
            _context.Entry(entity).CurrentValues.SetValues(model);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Remove(Guid id)
        {
            var model = await _context.SourceControl.FirstAsync(t => t.Id == id);
            _context.SourceControl.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<SourceControl> ChangeBranch(Guid id, string branchName)
        {
            var model = await _context.SourceControl
                .Include(s => s.Statuses)
                .FirstAsync(t => t.Id == id);
            foreach (var status in model.Statuses!)
            {
                status.To ??= DateTime.UtcNow;
            }

            var newStatus = new SourceControlStatus(Guid.NewGuid(), branchName, DateTime.UtcNow, id);
            _context.Status.Add(newStatus);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task TerminateAllSourceControls(Guid agentIdGuid)
        {
            var models = await _context.SourceControl
                .Where(s => s.AgentId == agentIdGuid)
                .Include(s => s.Statuses)
                .ToListAsync();
            foreach (var status in models.SelectMany(s => s.Statuses!))
            {
                status.To ??= DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
        }
    }
}
