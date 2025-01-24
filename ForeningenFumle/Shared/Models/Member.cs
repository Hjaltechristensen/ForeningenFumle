using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeningenFumle.Shared.Models
{
	public class Member : Person
	{
		public DateTime MembershipStartDato { get; set; } = DateTime.Now;

		public Member()
		{
			
		}
		public Member(int id)
		{
			Id = id;
		}
	}
}
