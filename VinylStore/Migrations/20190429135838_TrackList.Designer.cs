﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VinylStore.Concrete;

namespace VinylStore.Migrations
{
    [DbContext(typeof(VinylStoreDbContext))]
    [Migration("20190429135838_TrackList")]
    partial class TrackList
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("VinylStore.Models.Vinyl", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AlbumName");

                    b.Property<string>("ArtistName");

                    b.Property<string>("Genre");

                    b.Property<string>("ImageUrl");

                    b.Property<decimal>("Price");

                    b.Property<string>("ReleaseYear");

                    b.Property<string>("_trackList")
                        .HasColumnName("TrackList");

                    b.HasKey("Id");

                    b.ToTable("Vinyls");
                });
#pragma warning restore 612, 618
        }
    }
}
