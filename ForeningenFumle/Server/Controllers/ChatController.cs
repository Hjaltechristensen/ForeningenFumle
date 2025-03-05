using ForeningenFumle.Server.Repositories.ChatRepository;
using ForeningenFumle.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ForeningenFumle.Server.Controllers
{
	[Route("api/chatapi")]
	[ApiController]
	public class ChatController : ControllerBase
	{
		private readonly IChatRepository _chatRepository;
		private readonly IHubContext<ChatHubRepository> _hubContext;

		public ChatController(IChatRepository chatRepository, IHubContext<ChatHubRepository> hubContext)
		{
			_chatRepository = chatRepository;
			_hubContext = hubContext;
		}

		[HttpGet]
		public IEnumerable<Message> GetMessages()
		{
			return _chatRepository.GetMessagesAsync();
		}


		[HttpPost]
		public async Task<ActionResult> SendMessage(Message message)
		{
			await _chatRepository.AddMessageAsync(message);
			await _hubContext.Clients.All.SendAsync("ReceiveMessage", message.PersonId, message.Content, message.TimeSent, message.PersonName);
			return Ok();
		}
	}
}
