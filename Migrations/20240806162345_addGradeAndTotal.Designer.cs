﻿// <auto-generated />
using Grading_System_Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Grading_System_Backend.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20240806162345_addGradeAndTotal")]
    partial class addGradeAndTotal
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Grading_System_Backend.Models.AcademicYear", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("academicYears");
                });

            modelBuilder.Entity("Grading_System_Backend.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AcademicYearId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NationalId")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("TotalGrade")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.HasKey("Id");

                    b.HasIndex("AcademicYearId");

                    b.HasIndex("NationalId")
                        .IsUnique();

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Grading_System_Backend.Models.StudentSubjects", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("Grade")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("Subjectid")
                        .HasColumnType("int");

                    b.Property<float>("Term1")
                        .HasColumnType("real");

                    b.Property<float>("Term2")
                        .HasColumnType("real");

                    b.Property<float>("Total")
                        .HasColumnType("real");

                    b.HasKey("id");

                    b.HasIndex("StudentId");

                    b.HasIndex("Subjectid");

                    b.ToTable("StudentSubjects");
                });

            modelBuilder.Entity("Grading_System_Backend.Models.Subject", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("MaximumDegree")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("Grading_System_Backend.Models.Student", b =>
                {
                    b.HasOne("Grading_System_Backend.Models.AcademicYear", "AcademicYear")
                        .WithMany("students")
                        .HasForeignKey("AcademicYearId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AcademicYear");
                });

            modelBuilder.Entity("Grading_System_Backend.Models.StudentSubjects", b =>
                {
                    b.HasOne("Grading_System_Backend.Models.Student", "Student")
                        .WithMany("StudentSubjects")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Grading_System_Backend.Models.Subject", "Subject")
                        .WithMany("StudentSubjects")
                        .HasForeignKey("Subjectid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("Grading_System_Backend.Models.AcademicYear", b =>
                {
                    b.Navigation("students");
                });

            modelBuilder.Entity("Grading_System_Backend.Models.Student", b =>
                {
                    b.Navigation("StudentSubjects");
                });

            modelBuilder.Entity("Grading_System_Backend.Models.Subject", b =>
                {
                    b.Navigation("StudentSubjects");
                });
#pragma warning restore 612, 618
        }
    }
}
