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
			var url = "api/registrationapi/";
			var result = httpClient.GetFromJsonAsync<Registration[]>("https://localhost:7242/" + url);

			return result;
		}

		public async Task<Registration?> GetRegistration(int id)
		{
			var url = "api/registrationapi/" + id;
			var result = await httpClient.GetFromJsonAsync<Registration>("https://localhost:7242/" + url);

			return result;
		}

		public async Task<int> AddRegistration(Registration registration)
		{
			var url = "api/registrationapi/";
			var response = await httpClient.PostAsJsonAsync("https://localhost:7242/" + url, registration);

			var responseStatusCode = response.StatusCode;

			return (int)responseStatusCode;
		}

		public async Task<int> DeleteRegistration(int eventId, int personId)
		{
			var url = $"api/registrationapi/{eventId}/{personId}";
			var response = await httpClient.DeleteAsync("https://localhost:7242/" + url);

			var responseStatusCode = response.StatusCode;

			return (int)responseStatusCode;
		}

		public async Task<int> UpdateRegistration(Registration registration)
		{
			var url = "api/registrationapi/";
			var response = await httpClient.PatchAsJsonAsync("https://localhost:7242/" + url, registration);

			var responseStatusCode = response.StatusCode;

			return (int)responseStatusCode;
		}

		public async Task<bool> IsUserRegisteredForEvent(int eventId, int personId)
		{
			var url = $"api/registrationapi/{eventId}/{personId}";
			var response = await httpClient.GetFromJsonAsync<bool>("https://localhost:7242/" + url);
			return response;
		}

	}
}
