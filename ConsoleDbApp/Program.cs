using ConsoleDbApp.Contexts;
using ConsoleDbApp.Services;

using var context = new FilmsDbContext();

var filmService = new FilmService(context);

//var sortedFilms = await filmService.GetSortedFilmsAsync("ReleaseYear");
//sortedFilms.ForEach(f => Console.WriteLine($"{f.Name} - {f.ReleaseYear}"));

var sessionService = new SessionService(context);

//Console.WriteLine(await sessionService.IncreaseSessionPriceByHallIdAsync(11,250m));

var genres = await filmService.GetFilmGenresByIdAsync(1);
genres.ForEach(Console.WriteLine);

Console.WriteLine();

var films = await filmService.GetFilmsByLetterRangeAsync('а', 'д');
films.ForEach(f => Console.WriteLine($"{f.Name} - {f.FilmId}"));

Console.WriteLine();

var sessionDto = await sessionService.GetPriceInfoByFilmIdAsync(1);
Console.WriteLine($"Max - {sessionDto.MaxPrice} | Min - {sessionDto.MinPrice} | Average - {sessionDto.AveragePrice}");

var sessionTime = await sessionService.GetSessionDateAndTimeByTicketId(1);
Console.WriteLine(sessionTime.ToString());