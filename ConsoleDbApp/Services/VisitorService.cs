using ConsoleDbApp.Contexts;
using ConsoleDbApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleDbApp.Services
{
    public class VisitorService(FilmsDbContext context)
    {
        private readonly FilmsDbContext _context = context;

        public async Task<Visitor> GetVisitorByIdAsync(int id)
        => await _context.Visitors.FirstOrDefaultAsync(v => v.VisitorId == id);

        public async Task<List<Visitor>> GetVisitorsAsync()
        => await _context.Visitors.ToListAsync();
        public async Task AddVisitorAsync(Visitor visitor)
        {
            await _context.Visitors.AddAsync(visitor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateVisitorAsync(Visitor visitor)
        {
            _context.Visitors.Update(visitor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVisitorAsync(int id)
        {
            var visitor = await _context.Visitors.FindAsync(id);
            if (visitor is not null)
            {
                _context.Visitors.Remove(visitor);
                await _context.SaveChangesAsync();
            }
        }
    }
}
