using ForeningenFumle.Shared.Models;
using System.Net.Http;
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
			var result = httpClient.GetFromJsonAsync<Registration[]>($"{GetUrl.ReturnUrlString()}" + url);

			return result;
		}

		public async Task<List<Registration>?> GetRegistrationsByPersonId(int personId)
		{
			var url = $"api/registrationapi/{personId}";
			return await httpClient.GetFromJsonAsync<List<Registration>>($"{GetUrl.ReturnUrlString()}" + url);
		}

		public async Task<int> AddRegistration(Registration registration)
		{
			var url = "api/registrationapi/";
			var response = await httpClient.PostAsJsonAsync($"{GetUrl.ReturnUrlString()}" + url, registration);

			var responseStatusCode = response.StatusCode;

			return (int)responseStatusCode;
		}

		public async Task<int> DeleteRegistration(int eventId, int personId)
		{
			var url = $"api/registrationapi/{eventId}/{personId}";
			var response = await httpClient.DeleteAsync($"{GetUrl.ReturnUrlString()}" + url);

			var responseStatusCode = response.StatusCode;

			return (int)responseStatusCode;
		}

		public async Task<int> UpdateRegistration(Registration registration)
		{
			var url = "api/registrationapi/";
			var response = await httpClient.PatchAsJsonAsync($"{GetUrl.ReturnUrlString()}" + url, registration);

			var responseStatusCode = response.StatusCode;

			return (int)responseStatusCode;
		}

		public async Task<bool> IsUserRegisteredForEvent(int eventId, int personId)
		{
			var url = $"api/registrationapi/{eventId}/{personId}";
			var response = await httpClient.GetFromJsonAsync<bool>($"{GetUrl.ReturnUrlString()}" + url);
			return response;
		}

	}
}
