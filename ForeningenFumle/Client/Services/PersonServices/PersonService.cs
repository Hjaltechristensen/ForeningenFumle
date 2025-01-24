//using ForeningenFumle.Shared.Models;
//using System.Net.Http.Json;

//namespace ForeningenFumle.Client.Services.PersonServices
//{
//	public class PersonService : IPersonService
//	{
//		private readonly HttpClient httpClient;

//		public PersonService(HttpClient httpClient)
//		{
//			this.httpClient = httpClient;
//		}
//		public Task<Person[]?> GetAllPersons()
//		{
//			var result = httpClient.GetFromJsonAsync<Person[]>("api/personapi");

//			return result;
//		}

//		public async Task<Person?> GetPerson(int id)
//		{
//			var result = await httpClient.GetFromJsonAsync<Person>("api/personapi/" + id);

//			return result;
//		}

//		public async Task<int> AddPerson(Person person)
//		{
//			var response = await httpClient.PostAsJsonAsync("api/personapi", person);

//			var responseStatusCode = response.StatusCode;

//			return (int)responseStatusCode;
//		}

//		public async Task<int> DeletePerson(int id)
//		{
//			var response = await httpClient.DeleteAsync("api/personapi/" + id);

//			var responseStatusCode = response.StatusCode;

//			return (int)responseStatusCode;
//		}

//		public async Task<int> UpdatePerson(Person person)
//		{
//			var response = await httpClient.PatchAsJsonAsync("api/personapi", person);

//			var responseStatusCode = response.StatusCode;

//			return (int)responseStatusCode;
//		}
//	}
//}
