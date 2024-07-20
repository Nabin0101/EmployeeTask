﻿// <auto-generated />
using System;
using EmployeeTask.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EmployeeTask.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EmployeeTask.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("EmployeeId"));

                    b.Property<string>("EmployeeCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDisable")
                        .HasColumnType("boolean");

                    b.Property<int>("PersonId")
                        .HasColumnType("integer");

                    b.Property<int>("PositionId")
                        .HasColumnType("integer");

                    b.Property<double>("Salary")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("EmployeeId");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.ToTable("employee");
                });

            modelBuilder.Entity("EmployeeTask.Models.EmployeeJobHistories", b =>
                {
                    b.Property<int>("EmployeeJobHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("EmployeeJobHistoryId"));

                    b.Property<int>("EmployeeId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("PositionId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("EmployeeJobHistoryId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("PositionId");

                    b.ToTable("employeeJobHistories");
                });

            modelBuilder.Entity("EmployeeTask.Models.EmployeePosition", b =>
                {
                    b.Property<int>("EmployeeId")
                        .HasColumnType("integer");

                    b.Property<int>("PositionId")
                        .HasColumnType("integer");

                    b.HasKey("EmployeeId", "PositionId");

                    b.HasIndex("PositionId");

                    b.ToTable("employeePositions");
                });

            modelBuilder.Entity("EmployeeTask.Models.People", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PersonId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("PersonId");

                    b.ToTable("people");
                });

            modelBuilder.Entity("EmployeeTask.Models.Positions", b =>
                {
                    b.Property<int>("PositionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PositionId"));

                    b.Property<string>("PositionName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("PositionId");

                    b.ToTable("positions");
                });

            modelBuilder.Entity("EmployeeTask.Models.Employee", b =>
                {
                    b.HasOne("EmployeeTask.Models.People", "People")
                        .WithOne("Employee")
                        .HasForeignKey("EmployeeTask.Models.Employee", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("People");
                });

            modelBuilder.Entity("EmployeeTask.Models.EmployeeJobHistories", b =>
                {
                    b.HasOne("EmployeeTask.Models.Employee", "Employee")
                        .WithMany("EmployeeJobHistories")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmployeeTask.Models.Positions", "Position")
                        .WithMany("EmployeeJobHistories")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("EmployeeTask.Models.EmployeePosition", b =>
                {
                    b.HasOne("EmployeeTask.Models.Employee", "Employee")
                        .WithMany("EmployeePositions")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmployeeTask.Models.Positions", "Position")
                        .WithMany("EmployeePositions")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("EmployeeTask.Models.Employee", b =>
                {
                    b.Navigation("EmployeeJobHistories");

                    b.Navigation("EmployeePositions");
                });

            modelBuilder.Entity("EmployeeTask.Models.People", b =>
                {
                    b.Navigation("Employee")
                        .IsRequired();
                });

            modelBuilder.Entity("EmployeeTask.Models.Positions", b =>
                {
                    b.Navigation("EmployeeJobHistories");

                    b.Navigation("EmployeePositions");
                });
#pragma warning restore 612, 618
        }
    }
}
