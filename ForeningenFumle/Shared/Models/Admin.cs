using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeningenFumle.Shared.Models
{
	public class Admin : Person
	{
		public DateTime AdminAssignmentDate { get; set; } = DateTime.Now;

		public Admin()
		{
			
		}
		public Admin(int id)
		{
			Id = id;
		}
	}
}
