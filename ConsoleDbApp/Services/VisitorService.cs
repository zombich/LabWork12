using ConsoleDbApp.Contexts;
using ConsoleDbApp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ConsoleDbApp.Services
{
    public class VisitorService(FilmsDbContext context)
    {
        private readonly FilmsDbContext _context = context;

        public async Task<int> AddVisitorByPhoneAsync(string phone)
        {
            var id = new SqlParameter("@id", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output,
            };
            await _context.Database.ExecuteSqlRawAsync($"dbo.AddVisitor {phone}, @id OUTPUT", id);
            return (int)id.Value;
}
    }
}
