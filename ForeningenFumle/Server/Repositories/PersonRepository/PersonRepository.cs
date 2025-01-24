//using ForeningenFumle.Server.DataAccess;
//using ForeningenFumle.Shared.Models;

//namespace ForeningenFumle.Server.Repositories.PersonRepository
//{
//	public class PersonRepository : IPersonRepository
//	{
//		public List<Person> GetAllPersons()
//		{
//			var db = new FumleDbContext();
//			List<Person> persons;
//			try
//			{
//				// Brug OfType<Member>() for at hente kun Members
//				persons = db.Persons.ToList();
//			}
//			//catch
//			{
//				persons = new List<Person>();
//			}
//			return persons;
//		}
//		public Person FindPerson(int id)
//		{
//			try
//			{
//				using (var db = new FumleDbContext())
//				{
//					return db.Persons.FirstOrDefault(x => x.Id == id)
//						   ?? new Person(-1); // Returnér en standard, hvis medlemmet ikke findes
//				}
//			}
//			catch (Exception ex)
//			{
//				Console.WriteLine($"Fejl under hentning af person: {ex.Message}");
//				return new Person(-1); // Returnér standard ved fejl
//			}
//		}
//		public void AddPerson(Person person)
//		{
//			try
//			{
//				// Brug using for korrekt oprydning af DbContext
//				using (var db = new FumleDbContext())
//				{
//					// Tilføj medlemmet til Persons (da Member arver fra Person)
//					db.Persons.Add(person);
//					db.SaveChanges(); // Gem ændringer i databasen
//				}
//			}
//			catch (Exception ex)
//			{
//				// Log eller håndter fejlen
//				Console.WriteLine($"Fejl ved tilføjelse af person: {ex.Message}");
//			}
//		}
//		public bool DeletePerson(int id)
//		{
//			try
//			{
//				using (var db = new FumleDbContext())
//				{
//					// Find medlemmet (OfType<Member> er ikke nødvendigt her)
//					var person = db.Persons.FirstOrDefault(x => x.Id == id);

//					// Hvis medlemmet findes
//					if (person != null)
//					{
//						db.Persons.Remove(person);  // Fjern medlemmet fra Persons
//						int changed = db.SaveChanges();  // Gem ændringer

//						// Hvis der blev foretaget ændringer i databasen
//						return changed > 0;
//					}

//					// Hvis medlemmet ikke findes, returner false
//					return false;
//				}
//			}
//			catch (Exception ex)
//			{
//				// Log fejlen eller håndter undtagelsen
//				Console.WriteLine($"Fejl ved sletning af person: {ex.Message}");
//				return false;
//			}
//		}
//		public bool UpdatePerson(Person person)
//		{
//			try
//			{
//				using (var db = new FumleDbContext())
//				{
//					// Find medlemmet med det angivne ID
//					var foundPerson= db.Persons.FirstOrDefault(x => x.Id == person.Id);

//					// Hvis medlemmet ikke findes, returner false
//					if (foundPerson == null)
//					{
//						return false;
//					}

//					// Hvis værdierne allerede er de samme, kan vi undgå at opdatere databasen
//					if (person.Name == foundPerson.Name &&
//						person.Email == foundPerson.Email &&
//						person.Phonenumber == foundPerson.Phonenumber &&
//						person.Username == foundPerson.Username &&
//						person.PasswordHash == foundPerson.PasswordHash)
//					{
//						return true; // Ingen ændringer, returner true
//					}

//					// Opdater kun de ændrede felter
//					foundPerson.Name = person.Name;
//					foundPerson.Email = person.Email;
//					foundPerson.Phonenumber = person.Phonenumber;
//					foundPerson.Username = person.Username;
//					foundPerson.PasswordHash = person.PasswordHash;

//					// Gem ændringer i databasen
//					int changed = db.SaveChanges();

//					// Hvis der blev gemt ændringer, returner true
//					return changed > 0;
//				}
//			}
//			catch (Exception ex)
//			{
//				// Log fejlmeddelelse for fejlfinding
//				Console.WriteLine($"Fejl ved opdatering af person: {ex.Message}");
//				return false;
//			}
//		}
//	}
//}
