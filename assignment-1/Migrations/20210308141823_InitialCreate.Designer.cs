﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using assignment_1.Persistent;

namespace assignment_1.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20210308141823_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("assignment_1.DataModels.Address", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long?>("cityid")
                        .HasColumnType("bigint");

                    b.Property<string>("housenumber")
                        .HasColumnType("text");

                    b.Property<string>("street")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("cityid");

                    b.ToTable("addresses");
                });

            modelBuilder.Entity("assignment_1.DataModels.City", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("city")
                        .HasColumnType("text");

                    b.Property<int>("zip")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.ToTable("cities");
                });

            modelBuilder.Entity("assignment_1.DataModels.Address", b =>
                {
                    b.HasOne("assignment_1.DataModels.City", "city")
                        .WithMany("addresses")
                        .HasForeignKey("cityid");

                    b.Navigation("city");
                });

            modelBuilder.Entity("assignment_1.DataModels.City", b =>
                {
                    b.Navigation("addresses");
                });
#pragma warning restore 612, 618
        }
    }
}