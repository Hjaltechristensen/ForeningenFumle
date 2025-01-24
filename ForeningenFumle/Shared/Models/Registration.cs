using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeningenFumle.Shared.Models
{
	public class Registration
	{
		public int PersonId { get; set; }
		public int EventId { get; set; }
		public DateTime RegistrationDate { get; set; } = DateTime.Now;
		public Person Person { get; set; } = null!;
		public Event Event { get; set; } = null!;

		public Registration()
		{
			
		}
	}
}
