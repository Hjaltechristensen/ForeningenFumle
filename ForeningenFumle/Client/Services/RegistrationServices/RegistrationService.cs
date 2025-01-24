using ForeningenFumle.Shared.Models;
using System.Net.Http.Json;

namespace ForeningenFumle.Client.Services.RegistrationServices
{
	public class RegistrationService : IRegistrationService
	{
		private readonly HttpClient httpClient;

		public RegistrationService(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}
		public Task<Registration[]?> GetAllRegistration()
		{
			var result = httpClient.GetFromJsonAsync<Registration[]>("api/registrationapi");

			return result;
		}

		public async Task<Registration?> GetRegistration(int id)
		{
			var result = await httpClient.GetFromJsonAsync<Registration>("api/registrationapi/" + id);

			return result;
		}

		public async Task<int> AddRegistration(Registration registration)
		{
			var response = await httpClient.PostAsJsonAsync("api/registrationapi", registration);

			var responseStatusCode = response.StatusCode;

			return (int)responseStatusCode;
		}

		public async Task<int> DeleteRegistration(int id)
		{
			var response = await httpClient.DeleteAsync("api/registrationapi/" + id);

			var responseStatusCode = response.StatusCode;

			return (int)responseStatusCode;
		}

		public async Task<int> UpdateRegistration(Registration registration)
		{
			var response = await httpClient.PatchAsJsonAsync("api/registrationapi", registration);

			var responseStatusCode = response.StatusCode;

			return (int)responseStatusCode;
		}
	}
}
