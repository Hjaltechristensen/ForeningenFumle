using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeningenFumle.Shared.Models
{
	public abstract class Person
	{
		public int Id { get; set; }
		public string? Name { get; set; } = string.Empty;
		public string? Email { get; set; } = string.Empty;
		public string? Phonenumber { get; set; }
		public string? Username { get; set; }
		public string? PasswordHash { get; set; }
		public string Role { get; set; }
		public ICollection<Registration> Registrations { get; set; } = new List<Registration>();

		public Person()
		{
			
		}

		public Person(int id)
		{
			Id = id;
		}
	}
}
