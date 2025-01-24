using ForeningenFumle.Client.Services.RegistrationServices;
using ForeningenFumle.Server.DataAccess;
using ForeningenFumle.Server.Services;
using ForeningenFumle.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForeningenFumle.Server.Controllers
{
	[Route("api/authapi")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly AuthenticationService _authenticationService;
		private readonly RegistrationService _registrationService;
		private FumleDbContext _dbContext;

		public AuthController(AuthenticationService authenticationService, RegistrationService registrationService, FumleDbContext dbContext)
		{
			_authenticationService = authenticationService;
			_registrationService = registrationService;
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
		{
			try
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

				// Verificer password
				var passwordHasher = new PasswordHasher<Person>();
				var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginModel.Password);

				if (result == PasswordVerificationResult.Success)
				{
					// Tjek brugertype
					if (user is Admin)
					{
						return Ok("Login succes: Admin");
					}
					else if (user is Member)
					{
						return Ok("Login succes: Member");
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
			catch (Exception ex)
			{
				// Log fejl og returnér en intern serverfejl
				Console.WriteLine($"Fejl i Login: {ex.Message}");
				return StatusCode(500, "Der opstod en fejl under loginforsøget.");
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
	}
}
