﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PZ_18.Data;

#nullable disable

namespace PZ_18.Migrations
{
    [DbContext(typeof(CoreContext))]
    partial class CoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PZ_18.Models.Note", b =>
            {
                b.Property<int>("NoteID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NoteID"));

                b.Property<int?>("TechnicianID")
                    .HasColumnType("int");

                b.Property<string>("NoteText")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<int?>("RequestID")
                    .HasColumnType("int");

                b.HasKey("NoteID");

                b.HasIndex("TechnicianID");

                b.HasIndex("RequestID");

                b.ToTable("Notes", (string)null);
            });

            modelBuilder.Entity("PZ_18.Models.ApplianceCategory", b =>
            {
                b.Property<int>("ApplianceID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ApplianceID"));

                b.Property<string>("ApplianceName")
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnType("nvarchar(255)");

                b.HasKey("ApplianceID");

                b.ToTable("ApplianceCategories", (string)null);
            });

            modelBuilder.Entity("PZ_18.Models.RepairComponent", b =>
            {
                b.Property<int>("ComponentID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ComponentID"));

                b.Property<string>("ComponentName")
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnType("nvarchar(255)");

                b.Property<int>("ComponentQuantity")
                    .HasColumnType("int");

                b.Property<int>("RequestID")
                    .HasColumnType("int");

                b.HasKey("ComponentID");

                b.HasIndex("RequestID");

                b.ToTable("RepairComponents", (string)null);
            });

            modelBuilder.Entity("PZ_18.Models.RepairRequest", b =>
            {
                b.Property<int>("RequestID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RequestID"));

                b.Property<string>("CustomerName")
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnType("nvarchar(255)");

                b.Property<int?>("CustomerID")
                    .HasColumnType("int");

                b.Property<string>("CustomerPhone")
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnType("nvarchar(15)");

                b.Property<DateTime?>("ResolutionDate")
                    .HasColumnType("datetime2");

                b.Property<string>("ApplianceModel")
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnType("nvarchar(255)");

                b.Property<int?>("TechnicianID")
                    .HasColumnType("int");

                b.Property<string>("IssueDescription")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Status")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar(50)");

                b.Property<DateTime>("CreationDate")
                    .HasColumnType("datetime2");

                b.Property<int>("ApplianceID")
                    .HasColumnType("int");

                b.HasKey("RequestID");

                b.HasIndex("CustomerID");

                b.HasIndex("TechnicianID");

                b.HasIndex("ApplianceID");

                b.ToTable("RepairRequests", (string)null);
            });

            modelBuilder.Entity("PZ_18.Models.Account", b =>
            {
                b.Property<int>("AccountID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccountID"));

                b.Property<string>("FullName")
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnType("nvarchar(255)");

                b.Property<string>("Username")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar(50)");

                b.Property<string>("UserPassword")
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnType("nvarchar(255)");

                b.Property<string>("PhoneNumber")
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnType("nvarchar(15)");

                b.Property<int>("CategoryID")
                    .HasColumnType("int");

                b.HasKey("AccountID");

                b.HasIndex("CategoryID");

                b.ToTable("Accounts", (string)null);
            });

            modelBuilder.Entity("PZ_18.Models.UserCategory", b =>
            {
                b.Property<int>("CategoryID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryID"));

                b.Property<string>("CategoryName")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar(50)");

                b.HasKey("CategoryID");

                b.ToTable("UserCategories", (string)null);
            });

            modelBuilder.Entity("PZ_18.Models.Note", b =>
            {
                b.HasOne("PZ_18.Models.Account", "Technician")
                    .WithMany()
                    .HasForeignKey("TechnicianID");

                b.HasOne("PZ_18.Models.RepairRequest", "RepairRequest")
                    .WithMany()
                    .HasForeignKey("RequestID");

                b.Navigation("Technician");

                b.Navigation("RepairRequest");
            });

            modelBuilder.Entity("PZ_18.Models.RepairComponent", b =>
            {
                b.HasOne("PZ_18.Models.RepairRequest", "RepairRequest")
                    .WithMany()
                    .HasForeignKey("RequestID")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("RepairRequest");
            });

            modelBuilder.Entity("PZ_18.Models.RepairRequest", b =>
            {
                b.HasOne("PZ_18.Models.Account", "Customer")
                    .WithMany()
                    .HasForeignKey("CustomerID");

                b.HasOne("PZ_18.Models.Account", "Technician")
                    .WithMany()
                    .HasForeignKey("TechnicianID");

                b.HasOne("PZ_18.Models.ApplianceCategory", "ApplianceCategory")
                    .WithMany()
                    .HasForeignKey("ApplianceID")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Customer");

                b.Navigation("ApplianceCategory");

                b.Navigation("Technician");
            });

            modelBuilder.Entity("PZ_18.Models.Account", b =>
            {
                b.HasOne("PZ_18.Models.UserCategory", "UserCategory")
                    .WithMany()
                    .HasForeignKey("CategoryID")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("UserCategory");
            });
#pragma warning restore 612, 618
        }
    }
}