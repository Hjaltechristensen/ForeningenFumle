using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeningenFumle.Shared.Models
{
	public class RegisterModel
	{
		[Required]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "Brugernavn skal være mellem 3 og 50 tegn.")]
		public string Username { get; set; }

		[Required]
		[StringLength(100, MinimumLength = 6, ErrorMessage = "Adgangskoden skal være mindst 6 tegn.")]
		public string Password { get; set; }

		[EmailAddress(ErrorMessage = "Ugyldig e-mail-adresse.")]
		public string Email { get; set; }
		public string Phonenumber { get; set; }
		public string Name { get; set; }

		public string Role { get; set; } 

		public RegisterModel()
		{
			
		}
	}

}
