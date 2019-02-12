﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using NpgsqlTypes;
using TB.Db;

namespace TB.Db.Migrations
{
    [DbContext(typeof(ToBuyContext))]
    partial class ToBuyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.HasIndex("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("TB.Db.Entities.Ad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<int>("City");

                    b.Property<string>("IpAddress");

                    b.Property<string>("Message");

                    b.Property<DateTime>("PostDate");

                    b.Property<int>("PosterId");

                    b.Property<float>("Price");

                    b.Property<NpgsqlTsVector>("SearchVector");

                    b.Property<int>("State");

                    b.Property<string>("Title");

                    b.Property<bool>("ToSell");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("PosterId");

                    b.HasIndex("SearchVector")
                        .HasAnnotation("Npgsql:IndexMethod", "GIN");

                    b.ToTable("Ads");
                });

            modelBuilder.Entity("TB.Db.Entities.Mail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("HtmlMessage");

                    b.Property<int>("MType");

                    b.Property<string>("SenderAddress");

                    b.Property<string>("Subject");

                    b.Property<string>("Textmessage");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("Mails");
                });

            modelBuilder.Entity("TB.Db.Entities.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("IpAddress");

                    b.Property<int>("ReceiverId");

                    b.Property<int>("ReceiverStatus");

                    b.Property<int>("SenderId");

                    b.Property<int>("SenderStatus");

                    b.Property<DateTime>("SentDate");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("TB.Db.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("IpAddress");

                    b.Property<string>("MailSecret");

                    b.Property<string>("PassHash");

                    b.Property<DateTime>("RegistrationDate");

                    b.Property<DateTime>("SecretDate");

                    b.Property<string>("UserName");

                    b.Property<int>("UserRoles");

                    b.HasKey("Id");

                    b.HasIndex("Id");

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

            modelBuilder.Entity("TB.Db.Entities.Message", b =>
                {
                    b.HasOne("TB.Db.Entities.User", "Receiver")
                        .WithMany()
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TB.Db.Entities.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
