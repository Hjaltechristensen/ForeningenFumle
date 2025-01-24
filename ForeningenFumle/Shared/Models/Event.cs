using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ForeningenFumle.Shared.Models
{
	public class Event
	{
		public int EventId { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public DateTime TimeAndDate { get; set; }
		public string Location { get; set; } = string.Empty;

		public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
		
		public Event()
		{
			
		}
	}
}
