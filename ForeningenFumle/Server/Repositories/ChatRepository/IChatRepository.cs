using ForeningenFumle.Shared.Models;

namespace ForeningenFumle.Server.Repositories.ChatRepository
{
	public interface IChatRepository
	{
		List<Message> GetMessagesAsync();
		Task AddMessageAsync(Message message);
	}
}
