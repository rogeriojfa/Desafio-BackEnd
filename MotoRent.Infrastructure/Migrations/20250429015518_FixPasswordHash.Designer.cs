﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MotoRent.Infrastructure.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MotoRent.Infrastructure.Migrations
{
    [DbContext(typeof(MotoRentDbContext))]
    [Migration("20250429015518_FixPasswordHash")]
    partial class FixPasswordHash
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Deliveryman", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Cnh")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CNHImagePath")
                        .HasColumnType("text");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CnhType")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Cnh")
                        .IsUnique();

                    b.HasIndex("Cnpj")
                        .IsUnique();

                    b.ToTable("Deliverymen");
                });

            modelBuilder.Entity("MotoRent.Domain.Entities.Moto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("boolean");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Plate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Plate")
                        .IsUnique();

                    b.ToTable("Motos");
                });

            modelBuilder.Entity("MotoRent.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a1f5a1d0-57d8-4a3c-9999-123456789abc"),
                            Email = "admin@motorrent.com",
                            Name = "Administrator",
                            PasswordHash = "$2a$11$kt3sttPZjh6qXrlOL9UPh.JeigGW4LwMuJw5W87LfrMXDO.vqobhy",
                            Role = 0
                        });
                });

            modelBuilder.Entity("Rental", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ActualEndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("DailyRate")
                        .HasColumnType("numeric");

                    b.Property<Guid>("DeliverymanId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ExpectedEndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("MotoId")
                        .HasColumnType("uuid");

                    b.Property<int>("PlanDays")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<decimal?>("TotalPrice")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("DeliverymanId");

                    b.HasIndex("MotoId");

                    b.ToTable("Rentals");
                });

            modelBuilder.Entity("Rental", b =>
                {
                    b.HasOne("Deliveryman", "Deliveryman")
                        .WithMany()
                        .HasForeignKey("DeliverymanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MotoRent.Domain.Entities.Moto", "Moto")
                        .WithMany()
                        .HasForeignKey("MotoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Deliveryman");

                    b.Navigation("Moto");
                });
#pragma warning restore 612, 618
        }
    }
}
