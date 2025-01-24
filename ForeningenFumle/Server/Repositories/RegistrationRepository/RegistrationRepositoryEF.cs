using ForeningenFumle.Server.DataAccess;
using ForeningenFumle.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace ForeningenFumle.Server.Repositories.RegistrationRepository
{
	public class RegistrationRepositoryEF : IRegistrationRepository
	{
		public List<Registration> GetAllRegistrations()
		{
			throw new NotImplementedException();
		}
		public Registration FindRegistration(int id)
		{
			throw new NotImplementedException();
		}
		public void AddRegistration(Registration registration)
		{
			try
			{
				// Brug using for korrekt oprydning af DbContext
				using (var db = new FumleDbContext())
				{
					// Tilføj medlemmet til Persons (da Member arver fra Person)
					db.Registrations.Add(registration);
					db.SaveChanges(); // Gem ændringer i databasen
				}
			}
			catch (Exception ex)
			{
				// Log eller håndter fejlen
				Console.WriteLine($"Fejl ved tilføjelse af registrering: {ex.Message}");
			}
		}
		public bool DeleteRegistration(int eventId, int personId)
		{
			var db = new FumleDbContext();
			var registration = db.Registrations.FirstOrDefault(r => r.EventId == eventId && r.PersonId == personId);

			if (registration != null)
			{
				db.Registrations.Remove(registration);
				db.SaveChangesAsync();
				return true;
			}
			return false;
		}
		public bool UpdateRegistration(Registration registration)
		{
			throw new NotImplementedException();
		}
		public bool IsUserRegisteredForEvent(int eventId, int personId)
		{
			var db = new FumleDbContext();
			return db.Registrations.Any(r => r.EventId == eventId && r.PersonId == personId);
		}

	}
}
