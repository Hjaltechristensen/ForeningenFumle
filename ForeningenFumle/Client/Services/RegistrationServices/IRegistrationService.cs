using ForeningenFumle.Shared.Models;

namespace ForeningenFumle.Client.Services.RegistrationServices
{
	public interface IRegistrationService
	{
		Task<Registration[]?> GetAllRegistration();

		Task<Registration?> GetRegistration(int id);

		Task<int> AddRegistration(Registration registration);

		Task<int> DeleteRegistration(int eventId, int PersonId);

		Task<int> UpdateRegistration(Registration registration);

		Task<bool> IsUserRegisteredForEvent(int eventId, int personId);

	}
}
