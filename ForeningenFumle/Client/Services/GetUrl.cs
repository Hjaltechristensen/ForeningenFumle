namespace ForeningenFumle.Client.Services
{
	public static class GetUrl
	{
		public static string ReturnUrlString()
		{
#if DEBUG
			string url = "https://localhost:7242/"; // Localhost
#else
			string url = "https://foreningenfumleserver.azurewebsites.net/"; // Online
#endif
			return url;
		}
	}
}
