using Microsoft.EntityFrameworkCore;
using ForeningenFumle.Shared.Models;
using Microsoft.AspNetCore.Connections;

namespace ForeningenFumle.Server.DataAccess
{
	public class FumleDbContext : DbContext
	{
		public DbSet<Person> Persons { get; set; }
		public DbSet<Admin> Admins { get; set; }
		public DbSet<Member> Members { get; set; }
		public DbSet<Event> Events { get; set; }
		public DbSet<Registration> Registrations { get; set; }
		public DbSet<Message> Messages { get; set; }

		public FumleDbContext(DbContextOptions<FumleDbContext> options)
		: base(options) // Videregiv options til base-konstruktøren
		{
		}
		public FumleDbContext()
		{

		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
			=> optionsBuilder.UseSqlServer("Server=LAPTOP-KOEHKSOQ;Database=ForeningenFumle;Trusted_Connection=True;TrustServerCertificate=True;");

		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//	=> optionsBuilder.UseSqlServer("Server=LAPTOP-KOEHKSOQ;Database=ForeningenFumle;Trusted_Connection=True;");

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Eksempel på komposit primærnøgle i Tilmelding
			modelBuilder.Entity<Registration>()
				.HasKey(t => new { t.PersonId, t.EventId }); // Kombiner MedlemId og EventId som primærnøgle

			modelBuilder.Entity<Registration>()
				.HasOne(t => t.Person)
				.WithMany(p => p.Registrations)
				.HasForeignKey(t => t.PersonId)
				.OnDelete(DeleteBehavior.Cascade);

			// Konfigurer Event → Tilmelding (én-til-mange)
			modelBuilder.Entity<Registration>()
				.HasOne(t => t.Event)                   // Tilmelding hører til ét Event
				.WithMany(e => e.Registrations)          // Event har mange Tilmeldinger
				.HasForeignKey(t => t.EventId)          // Fremmednøgle i Tilmelding
				.OnDelete(DeleteBehavior.Restrict);     // Restrict: Event kan ikke slettes, hvis der findes tilmeldinger

			modelBuilder.Entity<Message>()
				.HasOne(m => m.Sender)
				.WithMany() // En person kan sende flere beskeder
				.HasForeignKey(m => m.PersonId)
				.OnDelete(DeleteBehavior.Cascade); // Sletter beskeder, hvis brugeren slettes

			modelBuilder.Entity<Person>()
				.Property(c => c.PersonId)
				.ValueGeneratedOnAdd();

			modelBuilder.Entity<Person>()
				.HasIndex(p => p.Email)
				.IsUnique();

			modelBuilder.Entity<Person>()
				.HasIndex(m => m.Phonenumber)
				.IsUnique(); // Unikt telefonnummer

			modelBuilder.Entity<Person>()
				.HasIndex(p => p.Username)
				.IsUnique();

			modelBuilder.Entity<Person>()
				.HasDiscriminator<string>("PersonType") // Tilføj kolonne 'PersonType'
				.HasValue<Person>("Person")            // Basisværdien
				.HasValue<Member>("Medlem")            // Værdi for Medlem
				.HasValue<Admin>("Admin");             // Værdi for Admin

			base.OnModelCreating(modelBuilder);
		}
	}
}
