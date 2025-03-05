using ForeningenFumle.Server.DataAccess;
using ForeningenFumle.Server.Repositories.EventRepository;
using ForeningenFumle.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ForeningenFumle.Server.Controllers
{
	[Route("api/eventapi")]
	[ApiController]
	public class EventController : ControllerBase
	{
		private readonly IEventRepository repository;
		private readonly FumleDbContext _dbContext;


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
		public IActionResult AddEvent([FromBody] Event eEvent)
		{
			Console.WriteLine("➡️ AddEvent() blev kaldt!");

			if (eEvent == null)
			{
				Console.WriteLine("❌ Event er null");
				return BadRequest("Event data is null");
			}

			try
			{
				repository.AddEvent(eEvent);
				Console.WriteLine($"✅ Event '{eEvent.Title}' tilføjet til repository!");
				return Ok(new { message = "Event added successfully" });
			}
			catch (Exception ex)
			{
				Console.WriteLine($"❌ Fejl ved tilføjelse af event: {ex.Message}");
				return StatusCode(500, $"An error occurred: {ex.Message}");
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
