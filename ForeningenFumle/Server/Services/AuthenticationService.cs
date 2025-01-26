using ForeningenFumle.Server.DataAccess;
using ForeningenFumle.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ForeningenFumle.Server.Services
{
	public class AuthenticationService
	{
		private readonly FumleDbContext _context;
		private readonly IPasswordHasher<Person> _passwordHasher;

		public AuthenticationService(FumleDbContext context, IPasswordHasher<Person> passwordHasher)
		{
			_context = context;
			_passwordHasher = passwordHasher;
		}

		public async Task<bool> LoginAsync(LoginModel loginModel)
		{
			var user = await _context.Persons
									  .FirstOrDefaultAsync(u => u.Username == loginModel.Username);

			if (user == null)
			{
				return false; // Bruger ikke fundet
			}

			// Valider adgangskoden mod den gemte hash
			var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginModel.Password);

			return result == PasswordVerificationResult.Success;
		}

		public async Task<bool> RegisterAsync(RegisterModel registerModel)
		{
			// Opret Admin eller Member afhængigt af Role
			if (registerModel.Role == "Admin")
			{
				var admin = new Admin
				{
					Username = registerModel.Username,
					Name = registerModel.Name,
					Email = registerModel.Email,
					PasswordHash = _passwordHasher.HashPassword(null, registerModel.Password),
					Role = registerModel.Role,
					Phonenumber = registerModel.Phonenumber
				};
				_context.Admins.Add(admin);
			}
			else
			{
				var member = new Member
				{
					Username = registerModel.Username,
					Name = registerModel.Name,
					Email = registerModel.Email,
					PasswordHash = _passwordHasher.HashPassword(null, registerModel.Password),
					Role = registerModel.Role,
					Phonenumber = registerModel.Phonenumber
				};
				_context.Members.Add(member);
			}

			await _context.SaveChangesAsync();
			return true;
		}



	}
}
