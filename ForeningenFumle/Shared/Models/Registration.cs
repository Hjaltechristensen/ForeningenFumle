using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ForeningenFumle.Shared.Models
{
	public class Registration
	{
		public int PersonId { get; set; } // Delt primary key
		public int EventId { get; set; } // Delt primary key
		public DateTime RegistrationDate { get; set; } = DateTime.Now;
		[JsonIgnore]
		public Person? Person { get; set; } = null!;
		[JsonIgnore]
		public Event? Event { get; set; } = null!;

		public Registration()
		{
			
		}
	}
}
