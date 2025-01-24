using ForeningenFumle.Shared.Models;

namespace ForeningenFumle.Client.Services.EventServices
{
	public interface IEventService
	{
		Task<Event[]?> GetAllEvent();

		Task<Event?> GetEvent(int id);

		Task<int> AddEvent(Event @event);

		Task<int> DeleteEvent(int id);

		Task<int> UpdateEvent(Event @event);
	}
}
