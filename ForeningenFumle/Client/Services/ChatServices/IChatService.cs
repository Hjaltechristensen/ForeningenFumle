using ForeningenFumle.Shared.Models;

namespace ForeningenFumle.Client.Services.ChatServices
{
	public interface IChatService
	{
		Task<Message[]?> GetMessagesAsync();
		Task SendMessageAsync(Message message);
	}
}
