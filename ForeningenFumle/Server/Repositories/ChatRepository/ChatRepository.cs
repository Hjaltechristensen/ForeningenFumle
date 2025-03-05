using ForeningenFumle.Server.DataAccess;
using ForeningenFumle.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace ForeningenFumle.Server.Repositories.ChatRepository
{
	public class ChatRepository : IChatRepository
	{
		private readonly FumleDbContext _context;

		public ChatRepository(FumleDbContext context)
		{
			_context = context;
		}

		public List<Message> GetMessagesAsync()
		{
			List<Message> messages;
			try
			{
				// Brug OfType<Member>() for at hente kun Members
				messages = _context.Messages.OrderBy(m => m.TimeSent).ToList();
			}
			catch
			{
				messages = new List<Message>();
			}
			return messages;
		}

		public async Task AddMessageAsync(Message message)
		{
			_context.Messages.Add(message);
			await _context.SaveChangesAsync();
		}
	}
}

