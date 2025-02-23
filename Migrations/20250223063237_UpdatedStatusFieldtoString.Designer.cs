﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api.Data;

#nullable disable

namespace api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250223063237_UpdatedStatusFieldtoString")]
    partial class UpdatedStatusFieldtoString
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InternEducationMapping", b =>
                {
                    b.Property<int>("InternEducationId")
                        .HasColumnType("int");

                    b.Property<int>("InternId")
                        .HasColumnType("int");

                    b.HasKey("InternEducationId", "InternId");

                    b.HasIndex("InternId");

                    b.ToTable("InternEducationMapping");
                });

            modelBuilder.Entity("InternFieldMapping", b =>
                {
                    b.Property<int>("FieldId")
                        .HasColumnType("int");

                    b.Property<int>("InternId")
                        .HasColumnType("int");

                    b.HasKey("FieldId", "InternId");

                    b.HasIndex("InternId");

                    b.ToTable("InternFieldMapping");
                });

            modelBuilder.Entity("api.Entities.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("InternPicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountTypeId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("api.Entities.AccountType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("AccountTypes");
                });

            modelBuilder.Entity("api.Entities.Field", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Fields");
                });

            modelBuilder.Entity("api.Entities.Intern", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EducationId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("HasProfile")
                        .HasColumnType("bit");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Skills")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Specialization")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Interns");
                });

            modelBuilder.Entity("api.Entities.InternEducation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Degree")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("School")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("InternEducations");
                });

            modelBuilder.Entity("api.Entities.InternWorkExperience", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("CompanyLocation")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("InternId")
                        .HasColumnType("int");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("InternId");

                    b.ToTable("InternWorkExperiences");
                });

            modelBuilder.Entity("api.Entities.Recruit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Field")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Recruits");
                });

            modelBuilder.Entity("api.Entities.Swipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("InternId")
                        .HasColumnType("int");

                    b.Property<int>("RecruitId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("SwipeDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("InternId");

                    b.HasIndex("RecruitId");

                    b.ToTable("Swipes");
                });

            modelBuilder.Entity("InternEducationMapping", b =>
                {
                    b.HasOne("api.Entities.InternEducation", null)
                        .WithMany()
                        .HasForeignKey("InternEducationId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("api.Entities.Intern", null)
                        .WithMany()
                        .HasForeignKey("InternId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("InternFieldMapping", b =>
                {
                    b.HasOne("api.Entities.Field", null)
                        .WithMany()
                        .HasForeignKey("FieldId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("api.Entities.Intern", null)
                        .WithMany()
                        .HasForeignKey("InternId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("api.Entities.Account", b =>
                {
                    b.HasOne("api.Entities.AccountType", "AccountType")
                        .WithMany("Accounts")
                        .HasForeignKey("AccountTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AccountType");
                });

            modelBuilder.Entity("api.Entities.Intern", b =>
                {
                    b.HasOne("api.Entities.Account", "Account")
                        .WithMany("Interns")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("api.Entities.InternWorkExperience", b =>
                {
                    b.HasOne("api.Entities.Intern", "Intern")
                        .WithMany("WorkExperiences")
                        .HasForeignKey("InternId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Intern");
                });

            modelBuilder.Entity("api.Entities.Recruit", b =>
                {
                    b.HasOne("api.Entities.Account", "Account")
                        .WithMany("Recruits")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("api.Entities.Swipe", b =>
                {
                    b.HasOne("api.Entities.Intern", "Intern")
                        .WithMany()
                        .HasForeignKey("InternId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("api.Entities.Recruit", "Recruit")
                        .WithMany("Swipes")
                        .HasForeignKey("RecruitId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Intern");

                    b.Navigation("Recruit");
                });

            modelBuilder.Entity("api.Entities.Account", b =>
                {
                    b.Navigation("Interns");

                    b.Navigation("Recruits");
                });

            modelBuilder.Entity("api.Entities.AccountType", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("api.Entities.Intern", b =>
                {
                    b.Navigation("WorkExperiences");
                });

            modelBuilder.Entity("api.Entities.Recruit", b =>
                {
                    b.Navigation("Swipes");
                });
#pragma warning restore 612, 618
        }
    }
}
