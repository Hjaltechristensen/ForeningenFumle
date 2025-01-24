using ForeningenFumle.Shared.Models;

namespace ForeningenFumle.Client.Services.RegistrationServices
{
	public interface IRegistrationService
	{
		Task<Registration[]?> GetAllRegistration();

		Task<Registration?> GetRegistration(int id);

		Task<int> AddRegistration(Registration registration);

		Task<int> DeleteRegistration(int id);

		Task<int> UpdateRegistration(Registration registration);
	}
}
