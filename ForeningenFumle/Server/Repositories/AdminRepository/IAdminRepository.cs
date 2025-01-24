using ForeningenFumle.Shared.Models;

namespace ForeningenFumle.Server.Repositories.AdminRepository
{
	public interface IAdminRepository
	{
		List<Admin> GetAllAdmins();
		Admin FindAdmin(string username);
		void AddAdmin(Admin admin);
		bool DeleteAdmin(int id);
		bool UpdateAdmin(Admin admin);
	}
}
