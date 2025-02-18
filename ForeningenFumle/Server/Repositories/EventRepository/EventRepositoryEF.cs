using ForeningenFumle.Server.DataAccess;
using ForeningenFumle.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace ForeningenFumle.Server.Repositories.EventRepository
{
	public class EventRepositoryEF : IEventRepository
	{
		private readonly FumleDbContext _dbContext;

		public EventRepositoryEF(FumleDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public List<Event> GetAllEvents()
		{
			var db = new FumleDbContext();
			List<Event> events;
			try
			{
				// Brug OfType<Member>() for at hente kun Members
				events = db.Events.Include(e => e.Registrations).ToList();
			}
			catch
			{
				events = new List<Event>();
			}
			return events;
		}
		public Event FindEvent(int id)
		{
			throw new NotImplementedException();
		}

		public void AddEvent(Event @event)
		{
			try
			{
				_dbContext.Events.Add(@event);
				_dbContext.SaveChanges();
				Console.WriteLine($"Event tilføjet: {@event.Title}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Fejl ved tilføjelse af event: {ex.Message}");
			}
		}
		public bool DeleteEvent(int id)
		{
			throw new NotImplementedException();
		}
		public bool UpdateEvent(Event @event)
		{
			throw new NotImplementedException();
		}
	}
}
