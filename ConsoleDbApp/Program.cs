using ConsoleDbApp.Contexts;
using ConsoleDbApp.Services;

using var context = new FilmsDbContext();

var filmService = new FilmService(context);

//var sortedFilms = await filmService.GetSortedFilmsAsync("ReleaseYear");
//sortedFilms.ForEach(f => Console.WriteLine($"{f.Name} - {f.ReleaseYear}"));

var sessionService = new SessionService(context);

//Console.WriteLine(await sessionService.IncreaseSessionPriceByHallIdAsync(11,250m));

//var genres = await filmService.GetFilmGenresByIdAsync(1);
//genres.ForEach(Console.WriteLine);

//Console.WriteLine();

//var films = await filmService.GetFilmsByLetterRangeAsync('а', 'д');
//films.ForEach(f => Console.WriteLine($"{f.Name} - {f.FilmId}"));

//Console.WriteLine();

//var sessionDto = await sessionService.GetPriceInfoByFilmIdAsync(1);
//Console.WriteLine($"Max - {sessionDto.MaxPrice} | Min - {sessionDto.MinPrice} | Average - {sessionDto.AveragePrice}");

//var sessionTime = await sessionService.GetSessionDateAndTimeByTicketId(1);
//Console.WriteLine(sessionTime.ToString());

var ticketService = new TicketService(context);

//var tickets = await ticketService.GetTicketsByVisitorPhoneAsync("7892113445");
//tickets.ForEach(ticket => Console.WriteLine($"Id - {ticket.TicketId} | Seat - {ticket.Seat} | VisitorId - {ticket.VisitorId}"));

var visitorService = new VisitorService(context);

//Console.WriteLine($"Id - {await visitorService.AddVisitorByPhoneAsync("1457123000")}");

var sessions = await sessionService.GetSessionsByFilmIdAsync(1);
sessions.ForEach(session => Console.WriteLine(@$"FilmId - {session.FilmId} | Price - {session.Price} | SessionId - {session.SessionId} | StartDate - {session.StartDate}"));