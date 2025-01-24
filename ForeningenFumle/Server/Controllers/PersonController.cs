//using ForeningenFumle.Shared.Models;
//using Microsoft.AspNetCore.Mvc;
//using ForeningenFumle.Server.Repositories.PersonRepository;
//using System.Net;

//namespace ForeningenFumle.Server.Controllers
//{
//	[Route("api/personapi")]
//	[ApiController]
//	public class PersonController : ControllerBase
//	{
//		private readonly IPersonRepository repository = new PersonRepository();

//		public PersonController(IPersonRepository personRepository)
//		{
//			if (repository == null && personRepository != null)
//			{
//				repository = personRepository;
//				Console.WriteLine("Repository initialized");
//			}
//		}

//		[HttpGet]
//		public IEnumerable<Person> GetAllPersons()
//		{
//			return repository.GetAllPersons();
//		}

//		[HttpDelete("{id:int}")]
//		public StatusCodeResult DeletePerson(int id)
//		{
//			Console.WriteLine("Server: Delete person called: id = " + id);

//			bool deleted = repository.DeletePerson(id);
//			if (deleted)
//			{
//				Console.WriteLine("Server: Person deleted succces");
//				int code = (int)HttpStatusCode.OK;
//				return new StatusCodeResult(code);
//			}
//			else
//			{
//				Console.WriteLine("Server: Person deleted fail - not found");
//				int code = (int)HttpStatusCode.NotFound;
//				return new StatusCodeResult(code);
//			}
//		}

//		[HttpPost]
//		public void AddPerson(Person person)
//		{
//			Console.WriteLine("Add person called: " + person.ToString());
//			repository.AddPerson(person);
//		}

//		[HttpGet("{id:int}")]
//		public Person FindPerson(int id)
//		{
//			var result = repository.FindPerson(id);
//			return result;
//		}

//		[HttpPatch]
//		public StatusCodeResult UpdatePerson(Person person)
//		{
//			bool updated = repository.UpdatePerson(person);
//			if (updated)
//			{
//				Console.WriteLine("Server: Person updated succces");
//				int code = (int)HttpStatusCode.OK;
//				return new StatusCodeResult(code);
//			}
//			else
//			{
//				Console.WriteLine("Server: Person updated fail - something went wrong");
//				int code = (int)HttpStatusCode.NotFound;
//				return new StatusCodeResult(code);
//			}
//		}
//	}
//}
