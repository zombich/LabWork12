using ConsoleDbApp.Contexts;
using ConsoleDbApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleDbApp.Services
{
    public class SessionService(FilmsDbContext context)
    {
        private readonly FilmsDbContext _context = context;

        public async Task<int> IncreaseSessionPriceByHallIdAsync(int hallId, decimal price)
        {
            return await _context.Database
                .ExecuteSqlAsync($"UPDATE Session SET Price += {price} WHERE HallId = {hallId} ");
        }
    }
}

