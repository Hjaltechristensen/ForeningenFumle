using ForeningenFumle.Shared.Models;
using System.Net.Http.Json;

namespace ForeningenFumle.Client.Services.EventServices
{
	public class EventService : IEventService
	{
		private readonly HttpClient httpClient;
		public EventService(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		public Task<Event[]?> GetAllEvent()
		{
			var url = "api/eventapi/";
			var result = httpClient.GetFromJsonAsync<Event[]>("https://localhost:7242/" + url);

			return result;
		}

		public async Task<Event?> GetEvent(int id)
		{
			var result = await httpClient.GetFromJsonAsync<Event>("api/eventapi/" + id);

			return result;
		}

		public async Task<int> AddEvent(Event eEvent)
		{
			var url = "api/eventapi/";
			var response = await httpClient.PostAsJsonAsync("https://localhost:7242/" + url, eEvent);
			if (response.IsSuccessStatusCode)
			{
				Console.WriteLine("Event added successfully.");
			}
			else
			{
				Console.WriteLine("Failed to add event.");
			}
			var responseStatusCode = response.StatusCode;

			return (int)responseStatusCode;
		}

		public async Task<int> DeleteEvent(int id)
		{
			var response = await httpClient.DeleteAsync("api/eventapi/" + id);

			var responseStatusCode = response.StatusCode;

			return (int)responseStatusCode;
		}

		public async Task<int> UpdateEvent(Event @event)
		{
			var response = await httpClient.PatchAsJsonAsync("api/eventapi", @event);

			var responseStatusCode = response.StatusCode;

			return (int)responseStatusCode;
		}
	}
}
