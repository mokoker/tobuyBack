﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using NpgsqlTypes;
using TB.Db;

namespace TB.Db.Migrations
{
    [DbContext(typeof(ToBuyContext))]
    [Migration("20181216214858_first")]
    partial class first
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("TB.Db.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int?>("ParentId");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("TB.Db.Entities.Ad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<int>("City");

                    b.Property<string>("Message");

                    b.Property<DateTime>("PostDate");

                    b.Property<int>("PosterId");

                    b.Property<NpgsqlTsVector>("SearchVector");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("PosterId");

                    b.HasIndex("SearchVector")
                        .HasAnnotation("Npgsql:IndexMethod", "GIN");

                    b.ToTable("Ads");
                });

            modelBuilder.Entity("TB.Db.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("PassHash");

                    b.Property<string>("UserName");

                    b.Property<int>("UserRoles");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TB.Db.Category", b =>
                {
                    b.HasOne("TB.Db.Category", "Parent")
                        .WithMany("Childs")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("TB.Db.Entities.Ad", b =>
                {
                    b.HasOne("TB.Db.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TB.Db.Entities.User", "Poster")
                        .WithMany("Ads")
                        .HasForeignKey("PosterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
