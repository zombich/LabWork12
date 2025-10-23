using ConsoleDbApp.Contexts;
using ConsoleDbApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleDbApp.Services
{
    public class TicketService(FilmsDbContext context)
    {
        private readonly FilmsDbContext _context = context;

        public async Task<List<Ticket>> GetTicketsByVisitorPhoneAsync(string phone)
        => await _context.Tickets.FromSql($"dbo.GetTicketsByPhone {phone}")
            .ToListAsync();
    }
}
