using ConsoleDbApp.Contexts;
using ConsoleDbApp.Services;

using var context = new FilmsDbContext();

var filmService = new FilmService(context);

//var sortedFilms = await filmService.GetSortedFilmsAsync("ReleaseYear");
//sortedFilms.ForEach(f => Console.WriteLine($"{f.Name} - {f.ReleaseYear}"));

var sessionService = new SessionService(context);

Console.WriteLine(await sessionService.IncreaseSessionPriceByHallIdAsync(11,250m));

