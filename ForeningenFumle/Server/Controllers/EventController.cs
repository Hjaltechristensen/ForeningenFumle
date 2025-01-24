using ForeningenFumle.Client.Services.RegistrationServices;
using ForeningenFumle.Server.DataAccess;
using ForeningenFumle.Server.Repositories.EventRepository;
using ForeningenFumle.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ForeningenFumle.Server.Controllers
{
	[Route("api/eventapi")]
	[ApiController]
	public class EventController : ControllerBase
	{
		private readonly IEventRepository repository = new EventRepositoryEF();
		private FumleDbContext _dbContext;

		public EventController(IEventRepository repository, FumleDbContext dbContext)
		{
			this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
		}

		[HttpGet]
		public IEnumerable<Event> GetAllEvents()
		{
			return repository.GetAllEvents();
		}

		[HttpDelete("{id:int}")]
		public StatusCodeResult DeleteEvent(int id)
		{
			Console.WriteLine("Server: Delete event called: id = " + id);

			bool deleted = repository.DeleteEvent(id);
			if (deleted)
			{
				Console.WriteLine("Server: Event deleted succces");
				int code = (int)HttpStatusCode.OK;
				return new StatusCodeResult(code);
			}
			else
			{
				Console.WriteLine("Server: Event deleted fail - not found");
				int code = (int)HttpStatusCode.NotFound;
				return new StatusCodeResult(code);
			}
		}

		[HttpPost]
		public void AddEvent(Event eEvent)
		{
			if (eEvent == null)
			{
				Console.WriteLine("eEvent is null");
			}
			else
			{
				Console.WriteLine("Received event: " + eEvent.Title + ", " + eEvent.TimeAndDate);
				try
				{
					repository.AddEvent(eEvent);
				}
				catch (Exception ex)
				{
					Console.WriteLine("Error while adding event: " + ex.Message);
					throw;
				}
			}
		}

		[HttpGet("{id:int}")]
		public Event FindEvent(int id)
		{
			var result = repository.FindEvent(id);
			return result;
		}

		[HttpPatch]
		public StatusCodeResult UpdateEvent(Event @event)
		{
			bool updated = repository.UpdateEvent(@event);
			if (updated)
			{
				Console.WriteLine("Server: Event updated succces");
				int code = (int)HttpStatusCode.OK;
				return new StatusCodeResult(code);
			}
			else
			{
				Console.WriteLine("Server: Event updated fail - something went wrong");
				int code = (int)HttpStatusCode.NotFound;
				return new StatusCodeResult(code);
			}
		}
	}
}
