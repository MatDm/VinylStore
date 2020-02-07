﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PaymentApp.DAL;

namespace PaymentApp.DAL.Migrations
{
    [DbContext(typeof(PaymentAppDbContext))]
    [Migration("20200205130432_PaymentDbContext")]
    partial class PaymentDbContext
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PaymentApp.DAL.Entities.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CallbackUrl");

                    b.Property<string>("Name");

                    b.Property<string>("Token");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("PaymentApp.DAL.Entities.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClientId");

                    b.Property<string>("Country");

                    b.Property<DateTime>("CreationDate");

                    b.Property<bool>("IsPayed");

                    b.Property<string>("Name");

                    b.Property<DateTime>("PaymentDate");

                    b.Property<string>("PostalCode");

                    b.Property<string>("Street");

                    b.Property<string>("StreetNumber");

                    b.Property<double>("Total");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("PaymentApp.DAL.Entities.Transaction", b =>
                {
                    b.HasOne("PaymentApp.DAL.Entities.Client", "Client")
                        .WithMany("Transactions")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
