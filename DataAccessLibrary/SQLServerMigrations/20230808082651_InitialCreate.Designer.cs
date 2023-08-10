﻿// <auto-generated />
using System;
using Backend.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Backend.SQLServerMigrations
{
    [DbContext(typeof(Database))]
    [Migration("20230808082651_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ApplicantSkill", b =>
                {
                    b.Property<int>("ApplicantId")
                        .HasColumnType("int")
                        .HasColumnName("ApplicantID");

                    b.Property<int>("SkillId")
                        .HasColumnType("int")
                        .HasColumnName("SkillID");

                    b.HasKey("ApplicantId", "SkillId")
                        .HasName("PK__Applican__5454985647A1CDEF");

                    b.HasIndex("SkillId");

                    b.ToTable("ApplicantSkills", (string)null);
                });

            modelBuilder.Entity("Backend.Models.Applicant", b =>
                {
                    b.Property<int>("ApplicantId")
                        .HasColumnType("int")
                        .HasColumnName("ApplicantID");

                    b.Property<byte[]>("ApplicantImg")
                        .HasColumnType("varbinary(max)");

                    b.Property<int?>("Catagory")
                        .HasColumnType("int");

                    b.Property<byte[]>("Cv")
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("CV");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("PhoneNo")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.HasKey("ApplicantId");

                    b.HasIndex("Catagory");

                    b.ToTable("Applicants");
                });

            modelBuilder.Entity("Backend.Models.Attendance", b =>
                {
                    b.Property<int>("EmployeeId")
                        .HasColumnType("int")
                        .HasColumnName("EmployeeID");

                    b.Property<DateTime>("AttendanceDate")
                        .HasColumnType("date");

                    b.Property<TimeSpan?>("CheckInTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan?>("CheckOutTime")
                        .HasColumnType("time");

                    b.Property<int?>("TotalHoursWorked")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("int")
                        .HasComputedColumnSql("(datediff(hour,[CheckInTime],[CheckOutTime]))", true);

                    b.HasKey("EmployeeId", "AttendanceDate")
                        .HasName("PK__Attendan__77AAB78EE6F5E309");

                    b.ToTable("Attendance", (string)null);
                });

            modelBuilder.Entity("Backend.Models.Catagory", b =>
                {
                    b.Property<int>("CatagoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CatagoryID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CatagoryId"));

                    b.Property<string>("CatagoryName")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("CatagoryId");

                    b.HasIndex(new[] { "CatagoryName" }, "UQ__Catagori__849518B34E6ED5FB")
                        .IsUnique()
                        .HasFilter("[CatagoryName] IS NOT NULL");

                    b.ToTable("Catagories");
                });

            modelBuilder.Entity("Backend.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .HasColumnType("int")
                        .HasColumnName("EmployeeID");

                    b.Property<int?>("Catagory")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<byte[]>("EmployeeImg")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("FatherName")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("PhoneNo")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<decimal?>("Salary")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("EmployeeId");

                    b.HasIndex("Catagory");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Backend.Models.Leave", b =>
                {
                    b.Property<int>("LeaveId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("LeaveID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LeaveId"));

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int")
                        .HasColumnName("EmployeeID");

                    b.Property<DateTime?>("LeaveEndDate")
                        .HasColumnType("date");

                    b.Property<DateTime?>("LeaveStartDate")
                        .HasColumnType("date");

                    b.Property<string>("Reason")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.HasKey("LeaveId")
                        .HasName("PK__Leaves__796DB979D4300E91");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Leaves");
                });

            modelBuilder.Entity("Backend.Models.MonthlyAttendance", b =>
                {
                    b.Property<int?>("DaysPresent")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int")
                        .HasColumnName("EmployeeID");

                    b.Property<int?>("Leaves")
                        .HasColumnType("int");

                    b.Property<int?>("Month")
                        .HasColumnType("int");

                    b.Property<int?>("NoOfHoursWorked")
                        .HasColumnType("int");

                    b.Property<int?>("Year")
                        .HasColumnType("int");

                    b.ToTable((string)null);

                    b.ToView("MonthlyAttendance", (string)null);
                });

            modelBuilder.Entity("Backend.Models.Skill", b =>
                {
                    b.Property<int>("SkillId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("SkillID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SkillId"));

                    b.Property<string>("SkillName")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("SkillId");

                    b.HasIndex(new[] { "SkillName" }, "UQ__Skills__B63C6571E83C0DF5")
                        .IsUnique()
                        .HasFilter("[SkillName] IS NOT NULL");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("EmployeeSkill", b =>
                {
                    b.Property<int>("EmployeeId")
                        .HasColumnType("int")
                        .HasColumnName("EmployeeID");

                    b.Property<int>("SkillId")
                        .HasColumnType("int")
                        .HasColumnName("SkillID");

                    b.HasKey("EmployeeId", "SkillId")
                        .HasName("PK__Employee__172A46EF7E01E75E");

                    b.HasIndex("SkillId");

                    b.ToTable("EmployeeSkills", (string)null);
                });

            modelBuilder.Entity("ApplicantSkill", b =>
                {
                    b.HasOne("Backend.Models.Applicant", null)
                        .WithMany()
                        .HasForeignKey("ApplicantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__Applicant__Appli__46E78A0C");

                    b.HasOne("Backend.Models.Skill", null)
                        .WithMany()
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__Applicant__Skill__47DBAE45");
                });

            modelBuilder.Entity("Backend.Models.Applicant", b =>
                {
                    b.HasOne("Backend.Models.Catagory", "CatagoryNavigation")
                        .WithMany("Applicants")
                        .HasForeignKey("Catagory")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK__Applicant__Catag__3D5E1FD2");

                    b.Navigation("CatagoryNavigation");
                });

            modelBuilder.Entity("Backend.Models.Attendance", b =>
                {
                    b.HasOne("Backend.Models.Employee", "Employee")
                        .WithMany("Attendances")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__Attendanc__Emplo__4AB81AF0");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Backend.Models.Employee", b =>
                {
                    b.HasOne("Backend.Models.Catagory", "CatagoryNavigation")
                        .WithMany("Employees")
                        .HasForeignKey("Catagory")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK__Employees__Catag__403A8C7D");

                    b.Navigation("CatagoryNavigation");
                });

            modelBuilder.Entity("Backend.Models.Leave", b =>
                {
                    b.HasOne("Backend.Models.Employee", "Employee")
                        .WithMany("Leaves")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__Leaves__Employee__4D94879B");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("EmployeeSkill", b =>
                {
                    b.HasOne("Backend.Models.Employee", null)
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__EmployeeS__Emplo__4316F928");

                    b.HasOne("Backend.Models.Skill", null)
                        .WithMany()
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__EmployeeS__Skill__440B1D61");
                });

            modelBuilder.Entity("Backend.Models.Catagory", b =>
                {
                    b.Navigation("Applicants");

                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Backend.Models.Employee", b =>
                {
                    b.Navigation("Attendances");

                    b.Navigation("Leaves");
                });
#pragma warning restore 612, 618
        }
    }
}