﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using gamenet.Model;

namespace gamenet.Migrations
{
    [DbContext(typeof(Core))]
    partial class CoreModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("gamenet.Model.Bill", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateTime");

                    b.Property<int>("Fee");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("gamenet.Model.ReservedStation", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateTime");

                    b.Property<string>("Description");

                    b.Property<string>("StationId")
                        .IsRequired();

                    b.Property<byte>("ToHour");

                    b.Property<byte>("ToMinute");

                    b.HasKey("Id");

                    b.HasIndex("StationId");

                    b.ToTable("ReservedStations");
                });

            modelBuilder.Entity("gamenet.Model.Settings", b =>
                {
                    b.Property<string>("Key")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Value");

                    b.HasKey("Key");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("gamenet.Model.Station", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BaseFee");

                    b.Property<int>("Num");

                    b.Property<bool>("Running");

                    b.Property<DateTime?>("StartTime");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasAlternateKey("Num");

                    b.ToTable("Stations");
                });

            modelBuilder.Entity("gamenet.Model.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Family");

                    b.Property<string>("Father");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<int>("Num");

                    b.Property<string>("Tel");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("gamenet.Model.Bill", b =>
                {
                    b.HasOne("gamenet.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("gamenet.Model.ReservedStation", b =>
                {
                    b.HasOne("gamenet.Model.Station", "Station")
                        .WithMany()
                        .HasForeignKey("StationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
