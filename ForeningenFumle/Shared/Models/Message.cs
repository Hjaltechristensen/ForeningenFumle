using System.ComponentModel.DataAnnotations;

namespace ForeningenFumle.Shared.Models
{
	public class Message
	{
		public int MessageId { get; set; }
		public string? Content { get; set; } = string.Empty;
		public DateTime TimeSent { get; set; } = DateTime.Now;

		// Foreign key til Person
		public int? PersonId { get; set; }
		public string? PersonName { get; set; }
		public Person? Sender { get; set; }

		public Message()
		{
			
		}
	}
}
