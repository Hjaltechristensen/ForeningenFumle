using System.Reflection.Metadata;
using Blazored.LocalStorage;
using ForeningenFumle.Client.Shared;
using Microsoft.AspNetCore.Components;

namespace ForeningenFumle.Client.Services
{
	public class LogoutTimeService
	{
		[Inject]
		ILocalStorageService localStore { get; set; }
		public async void CheckLogoutTime()
		{
			string? loggedIn = await localStore.GetItemAsync<string>("loggedIn");
			if (loggedIn == "false")
			{
				MainLayout.Logout();
			}
			else
			{
				string? timeStampStr = await localStore.GetItemAsync<string>("timeStamp");

				if (DateTime.TryParse(timeStampStr, out DateTime storedTime))
				{
					TimeSpan timeDiff = DateTime.Now - storedTime;

					if (timeDiff.TotalMinutes > 1) // Sammenlign total minutter i stedet for at splitte
					{
						MainLayout.Logout();
					}
				}
			}
		}
	}
}
