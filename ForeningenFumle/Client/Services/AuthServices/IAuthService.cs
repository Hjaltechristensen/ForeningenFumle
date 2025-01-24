using ForeningenFumle.Shared.Models;

namespace ForeningenFumle.Client.Services.AuthServices
{
	public interface IAuthService
	{
		Task<bool> LoginAsync(LoginModel loginModel);
	}
}
