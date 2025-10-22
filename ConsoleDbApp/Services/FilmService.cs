using ConsoleDbApp.Contexts;
using ConsoleDbApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ConsoleDbApp.Services
{
    public class FilmService(FilmsDbContext context)
    {
        private readonly FilmsDbContext _context = context;

        public async Task<List<Film>> GetSortedFilmsAsync(string column) 
            => await _context.Films
                .FromSqlRaw($"SELECT * FROM Film ORDER BY {column}")
                .ToListAsync();

        public async Task<List<Film>> GetFilmsByYearAndNameAsync(string name, int year) 
            => await _context.Films
                .FromSql($"SELECT * FROM Film WHERE Name = {name} AND ReleaseYear >= {year}")
                .ToListAsync();

        public async Task<List<string>> GetFilmGenresById(int id)
        {
            var a = await _context.Database.SqlQuery(@$"SELECT Genre.[Name]
                                                FROM FilmGenre INNER JOIN
                                                Genre ON FilmGenre.GenreId = Genre.GenreId
                                                GROUP BY Genre.[Name], FilmGenre.FilmId
                                                HAVING (FilmGenre.FilmId = {id})").ToListAsync();
        }
    }
}
