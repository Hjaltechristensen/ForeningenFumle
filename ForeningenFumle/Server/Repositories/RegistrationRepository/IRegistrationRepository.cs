using ForeningenFumle.Shared.Models;

namespace ForeningenFumle.Server.Repositories.RegistrationRepository
{
	public interface IRegistrationRepository
	{
		List<Registration> GetAllRegistrations();
		Registration FindRegistration(int id);
		List<Registration> GetRegistrationsByPersonId(int personId);
		void AddRegistration(Registration registration);
		bool DeleteRegistration(int eventId, int personId);
		bool UpdateRegistration(Registration registration);
		bool IsUserRegisteredForEvent(int eventId, int personId);

	}
}
