using ForeningenFumle.Server.Repositories.RegistrationRepository;
using ForeningenFumle.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ForeningenFumle.Server.Controllers
{
	[Route("api/registrationapi")]
	[ApiController]
	public class RegistrationController : ControllerBase
	{
		private readonly IRegistrationRepository repository = new RegistrationRepositoryEF();

		public RegistrationController(IRegistrationRepository registrationRepository)
		{
			if (repository == null && registrationRepository != null)
			{
				repository = registrationRepository;
				Console.WriteLine("Repository initialized");
			}
		}

		[HttpGet]
		public IEnumerable<Registration> GetAllRegistrations()
		{
			return repository.GetAllRegistrations();
		}

		[HttpDelete("{id:int}")]
		public StatusCodeResult DeleteRegistration(int id)
		{
			Console.WriteLine("Server: Delete registration called: id = " + id);

			bool deleted = repository.DeleteRegistration(id);
			if (deleted)
			{
				Console.WriteLine("Server: Registration deleted succces");
				int code = (int)HttpStatusCode.OK;
				return new StatusCodeResult(code);
			}
			else
			{
				Console.WriteLine("Server: Registration deleted fail - not found");
				int code = (int)HttpStatusCode.NotFound;
				return new StatusCodeResult(code);
			}
		}

		[HttpPost]
		public void AddRegistration(Registration registration)
		{
			Console.WriteLine("Add registration called: " + registration.ToString());
			repository.AddRegistration(registration);
		}

		[HttpGet("{id:int}")]
		public Registration FindRegistration(int id)
		{
			var result = repository.FindRegistration(id);
			return result;
		}

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
	}
}
