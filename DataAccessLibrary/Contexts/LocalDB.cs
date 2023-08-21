using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Contexts
{
    public partial class LocalDB: DbContext
    {
        public LocalDB()
        {
        }

        public LocalDB(DbContextOptions options)
            : base(options)
        {
        }

        public virtual DbSet<Applicant> Applicants { get; set; }
        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<Catagory> Catagories { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Leave> Leaves { get; set; }
        public virtual DbSet<MonthlyAttendance> MonthlyAttendances { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=C:\programdata\EmployeeManagementSystem\database.db");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Applicant>(entity =>
            {
                entity.Property(e => e.ApplicantId)
                    .ValueGeneratedNever()
                    .HasColumnName("ApplicantID");

                entity.Property(e => e.Cv).HasColumnName("CV");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.CatagoryNavigation)
                    .WithMany(p => p.Applicants)
                    .HasForeignKey(d => d.Catagory)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__Applicant__Catag__3D5E1FD2");

                entity.HasMany(d => d.Skills)
                    .WithMany(p => p.Applicants)
                    .UsingEntity<Dictionary<string, object>>(
                        "ApplicantSkill",
                        l => l.HasOne<Skill>().WithMany().HasForeignKey("SkillId").HasConstraintName("FK__Applicant__Skill__47DBAE45"),
                        r => r.HasOne<Applicant>().WithMany().HasForeignKey("ApplicantId").HasConstraintName("FK__Applicant__Appli__46E78A0C"),
                        j =>
                        {
                            j.HasKey("ApplicantId", "SkillId").HasName("PK__Applican__5454985647A1CDEF");

                            j.ToTable("ApplicantSkills");

                            j.IndexerProperty<int>("ApplicantId").HasColumnName("ApplicantID");

                            j.IndexerProperty<int>("SkillId").HasColumnName("SkillID");
                        });
            });

            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeId, e.AttendanceDate })
                    .HasName("PK__Attendan__77AAB78EE6F5E309");

                entity.ToTable("Attendance");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.AttendanceDate).HasColumnType("date");

                entity.Property(e => e.TotalHoursWorked).HasComputedColumnSql("ROUND((JULIANDAY([CheckInTime],[CheckOutTime]) * 24))", true);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__Attendanc__Emplo__4AB81AF0");
                
            });

            modelBuilder.Entity<Catagory>(entity =>
            {
                entity.HasIndex(e => e.CatagoryName, "UQ__Catagori__849518B34E6ED5FB")
                    .IsUnique();

                entity.Property(e => e.CatagoryId).HasColumnName("CatagoryID");

                entity.Property(e => e.CatagoryName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.EmployeeId)
                    .ValueGeneratedNever()
                    .HasColumnName("EmployeeID");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FatherName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Salary).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.CatagoryNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.Catagory)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__Employees__Catag__403A8C7D");

                entity.HasMany(d => d.Skills)
                    .WithMany(p => p.Employees)
                    .UsingEntity<Dictionary<string, object>>(
                        "EmployeeSkill",
                        l => l.HasOne<Skill>().WithMany().HasForeignKey("SkillId").HasConstraintName("FK__EmployeeS__Skill__440B1D61"),
                        r => r.HasOne<Employee>().WithMany().HasForeignKey("EmployeeId").HasConstraintName("FK__EmployeeS__Emplo__4316F928"),
                        j =>
                        {
                            j.HasKey("EmployeeId", "SkillId").HasName("PK__Employee__172A46EF7E01E75E");

                            j.ToTable("EmployeeSkills");

                            j.IndexerProperty<int>("EmployeeId").HasColumnName("EmployeeID");

                            j.IndexerProperty<int>("SkillId").HasColumnName("SkillID");
                        });
            });

            modelBuilder.Entity<Leave>(entity =>
            {
                entity.HasKey(e => e.LeaveId)
                    .HasName("PK__Leaves__796DB979D4300E91");

                entity.Property(e => e.LeaveId).HasColumnName("LeaveID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.LeaveEndDate).HasColumnType("date");

                entity.Property(e => e.LeaveStartDate).HasColumnType("date");

                entity.Property(e => e.Reason).IsUnicode(false);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Leaves)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Leaves__Employee__4D94879B");
            });

            modelBuilder.Entity<MonthlyAttendance>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("MonthlyAttendance");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.HasIndex(e => e.SkillName, "UQ__Skills__B63C6571E83C0DF5")
                    .IsUnique();

                entity.Property(e => e.SkillId).HasColumnName("SkillID");

                entity.Property(e => e.SkillName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
