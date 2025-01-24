using ForeningenFumle.Client.Services.AdminServices;
using ForeningenFumle.Client.Services.MemberServices;
using ForeningenFumle.Shared.Models;

namespace ForeningenFumle.Client.Shared
{
	public class UserState
	{
		private readonly IAdminService _adminService;
		private readonly IMemberService _memberService;

		public Admin? Admin { get; private set; }
		public Member? Member { get; private set; }
		public event Action? OnChange;

		public UserState(IAdminService adminService, IMemberService memberService)
		{
			_adminService = adminService ?? throw new ArgumentNullException(nameof(adminService));
			_memberService = memberService ?? throw new ArgumentNullException(nameof(memberService));
		}

		public async Task<bool> SetUser(string? username)
		{
			if (string.IsNullOrWhiteSpace(username))
				throw new ArgumentException("Brugernavn må ikke være tomt", nameof(username));

			try
			{
				// Tjek først, om det er en admin
				Admin = await _adminService.GetAdmin(username);

				if (Admin != null)
				{
					Member = null; // Ryd member, hvis der er logget ind som admin
					NotifyStateChanged();
					return true;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Fejl ved login: {ex.Message}");
			}

			try
			{
				Member = await _memberService.GetMember(username);

				if (Member != null)
				{
					Admin = null; // Ryd admin, hvis der er logget ind som medlem
					NotifyStateChanged();
					return true;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Fejl ved login: {ex.Message}");
			}
			// Tjek herefter, om det er et medlem
			
			

			// Ingen bruger fundet
			Admin = null;
			Member = null;
			NotifyStateChanged();
			return false;
		}

		private void NotifyStateChanged() => OnChange?.Invoke();
	}
}
