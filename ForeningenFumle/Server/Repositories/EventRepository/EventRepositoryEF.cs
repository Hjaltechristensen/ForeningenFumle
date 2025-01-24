using ForeningenFumle.Server.DataAccess;
using ForeningenFumle.Shared.Models;

namespace ForeningenFumle.Server.Repositories.EventRepository
{
	public class EventRepositoryEF : IEventRepository
	{
		public List<Event> GetAllEvents()
		{
			var db = new FumleDbContext();
			List<Event> events;
			try
			{
				// Brug OfType<Member>() for at hente kun Members
				events = db.Events.ToList();
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
				// Brug using for korrekt oprydning af DbContext
				using (var db = new FumleDbContext())
				{
					// Tilføj medlemmet til Persons (da Member arver fra Person)
					db.Events.Add(@event);
					db.SaveChanges(); // Gem ændringer i databasen
				}
			}
			catch (Exception ex)
			{
				// Log eller håndter fejlen
				Console.WriteLine($"Fejl ved tilføjelse af admin: {ex.Message}");
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
