using ConsoleDbApp.Contexts;
using ConsoleDbApp.DTOs;
using ConsoleDbApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace ConsoleDbApp.Services
{
    public class SessionService(FilmsDbContext context)
    {
        private readonly FilmsDbContext _context = context;

        public async Task<int> IncreaseSessionPriceByHallIdAsync(int hallId, decimal price)
            => await _context.Database
                .ExecuteSqlAsync($"UPDATE Session SET Price += {price} WHERE HallId = {hallId} ");

        public async Task<SessionPriceDto> GetPriceInfoByFilmIdAsync(int id)
        {
            var selectedPrices = _context.Sessions.Where(s => s.FilmId == id).Select(s => s.Price);

            var minPrice = await selectedPrices.MinAsync();
            var maxPrice = await selectedPrices.MaxAsync();
            var averagePrice = await selectedPrices.AverageAsync();

            return new SessionPriceDto(minPrice, maxPrice, averagePrice);
        }

        public async Task<DateTime> GetSessionDateAndTimeByTicketIdAsync(int id)
            => await _context.Database
                .SqlQuery<DateTime>(@$"SELECT Session.StartDate AS value
                FROM Ticket INNER JOIN 
                Session ON Ticket.SessionId = Session.SessionId 
                WHERE Ticket.TicketId = {id}")
                .FirstOrDefaultAsync();

        public async Task<List<Session>> GetSessionsByFilmIdAsync(int id)
        => await _context.Sessions
            .FromSql($"SELECT * FROM dbo.GetSessionsByFilmId({id})")
            .ToListAsync();
    }
}

