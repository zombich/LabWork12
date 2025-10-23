using ConsoleDbApp.Contexts;
using ConsoleDbApp.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<string>> GetFilmGenresByIdAsync(int id)
            => await _context.Database
                .SqlQuery<string>(@$"SELECT Genre.[Name]
                FROM FilmGenre INNER JOIN
                Genre ON FilmGenre.GenreId = Genre.GenreId
                WHERE FilmGenre.FilmId = {id}")
                .ToListAsync();

        public async Task<List<Film>> GetFilmsByLetterRangeAsync(char startRange, char endRange)
            => await context.Films
                .Where(f => EF.Functions.Like(f.Name, $"[{startRange}-{endRange}]%"))
                .ToListAsync();
    }
}
