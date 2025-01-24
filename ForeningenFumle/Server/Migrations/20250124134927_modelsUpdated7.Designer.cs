﻿// <auto-generated />
using System;
using ForeningenFumle.Server.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ForeningenFumle.Server.Migrations
{
    [DbContext(typeof(FumleDbContext))]
    [Migration("20250124134927_modelsUpdated7")]
    partial class modelsUpdated7
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ForeningenFumle.Shared.Models.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EventId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeAndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EventId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("ForeningenFumle.Shared.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonType")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("Phonenumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("Phonenumber")
                        .IsUnique()
                        .HasFilter("[Phonenumber] IS NOT NULL");

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasFilter("[Username] IS NOT NULL");

                    b.ToTable("Persons");

                    b.HasDiscriminator<string>("PersonType").HasValue("Person");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("ForeningenFumle.Shared.Models.Registration", b =>
                {
                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<int?>("MemberId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegistrationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.HasKey("PersonId", "EventId");

                    b.HasIndex("EventId");

                    b.HasIndex("MemberId");

                    b.ToTable("Registrations");
                });

            modelBuilder.Entity("ForeningenFumle.Shared.Models.Admin", b =>
                {
                    b.HasBaseType("ForeningenFumle.Shared.Models.Person");

                    b.Property<DateTime>("AdminAssignmentDate")
                        .HasColumnType("datetime2");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("ForeningenFumle.Shared.Models.Member", b =>
                {
                    b.HasBaseType("ForeningenFumle.Shared.Models.Person");

                    b.Property<DateTime>("MembershipStartDato")
                        .HasColumnType("datetime2");

                    b.HasDiscriminator().HasValue("Medlem");
                });

            modelBuilder.Entity("ForeningenFumle.Shared.Models.Registration", b =>
                {
                    b.HasOne("ForeningenFumle.Shared.Models.Event", "Event")
                        .WithMany("Registrations")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ForeningenFumle.Shared.Models.Member", null)
                        .WithMany("Registrations")
                        .HasForeignKey("MemberId");

                    b.HasOne("ForeningenFumle.Shared.Models.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("ForeningenFumle.Shared.Models.Event", b =>
                {
                    b.Navigation("Registrations");
                });

            modelBuilder.Entity("ForeningenFumle.Shared.Models.Member", b =>
                {
                    b.Navigation("Registrations");
                });
#pragma warning restore 612, 618
        }
    }
}
