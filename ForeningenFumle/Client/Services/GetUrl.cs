namespace ForeningenFumle.Client.Services
{
	public static class GetUrl
	{
		public static string ReturnUrlString()
		{
			string url = "https://foreningenfumleserver.azurewebsites.net/"; // Online
#if DEBUG
			url = "https://localhost:7242/"; // Localhost
#endif
			return url;
		}
	}
}
