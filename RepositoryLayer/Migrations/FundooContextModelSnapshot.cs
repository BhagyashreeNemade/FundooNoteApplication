﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RepositoryLayer.Context;

namespace RepositoryLayer.Migrations
{
    [DbContext(typeof(FundooContext))]
    partial class FundooContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RepositoryLayer.Entity.CollabEntity", b =>
                {
                    b.Property<long>("CollabID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CollabEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Modifiedat")
                        .HasColumnType("datetime2");

                    b.Property<long>("NoteID")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long?>("noteEntityNoteID")
                        .HasColumnType("bigint");

                    b.Property<long?>("userEntityUserId")
                        .HasColumnType("bigint");

                    b.HasKey("CollabID");

                    b.HasIndex("noteEntityNoteID");

                    b.HasIndex("userEntityUserId");

                    b.ToTable("CollabTable");
                });

            modelBuilder.Entity("RepositoryLayer.Entity.NoteEntity", b =>
                {
                    b.Property<long>("NoteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Createat")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsArchive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPin")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTrash")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Modifiedat")
                        .HasColumnType("datetime2");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Reminder")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("NoteID");

                    b.HasIndex("UserId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("RepositoryLayer.Entity.UserEntity", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EmailId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("FundooDbTable");
                });

            modelBuilder.Entity("RepositoryLayer.Entity.CollabEntity", b =>
                {
                    b.HasOne("RepositoryLayer.Entity.NoteEntity", "noteEntity")
                        .WithMany()
                        .HasForeignKey("noteEntityNoteID");

                    b.HasOne("RepositoryLayer.Entity.UserEntity", "userEntity")
                        .WithMany()
                        .HasForeignKey("userEntityUserId");
                });

            modelBuilder.Entity("RepositoryLayer.Entity.NoteEntity", b =>
                {
                    b.HasOne("RepositoryLayer.Entity.UserEntity", "user")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
