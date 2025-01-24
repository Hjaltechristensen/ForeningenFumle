using ForeningenFumle.Shared.Models;

namespace ForeningenFumle.Server.Repositories.EventRepository
{
	public interface IEventRepository
	{
		List<Event> GetAllEvents();
		Event FindEvent(int id);
		void AddEvent(Event @event);
		bool DeleteEvent(int id);
		bool UpdateEvent(Event @event);
	}
}
