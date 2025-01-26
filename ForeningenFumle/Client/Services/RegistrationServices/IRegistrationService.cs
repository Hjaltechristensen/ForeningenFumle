using ForeningenFumle.Shared.Models;
using System.Net.Http;

namespace ForeningenFumle.Client.Services.RegistrationServices
{
	public interface IRegistrationService
	{
		Task<Registration[]?> GetAllRegistration();

		Task<int> AddRegistration(Registration registration);

		Task<int> DeleteRegistration(int eventId, int PersonId);

		Task<int> UpdateRegistration(Registration registration);

		Task<bool> IsUserRegisteredForEvent(int eventId, int personId);

		Task<List<Registration>?> GetRegistrationsByPersonId(int personId);



	}
}
