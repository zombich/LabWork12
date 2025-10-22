using ConsoleDbApp.Contexts;
using ConsoleDbApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleDbApp.Services
{
    public class TicketService(FilmsDbContext context)
    {
        private readonly FilmsDbContext _context = context;

        public async Task<List<Ticket>> GetTicketsAsync()
            => await _context.Tickets.ToListAsync();

        public async Task<Ticket> GetTicketByIdAsync(int id)
        => await _context.Tickets.FirstOrDefaultAsync(t => t.TicketId == id);

        public async Task AddTicketAsync(Ticket ticket)
        {
            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTicketAsync(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTicketAsync(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket is not null)
            {
                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync();
            }
        }
    }
}
