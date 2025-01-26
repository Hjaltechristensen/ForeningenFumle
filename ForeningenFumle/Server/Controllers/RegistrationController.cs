using ForeningenFumle.Server.DataAccess;
using ForeningenFumle.Server.Repositories.EventRepository;
using ForeningenFumle.Server.Repositories.RegistrationRepository;
using ForeningenFumle.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ForeningenFumle.Server.Controllers
{
	[Route("api/registrationapi")]
	[ApiController]
	public class RegistrationController : ControllerBase
	{
		private readonly IRegistrationRepository repository = new RegistrationRepositoryEF();

		private FumleDbContext _dbContext;

		public RegistrationController(IRegistrationRepository repository, FumleDbContext dbContext)
		{
			this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
		}

		[HttpGet]
		public IEnumerable<Registration> GetAllRegistrations()
		{
			return repository.GetAllRegistrations();
		}

		[HttpGet("{personId}")]
		public List<Registration> GetRegistrationsByPersonId(int personId)
		{
			return repository.GetRegistrationsByPersonId(personId);
		}

		[HttpDelete("{eventId}/{personId}")]
		public async Task<IActionResult> RemoveRegistration(int eventId, int personId)
		{
			var registration = await _dbContext.Registrations
				.FirstOrDefaultAsync(r => r.EventId == eventId && r.PersonId == personId);

			if (registration == null)
			{
				return NotFound("Registration not found.");
			}

			_dbContext.Registrations.Remove(registration);
			await _dbContext.SaveChangesAsync();
			return NoContent();
		}

		[HttpPost]
		public async Task<IActionResult> AddRegistration([FromBody] Registration registration)
		{
			if (await _dbContext.Registrations
				.AnyAsync(r => r.EventId == registration.EventId && r.PersonId == registration.PersonId))
			{
				return Conflict("User is already registered for this event.");
			}

			_dbContext.Registrations.Add(registration);
			await _dbContext.SaveChangesAsync();
			return CreatedAtAction(nameof(IsUserRegisteredForEvent), new { eventId = registration.EventId, personId = registration.PersonId }, registration);
		}


		//[HttpGet("{id:int}")]
		//public Registration FindRegistration(int id)
		//{
		//	var result = repository.FindRegistration(id);
		//	return result;
		//}

		[HttpPatch]
		public StatusCodeResult UpdateRegistration(Registration registration)
		{
			bool updated = repository.UpdateRegistration(registration);
			if (updated)
			{
				Console.WriteLine("Server: Registration updated succces");
				int code = (int)HttpStatusCode.OK;
				return new StatusCodeResult(code);
			}
			else
			{
				Console.WriteLine("Server: Registration updated fail - something went wrong");
				int code = (int)HttpStatusCode.NotFound;
				return new StatusCodeResult(code);
			}
		}

		[HttpGet("{eventId}/{personId}")]
		public async Task<IActionResult> IsUserRegisteredForEvent(int eventId, int personId)
		{
			bool isRegistered = await _dbContext.Registrations.AnyAsync(r => r.EventId == eventId && r.PersonId == personId);

			return Ok(isRegistered);
		}
	}
}
