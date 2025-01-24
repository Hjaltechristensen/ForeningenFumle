using ForeningenFumle.Shared.Models;
using System;
using System.Net.Http.Json;

namespace ForeningenFumle.Client.Services.MemberServices
{
	public class MemberService : IMemberService
	{
		private readonly HttpClient httpClient;

		public MemberService(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		public Task<Member[]?> GetAllMembers()
		{
			var url = "api/memberapi/";
			var result = httpClient.GetFromJsonAsync<Member[]>("https://localhost:7242/" + url);

			return result;
		}

		public async Task<Member?> GetMember(string username)
		{
			var url = "api/memberapi/" + username;
			Console.WriteLine($"Calling API: {"https://localhost:7242/"}{url}");
			var result = await httpClient.GetFromJsonAsync<Member>("https://localhost:7242/" + url);

			return result;
		}

		public async Task<int> AddMember(RegisterModel member)
		{
			var url = "api/memberapi/";
			var response = await httpClient.PostAsJsonAsync("https://localhost:7242/" + url, member);
			if (response.IsSuccessStatusCode)
			{
				Console.WriteLine("Member added successfully.");
			}
			else
			{
				Console.WriteLine("Failed to add member.");
			}

			var responseStatusCode = response.StatusCode;

			return (int)responseStatusCode;
		}

		public async Task<int> DeleteMember(int id)
		{
			var url = "api/memberapi/";
			var response = await httpClient.DeleteAsync("https://localhost:7242/" + url + id);

			var responseStatusCode = response.StatusCode;

			return (int)responseStatusCode;
		}

		public async Task<int> UpdateMember(Member member)
		{
			var url = "api/memberapi/";
			var response = await httpClient.PatchAsJsonAsync("https://localhost:7242/" + url, member);

			var responseStatusCode = response.StatusCode;

			return (int)responseStatusCode;
		}
	}
}
