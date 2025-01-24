﻿using ForeningenFumle.Client.Pages;
using ForeningenFumle.Shared.Models;
using System.Net.Http;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace ForeningenFumle.Client.Services.AdminServices
{
	public class AdminService : IAdminService
	{
		private readonly HttpClient httpClient;
		public AdminService(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		public Task<Admin[]?> GetAllAdmins()
		{
			var url = "api/adminapi/";
			var result = httpClient.GetFromJsonAsync<Admin[]>("https://localhost:7242/" + url);

			return result;
		}

		public async Task<Admin?> GetAdmin(string username)
		{
			var url = "api/adminapi/" + username;
			Console.WriteLine($"Calling API: {"https://localhost:7242/"}{url}");
			var result = await httpClient.GetFromJsonAsync<Admin>("https://localhost:7242/" + url);

			return result;
		}

		public async Task<int> AddAdmin(RegisterModel admin)
		{
			var url = "api/adminapi/";
			var response = await httpClient.PostAsJsonAsync("https://localhost:7242/" + url, admin);
			if (response.IsSuccessStatusCode)
			{
				Console.WriteLine("Admin added successfully.");
			}
			else
			{
				Console.WriteLine("Failed to add admin.");
			}

			var responseStatusCode = response.StatusCode;

			return (int)responseStatusCode;
		}

		public async Task<int> DeleteAdmin(int id)
		{
			var url = "api/adminapi/";
			var response = await httpClient.DeleteAsync("https://localhost:7242/" + url + id);

			var responseStatusCode = response.StatusCode;

			return (int)responseStatusCode;
		}

		public async Task<int> UpdateAdmin(Admin admin)
		{
			var url = "api/adminapi/";
			var response = await httpClient.PatchAsJsonAsync("https://localhost:7242/" + url, admin);

			var responseStatusCode = response.StatusCode;

			return (int)responseStatusCode;
		}
	}
}
