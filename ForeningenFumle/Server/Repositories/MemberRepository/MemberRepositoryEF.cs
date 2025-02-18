using ForeningenFumle.Server.DataAccess;
using ForeningenFumle.Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace ForeningenFumle.Server.Repositories.MemberRepository
{
	public class MemberRepositoryEF : IMemberRepository
	{
		public List<Member> GetAllMembers()
		{
			var db = new FumleDbContext();
			List<Member> members;
			try
			{
				// Brug OfType<Member>() for at hente kun Members
				members = db.Persons.OfType<Member>().ToList();
			}
			catch
			{
				members = new List<Member>();
			}
			return members;
		}

		public Member FindMember(string username)
		{
			var db = new FumleDbContext();
			Member member;
			try
			{
				member = db.Persons.OfType<Member>().FirstOrDefault(x => x.Username == username);
			}
			catch
			{
				member = new Member(-1);
			}

			return member;
		}
		public void AddMember(Member member)
		{
			try
			{
				// Brug using for korrekt oprydning af DbContext
				using (var db = new FumleDbContext())
				{
					// Tilføj medlemmet til Persons (da Member arver fra Person)
					db.Persons.Add(member);
					db.SaveChanges(); // Gem ændringer i databasen
				}
			}
			catch (Exception ex)
			{
				// Log eller håndter fejlen
				Console.WriteLine($"Fejl ved tilføjelse af member: {ex.Message}");
			}
		}
		public bool DeleteMember(int id)
		{
			try
			{
				using (var db = new FumleDbContext())
				{
					// Find medlemmet (OfType<Member> er ikke nødvendigt her)
					var member = db.Persons.OfType<Member>().FirstOrDefault(x => x.PersonId == id);

					// Hvis medlemmet findes
					if (member != null)
					{
						db.Persons.Remove(member);  // Fjern medlemmet fra Persons
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
				Console.WriteLine($"Fejl ved sletning af member: {ex.Message}");
				return false;
			}
		}
		public bool UpdateMember(Member member)
		{
			try
			{
				using (var db = new FumleDbContext())
				{
					// Find medlemmet med det angivne ID
					var foundMember = db.Persons.OfType<Member>().FirstOrDefault(x => x.PersonId == member.PersonId);

					// Hvis medlemmet ikke findes, returner false
					if (foundMember == null)
					{
						return false;
					}

					// Hvis værdierne allerede er de samme, kan vi undgå at opdatere databasen
					if (member.Name == foundMember.Name &&
						member.Email == foundMember.Email &&
						member.Phonenumber == foundMember.Phonenumber &&
						member.Username == foundMember.Username &&
						member.PasswordHash == foundMember.PasswordHash)
					{
						return true; // Ingen ændringer, returner true
					}

					// Opdater kun de ændrede felter
					foundMember.Name = member.Name;
					foundMember.Email = member.Email;
					foundMember.Phonenumber = member.Phonenumber;
					foundMember.Username = member.Username;
					foundMember.PasswordHash = member.PasswordHash;

					// Gem ændringer i databasen
					int changed = db.SaveChanges();

					// Hvis der blev gemt ændringer, returner true
					return changed > 0;
				}
			}
			catch (Exception ex)
			{
				// Log fejlmeddelelse for fejlfinding
				Console.WriteLine($"Fejl ved opdatering af member: {ex.Message}");
				return false;
			}
		}
	}
}
