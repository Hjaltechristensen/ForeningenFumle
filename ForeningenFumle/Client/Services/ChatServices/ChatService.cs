using ForeningenFumle.Shared.Models;
using System.Net.Http.Json;

namespace ForeningenFumle.Client.Services.ChatServices
{
	public class ChatService : IChatService
	{
		private readonly HttpClient httpClient;

		public ChatService(HttpClient http)
		{
			httpClient = http;
		}

		public async Task<Message[]?> GetMessagesAsync()
		{
			try
			{
				var url = "api/chatapi/";
				var result = await httpClient.GetFromJsonAsync<Message[]>($"{GetUrl.ReturnUrlString()}" + url);

				if (result == null)
				{
					Console.WriteLine("No messages returned from the API.");
					return new Message[0]; // Return an empty array if no data is returned
				}

				return result;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error fetching events: {ex.Message}");
				return new Message[0]; // Return an empty array on error
			}
		}

		public async Task SendMessageAsync(Message message)
		{
			await httpClient.PostAsJsonAsync(GetUrl.ReturnUrlString() + "api/chatapi", message);
		}
	}
}
