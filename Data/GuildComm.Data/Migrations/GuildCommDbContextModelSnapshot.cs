﻿// <auto-generated />
using System;
using GuildComm.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GuildComm.Data.Migrations
{
    [DbContext(typeof(GuildCommDbContext))]
    partial class GuildCommDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GuildComm.Data.Models.Application", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Experience")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GuildId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MainCharacterName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("GuildId");

                    b.HasIndex("UserId");

                    b.ToTable("Application");
                });

            modelBuilder.Entity("GuildComm.Data.Models.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Class")
                        .HasColumnType("int");

                    b.Property<string>("GuildId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ItemLevel")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("MemberId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RealmId")
                        .HasColumnType("int");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("GuildId");

                    b.HasIndex("RealmId");

                    b.HasIndex("UserId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("GuildComm.Data.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EventType")
                        .HasColumnType("int");

                    b.Property<string>("GuildId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GuildId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("GuildComm.Data.Models.EventParticipant", b =>
                {
                    b.Property<string>("ParticipantId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.HasKey("ParticipantId", "EventId");

                    b.HasIndex("EventId");

                    b.ToTable("EventParticipants");
                });

            modelBuilder.Entity("GuildComm.Data.Models.Guild", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("GuildMaster")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RealmId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RealmId");

                    b.ToTable("Guilds");
                });

            modelBuilder.Entity("GuildComm.Data.Models.GuildCommUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("GuildId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("GuildId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("GuildComm.Data.Models.Member", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.Property<string>("GuildId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Info")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("MemberSince")
                        .HasColumnType("datetime2");

                    b.Property<int>("Rank")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId")
                        .IsUnique();

                    b.HasIndex("GuildId");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("GuildComm.Data.Models.Realm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RealmType")
                        .HasColumnType("int");

                    b.Property<int>("Region")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Realms");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("GuildComm.Data.Models.Application", b =>
                {
                    b.HasOne("GuildComm.Data.Models.Guild", "Guild")
                        .WithMany("Applications")
                        .HasForeignKey("GuildId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("GuildComm.Data.Models.GuildCommUser", "User")
                        .WithMany("Applications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("GuildComm.Data.Models.Character", b =>
                {
                    b.HasOne("GuildComm.Data.Models.Guild", "Guild")
                        .WithMany("Characters")
                        .HasForeignKey("GuildId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("GuildComm.Data.Models.Realm", "Realm")
                        .WithMany("Characters")
                        .HasForeignKey("RealmId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GuildComm.Data.Models.GuildCommUser", "User")
                        .WithMany("Characters")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("GuildComm.Data.Models.Event", b =>
                {
                    b.HasOne("GuildComm.Data.Models.Guild", "Guild")
                        .WithMany("Events")
                        .HasForeignKey("GuildId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("GuildComm.Data.Models.EventParticipant", b =>
                {
                    b.HasOne("GuildComm.Data.Models.Event", "Event")
                        .WithMany("Participants")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GuildComm.Data.Models.Member", "Participant")
                        .WithMany("Events")
                        .HasForeignKey("ParticipantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("GuildComm.Data.Models.Guild", b =>
                {
                    b.HasOne("GuildComm.Data.Models.Realm", "Realm")
                        .WithMany("Guilds")
                        .HasForeignKey("RealmId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("GuildComm.Data.Models.GuildCommUser", b =>
                {
                    b.HasOne("GuildComm.Data.Models.Guild", "Guild")
                        .WithMany()
                        .HasForeignKey("GuildId");
                });

            modelBuilder.Entity("GuildComm.Data.Models.Member", b =>
                {
                    b.HasOne("GuildComm.Data.Models.Character", "Character")
                        .WithOne("Member")
                        .HasForeignKey("GuildComm.Data.Models.Member", "CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GuildComm.Data.Models.Guild", "Guild")
                        .WithMany("Members")
                        .HasForeignKey("GuildId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("GuildComm.Data.Models.GuildCommUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("GuildComm.Data.Models.GuildCommUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GuildComm.Data.Models.GuildCommUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("GuildComm.Data.Models.GuildCommUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
