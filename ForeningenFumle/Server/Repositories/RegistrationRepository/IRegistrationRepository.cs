using ForeningenFumle.Shared.Models;

namespace ForeningenFumle.Server.Repositories.RegistrationRepository
{
	public interface IRegistrationRepository
	{
		List<Registration> GetAllRegistrations();
		Registration FindRegistration(int id);
		void AddRegistration(Registration registration);
		bool DeleteRegistration(int id);
		bool UpdateRegistration(Registration registration);
	}
}
