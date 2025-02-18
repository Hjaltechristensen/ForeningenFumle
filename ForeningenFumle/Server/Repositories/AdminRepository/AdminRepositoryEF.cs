using ForeningenFumle.Server.DataAccess;
using ForeningenFumle.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace ForeningenFumle.Server.Repositories.AdminRepository
{
	public class AdminRepositoryEF : IAdminRepository
	{
		public List<Admin> GetAllAdmins()
		{
			var db = new FumleDbContext();
			List<Admin> admins;
			try
			{
				// Brug OfType<Member>() for at hente kun Members
				admins = db.Persons.OfType<Admin>().ToList();
			}
			catch
			{
				admins = new List<Admin>();
			}
			return admins;
		}

		public Admin FindAdmin(string username)
		{
			var db = new FumleDbContext();
			Admin admin;
			try
			{
				admin = db.Persons.OfType<Admin>().FirstOrDefault(x => x.Username == username);
			}
			catch
			{
				admin = new Admin(-1);
			}

			return admin;
		}
		public void AddAdmin(Admin admin)
		{
			try
			{
				// Brug using for korrekt oprydning af DbContext
				using (var db = new FumleDbContext())
				{
					// Tilføj medlemmet til Persons (da Member arver fra Person)
					db.Persons.Add(admin);
					db.SaveChanges(); // Gem ændringer i databasen
				}
			}
			catch (Exception ex)
			{
				// Log eller håndter fejlen
				Console.WriteLine($"Fejl ved tilføjelse af admin: {ex.Message}");
			}
		}
		public bool DeleteAdmin(int id)
		{
			try
			{
				using (var db = new FumleDbContext())
				{
					// Find medlemmet (OfType<Member> er ikke nødvendigt her)
					var admin = db.Persons.OfType<Admin>().FirstOrDefault(x => x.PersonId == id);

					// Hvis medlemmet findes
					if (admin != null)
					{
						db.Persons.Remove(admin);  // Fjern medlemmet fra Persons
						int changed = db.SaveChanges();  // Gem ændringer

						// Hvis der blev foretaget ændringer i databasen
						return changed > 0;
					}

					// Hvis medlemmet ikke findes, returner false
					return false;
				}
			}
			catch (Exception ex)
			{
				// Log fejlen eller håndter undtagelsen
				Console.WriteLine($"Fejl ved sletning af admin: {ex.Message}");
				return false;
			}
		}
		public bool UpdateAdmin(Admin admin)
		{
			try
			{
				using (var db = new FumleDbContext())
				{
					// Find medlemmet med det angivne ID
					var foundAdmin = db.Persons.OfType<Admin>().FirstOrDefault(x => x.PersonId == admin.PersonId);

					// Hvis medlemmet ikke findes, returner false
					if (foundAdmin == null)
					{
						return false;
					}

					// Hvis værdierne allerede er de samme, kan vi undgå at opdatere databasen
					if (admin.Name == foundAdmin.Name &&
						admin.Email == foundAdmin.Email &&
						admin.Phonenumber == foundAdmin.Phonenumber &&
						admin.Username == foundAdmin.Username &&
						admin.PasswordHash == foundAdmin.PasswordHash)
					{
						return true; // Ingen ændringer, returner true
					}

					// Opdater kun de ændrede felter
					foundAdmin.Name = admin.Name;
					foundAdmin.Email = admin.Email;
					foundAdmin.Phonenumber = admin.Phonenumber;
					foundAdmin.Username = admin.Username;
					foundAdmin.PasswordHash = admin.PasswordHash;

					// Gem ændringer i databasen
					int changed = db.SaveChanges();

					// Hvis der blev gemt ændringer, returner true
					return changed > 0;
				}
			}
			catch (Exception ex)
			{
				// Log fejlmeddelelse for fejlfinding
				Console.WriteLine($"Fejl ved opdatering af admin: {ex.Message}");
				return false;
			}
		}
	}
}
