﻿// <auto-generated />
using System.Collections.Generic;
using Acquaintances.Bot.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Acquaintances.Bot.DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250103224629_RemoveIdToUser")]
    partial class RemoveIdToUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Acquaintances.Bot.Domain.Entities.Like", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Message")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<long>("RecipientId")
                        .HasColumnType("bigint");

                    b.Property<long>("SenderId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RecipientId");

                    b.ToTable("likes", (string)null);
                });

            modelBuilder.Entity("Acquaintances.Bot.Domain.Entities.Photo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("OwnerId")
                        .HasColumnType("bigint");

                    b.Property<long>("SourceId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("photos", (string)null);
                });

            modelBuilder.Entity("Acquaintances.Bot.Domain.Entities.Profile", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.ComplexProperty<Dictionary<string, object>>("Age", "Acquaintances.Bot.Domain.Entities.Profile.Age#Age", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int>("Value")
                                .HasColumnType("int")
                                .HasColumnName("Age");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("City", "Acquaintances.Bot.Domain.Entities.Profile.City#City", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("City");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Description", "Acquaintances.Bot.Domain.Entities.Profile.Description#Description", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)")
                                .HasColumnName("Description");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Gender", "Acquaintances.Bot.Domain.Entities.Profile.Gender#Gender", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("nvarchar(20)")
                                .HasColumnName("Gender");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Name", "Acquaintances.Bot.Domain.Entities.Profile.Name#Name", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("nvarchar(20)")
                                .HasColumnName("Name");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("PreferredGender", "Acquaintances.Bot.Domain.Entities.Profile.PreferredGender#Gender", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("nvarchar(20)")
                                .HasColumnName("PreferredGender");
                        });

                    b.HasKey("Id");

                    b.ToTable("profiles", (string)null);
                });

            modelBuilder.Entity("Acquaintances.Bot.Domain.Entities.Reciprocity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("AdmirerId")
                        .HasColumnType("bigint");

                    b.Property<long>("RecipientId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RecipientId");

                    b.ToTable("reciprocities", (string)null);
                });

            modelBuilder.Entity("Acquaintances.Bot.Domain.Entities.User", b =>
                {
                    b.Property<long>("ChatId")
                        .HasColumnType("bigint");

                    b.Property<long?>("ProfileId")
                        .HasColumnType("bigint");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ChatId");

                    b.HasIndex("ProfileId")
                        .IsUnique()
                        .HasFilter("[ProfileId] IS NOT NULL");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Acquaintances.Bot.Domain.Entities.Like", b =>
                {
                    b.HasOne("Acquaintances.Bot.Domain.Entities.User", null)
                        .WithMany("AdmirerLikes")
                        .HasForeignKey("RecipientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Acquaintances.Bot.Domain.Entities.Photo", b =>
                {
                    b.HasOne("Acquaintances.Bot.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Acquaintances.Bot.Domain.Entities.Profile", null)
                        .WithMany("Photos")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Acquaintances.Bot.Domain.Entities.Reciprocity", b =>
                {
                    b.HasOne("Acquaintances.Bot.Domain.Entities.User", null)
                        .WithMany("Reciprocities")
                        .HasForeignKey("RecipientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Acquaintances.Bot.Domain.Entities.User", b =>
                {
                    b.HasOne("Acquaintances.Bot.Domain.Entities.Profile", "Profile")
                        .WithOne("Owner")
                        .HasForeignKey("Acquaintances.Bot.Domain.Entities.User", "ProfileId");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("Acquaintances.Bot.Domain.Entities.Profile", b =>
                {
                    b.Navigation("Owner")
                        .IsRequired();

                    b.Navigation("Photos");
                });

            modelBuilder.Entity("Acquaintances.Bot.Domain.Entities.User", b =>
                {
                    b.Navigation("AdmirerLikes");

                    b.Navigation("Reciprocities");
                });
#pragma warning restore 612, 618
        }
    }
}
