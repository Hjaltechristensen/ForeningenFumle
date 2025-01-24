using ForeningenFumle.Server.DataAccess;
using ForeningenFumle.Server.Repositories.AdminRepository;
using ForeningenFumle.Server.Services;
using ForeningenFumle.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ForeningenFumle.Server.Controllers
{
	[Route("api/adminapi")]
	[ApiController]
	public class AdminController : ControllerBase
	{
		private readonly IAdminRepository repository = new AdminRepositoryEF();
		private AuthenticationService _authenticationService;
		private FumleDbContext _dbContext = new FumleDbContext();

		public AdminController(AuthenticationService authenticationService)
		{
			_authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
		}


		[HttpGet]
		public IEnumerable<Admin> GetAllAdmins()
		{
			return repository.GetAllAdmins();
		}

		[HttpDelete("{id:int}")]
		public StatusCodeResult DeleteAdmin(int id)
		{
			Console.WriteLine("Server: Delete admin called: id = " + id);

			bool deleted = repository.DeleteAdmin(id);
			if (deleted)
			{
				Console.WriteLine("Server: Admin deleted succces");
				int code = (int)HttpStatusCode.OK;
				return new StatusCodeResult(code);
			}
			else
			{
				Console.WriteLine("Server: Admin deleted fail - not found");
				int code = (int)HttpStatusCode.NotFound;
				return new StatusCodeResult(code);
			}
		}

		[HttpPost]
		public async Task<ActionResult> AddAdmin(RegisterModel registerModel)
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
			return StatusCode(500, "An error occurred while creating the admin.");
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
				// Her bestemmer vi om brugeren er en Admin eller Member
				if (user is Admin)
				{
					// Admin-specifik logik
					return Ok("Login succes: Admin"); // Du kan også returnere en JWT-token eller en specifik admin-respons her
				}
				else if (user is Member)
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
		public Admin FindAdmin(string username)
		{
			Console.WriteLine($"API kaldt med username: {username}");
			var result = repository.FindAdmin(username);
			return result;
		}

		[HttpPatch]
		public StatusCodeResult UpdateAdmin(Admin admin)
		{
			bool updated = repository.UpdateAdmin(admin);
			if (updated)
			{
				Console.WriteLine("Server: Admin updated succces");
				int code = (int)HttpStatusCode.OK;
				return new StatusCodeResult(code);
			}
			else
			{
				Console.WriteLine("Server: Admin updated fail - something went wrong");
				int code = (int)HttpStatusCode.NotFound;
				return new StatusCodeResult(code);
			}
		}
	}
}
