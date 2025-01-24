using ForeningenFumle.Shared.Models;

namespace ForeningenFumle.Client.Services.AdminServices
{
	public interface IAdminService
	{
		Task<Admin[]?> GetAllAdmins();

		Task<Admin?> GetAdmin(string username);

		Task<int> AddAdmin(RegisterModel admin);

		Task<int> DeleteAdmin(int id);

		Task<int> UpdateAdmin(Admin admin);
	}
}
