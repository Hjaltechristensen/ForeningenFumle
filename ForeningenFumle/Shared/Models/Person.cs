using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeningenFumle.Shared.Models
{
	public abstract class Person
	{
		public int PersonId { get; set; }
		public string? Name { get; set; } = string.Empty;
		[EmailAddress(ErrorMessage = "Ugyldig e-mail-adresse.")]
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
			PersonId = id;
		}
	}
}
