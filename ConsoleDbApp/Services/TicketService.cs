using ConsoleDbApp.Contexts;
using ConsoleDbApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleDbApp.Services
{
    public class TicketService(FilmsDbContext context)
    {
        private readonly FilmsDbContext _context = context;
    }
}
