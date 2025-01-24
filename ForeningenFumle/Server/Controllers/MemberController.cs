using ForeningenFumle.Server.Repositories.MemberRepository;
using ForeningenFumle.Shared.Models;
using ForeningenFumle.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ForeningenFumle.Server.DataAccess;
using ForeningenFumle.Server.Repositories.AdminRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ForeningenFumle.Server.Controllers
{
	[Route("api/memberapi")]
	[ApiController]
	public class MemberController : ControllerBase
	{
		private readonly IMemberRepository repository = new MemberRepositoryEF();
		private AuthenticationService _authenticationService;
		private FumleDbContext _dbContext = new FumleDbContext();

		public MemberController(AuthenticationService authenticationService)
		{
			_authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
		}


		[HttpGet]
		public IEnumerable<Member> GetAllMembers()
		{
			return repository.GetAllMembers();
		}

		[HttpDelete("{id:int}")]
		public StatusCodeResult DeleteMember(int id)
		{
			Console.WriteLine("Server: Delete member called: id = " + id);

			bool deleted = repository.DeleteMember(id);
			if (deleted)
			{
				Console.WriteLine("Server: Member deleted succces");
				int code = (int)HttpStatusCode.OK;
				return new StatusCodeResult(code);
			}
			else
			{
				Console.WriteLine("Server: Member deleted fail - not found");
				int code = (int)HttpStatusCode.NotFound;
				return new StatusCodeResult(code);
			}
		}

		[HttpPost]
		public async Task<ActionResult> AddMember(RegisterModel registerModel)
		{
			if (!ModelState.IsValid)
			{
				var errors = ModelState.Values.SelectMany(v => v.Errors)
											   .Select(e => e.ErrorMessage)
											   .ToList();
				Console.WriteLine("ModelState errors: " + string.Join(", ", errors));
				return BadRequest("Invalid data");
			}
			bool result = await _authenticationService.RegisterAsync(registerModel);
			if (result)
			{
				return Ok();
			}
			return StatusCode(500, "An error occurred while creating the member.");
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
		{
			if (loginModel == null || string.IsNullOrEmpty(loginModel.Username) || string.IsNullOrEmpty(loginModel.Password))
			{
				return BadRequest("Username and password are required.");
			}


			// Find brugeren i databasen
			var user = await _dbContext.Persons.FirstOrDefaultAsync(p => p.Username == loginModel.Username);

			if (user == null)
			{
				return Unauthorized("Brugernavn ikke fundet.");
			}

			// Brug PasswordHasher til at verificere passwordet
			var passwordHasher = new PasswordHasher<Person>();
			var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginModel.Password);

			if (result == PasswordVerificationResult.Success)
			{
				// Her bestemmer vi om brugeren er en Member eller Member
				if (user is Member)
				{
					// Member-specifik logik
					return Ok("Login succes: Member"); // Du kan også returnere en JWT-token eller en specifik Member-respons her
				}
				else if (user is Admin)
				{
					// Member-specifik logik
					return Ok("Login succes: Member"); // Du kan også returnere en JWT-token eller en specifik member-respons her
				}
				else
				{
					return Unauthorized("Ukendt brugertype.");
				}
			}
			else
			{
				return Unauthorized("Forkert adgangskode.");
			}
		}


		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState); // Returner fejl, hvis validering fejler
			}

			try
			{
				await _authenticationService.RegisterAsync(registerModel);
				return Ok("Bruger registreret.");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("{username}")]
		public Member FindMember(string username)
		{
			Console.WriteLine($"API kaldt med username: {username}");
			var result = repository.FindMember(username);
			return result;
		}

		[HttpPatch]
		public StatusCodeResult UpdateMember(Member member)
		{
			bool updated = repository.UpdateMember(member);
			if (updated)
			{
				Console.WriteLine("Server: Member updated succces");
				int code = (int)HttpStatusCode.OK;
				return new StatusCodeResult(code);
			}
			else
			{
				Console.WriteLine("Server: Member updated fail - something went wrong");
				int code = (int)HttpStatusCode.NotFound;
				return new StatusCodeResult(code);
			}
		}
	}
}
