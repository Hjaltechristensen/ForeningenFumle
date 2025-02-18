using ForeningenFumle.Shared.Models;
using System.Net.Http.Json;

namespace ForeningenFumle.Client.Services.AuthServices
{
	public class AuthService : IAuthService
	{
		private readonly HttpClient _httpClient;

		public AuthService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<bool> LoginAsync(LoginModel loginModel)
		{
			try
			{
				var url = "api/adminapi/login";
				var response = await _httpClient.PostAsJsonAsync($"{GetUrl.ReturnUrlString()}" + url, loginModel);
				Console.WriteLine("API URL: " + GetUrl.ReturnUrlString() + url);

				if (response.IsSuccessStatusCode)
				{
					return true;
				}
				else
				{
					// Log fejlrespons
					var errorMessage = await response.Content.ReadAsStringAsync();
					Console.WriteLine($"Login fejlede. Statuskode: {response.StatusCode}, Fejl: {errorMessage}");

					// Yderligere logning af fejl
					var responseHeaders = response.Headers.ToString();
					Console.WriteLine($"Response Headers: {responseHeaders}");

					return false;
				}
			}

			catch (Exception ex)
			{
				Console.WriteLine($"LoginAsync fejl: {ex.Message}");
			}

			// Login fejlede
			return false;
		}
	}
}
