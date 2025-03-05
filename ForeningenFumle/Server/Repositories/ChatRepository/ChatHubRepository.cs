using ForeningenFumle.Shared.Models;
using Microsoft.AspNetCore.SignalR;

namespace ForeningenFumle.Server.Repositories.ChatRepository
{
	public class ChatHubRepository : Hub
	{
		public async Task SendMessage(Message message)
		{
			// Send beskeden til alle forbundne klienter
			await Clients.All.SendAsync("ReceiveMessage", message.PersonId, message.Content, message.TimeSent, message.PersonName);
		}
	}
}
