﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SolarSystemEncyclopedia.Data;

#nullable disable

namespace SolarSystemEncyclopedia.Migrations
{
    [DbContext(typeof(SolarSystemContext))]
    [Migration("20240229130613_add-mainStarID")]
    partial class addmainStarID
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SolarSystemEncyclopedia.Models.CosmicObject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double?>("Density")
                        .HasColumnType("double");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Mass")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long>("Radius")
                        .HasColumnType("bigint");

                    b.Property<string>("SurfaceArea")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Volume")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("CosmicObject");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("SolarSystemEncyclopedia.Models.Moon", b =>
                {
                    b.HasBaseType("SolarSystemEncyclopedia.Models.CosmicObject");

                    b.Property<double?>("AtmosphericPressure")
                        .HasColumnType("double");

                    b.Property<double?>("AverageTemperature")
                        .HasColumnType("double");

                    b.Property<bool>("HasAtmosphere")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("MainPlanetId")
                        .HasColumnType("int");

                    b.HasIndex("MainPlanetId");

                    b.ToTable("Moon");
                });

            modelBuilder.Entity("SolarSystemEncyclopedia.Models.Planet", b =>
                {
                    b.HasBaseType("SolarSystemEncyclopedia.Models.CosmicObject");

                    b.Property<double?>("AtmosphericPressure")
                        .HasColumnType("double");

                    b.Property<double?>("AverageTemperature")
                        .HasColumnType("double");

                    b.Property<bool>("HasAtmosphere")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("MainStarId1")
                        .HasColumnType("int");

                    b.HasIndex("MainStarId1");

                    b.ToTable("Planet");
                });

            modelBuilder.Entity("SolarSystemEncyclopedia.Models.Star", b =>
                {
                    b.HasBaseType("SolarSystemEncyclopedia.Models.CosmicObject");

                    b.Property<bool>("HasPlanets")
                        .HasColumnType("tinyint(1)");

                    b.Property<double>("Luminosity")
                        .HasColumnType("double");

                    b.Property<string>("SpectralClass")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Temperature")
                        .HasColumnType("double");

                    b.ToTable("Star");
                });

            modelBuilder.Entity("SolarSystemEncyclopedia.Models.Moon", b =>
                {
                    b.HasOne("SolarSystemEncyclopedia.Models.CosmicObject", null)
                        .WithOne()
                        .HasForeignKey("SolarSystemEncyclopedia.Models.Moon", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SolarSystemEncyclopedia.Models.Planet", "MainPlanet")
                        .WithMany("Moons")
                        .HasForeignKey("MainPlanetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MainPlanet");
                });

            modelBuilder.Entity("SolarSystemEncyclopedia.Models.Planet", b =>
                {
                    b.HasOne("SolarSystemEncyclopedia.Models.CosmicObject", null)
                        .WithOne()
                        .HasForeignKey("SolarSystemEncyclopedia.Models.Planet", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SolarSystemEncyclopedia.Models.Star", "MainStar")
                        .WithMany("Planets")
                        .HasForeignKey("MainStarId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MainStar");
                });

            modelBuilder.Entity("SolarSystemEncyclopedia.Models.Star", b =>
                {
                    b.HasOne("SolarSystemEncyclopedia.Models.CosmicObject", null)
                        .WithOne()
                        .HasForeignKey("SolarSystemEncyclopedia.Models.Star", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SolarSystemEncyclopedia.Models.Planet", b =>
                {
                    b.Navigation("Moons");
                });

            modelBuilder.Entity("SolarSystemEncyclopedia.Models.Star", b =>
                {
                    b.Navigation("Planets");
                });
#pragma warning restore 612, 618
        }
    }
}
