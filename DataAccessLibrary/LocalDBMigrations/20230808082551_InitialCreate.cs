using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.LocalDBMigrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Catagories",
                columns: table => new
                {
                    CatagoryID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CatagoryName = table.Column<string>(type: "TEXT", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catagories", x => x.CatagoryID);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    SkillID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SkillName = table.Column<string>(type: "TEXT", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.SkillID);
                });

            migrationBuilder.CreateTable(
                name: "Applicants",
                columns: table => new
                {
                    ApplicantID = table.Column<int>(type: "INTEGER", nullable: false),
                    ApplicantImg = table.Column<byte[]>(type: "BLOB", nullable: true),
                    Name = table.Column<string>(type: "TEXT", unicode: false, maxLength: 100, nullable: false),
                    PhoneNo = table.Column<string>(type: "TEXT", unicode: false, maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "TEXT", unicode: false, maxLength: 100, nullable: true),
                    Catagory = table.Column<int>(type: "INTEGER", nullable: true),
                    CV = table.Column<byte[]>(type: "BLOB", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applicants", x => x.ApplicantID);
                    table.ForeignKey(
                        name: "FK__Applicant__Catag__3D5E1FD2",
                        column: x => x.Catagory,
                        principalTable: "Catagories",
                        principalColumn: "CatagoryID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(type: "INTEGER", nullable: false),
                    EmployeeImg = table.Column<byte[]>(type: "BLOB", nullable: true),
                    Name = table.Column<string>(type: "TEXT", unicode: false, maxLength: 100, nullable: false),
                    FatherName = table.Column<string>(type: "TEXT", unicode: false, maxLength: 100, nullable: true),
                    PhoneNo = table.Column<string>(type: "TEXT", unicode: false, maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "TEXT", unicode: false, maxLength: 100, nullable: true),
                    Catagory = table.Column<int>(type: "INTEGER", nullable: true),
                    Salary = table.Column<decimal>(type: "decimal(10, 2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeID);
                    table.ForeignKey(
                        name: "FK__Employees__Catag__403A8C7D",
                        column: x => x.Catagory,
                        principalTable: "Catagories",
                        principalColumn: "CatagoryID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ApplicantSkills",
                columns: table => new
                {
                    ApplicantID = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Applican__5454985647A1CDEF", x => new { x.ApplicantID, x.SkillID });
                    table.ForeignKey(
                        name: "FK__Applicant__Appli__46E78A0C",
                        column: x => x.ApplicantID,
                        principalTable: "Applicants",
                        principalColumn: "ApplicantID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Applicant__Skill__47DBAE45",
                        column: x => x.SkillID,
                        principalTable: "Skills",
                        principalColumn: "SkillID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attendance",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(type: "INTEGER", nullable: false),
                    AttendanceDate = table.Column<DateTime>(type: "date", nullable: false),
                    CheckInTime = table.Column<TimeSpan>(type: "TEXT", nullable: true),
                    CheckOutTime = table.Column<TimeSpan>(type: "TEXT", nullable: true),
                    TotalHoursWorked = table.Column<int>(type: "INTEGER", nullable: true, computedColumnSql: "ROUND(((JULIANDAY([CheckOutTime]) - JULIANDAY([CheckInTime])) * 24))", stored: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Attendan__77AAB78EE6F5E309", x => new { x.EmployeeID, x.AttendanceDate });
                    table.ForeignKey(
                        name: "FK__Attendanc__Emplo__4AB81AF0",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSkills",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Employee__172A46EF7E01E75E", x => new { x.EmployeeID, x.SkillID });
                    table.ForeignKey(
                        name: "FK__EmployeeS__Emplo__4316F928",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__EmployeeS__Skill__440B1D61",
                        column: x => x.SkillID,
                        principalTable: "Skills",
                        principalColumn: "SkillID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Leaves",
                columns: table => new
                {
                    LeaveID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmployeeID = table.Column<int>(type: "INTEGER", nullable: true),
                    LeaveStartDate = table.Column<DateTime>(type: "date", nullable: true),
                    LeaveEndDate = table.Column<DateTime>(type: "date", nullable: true),
                    Reason = table.Column<string>(type: "TEXT", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Leaves__796DB979D4300E91", x => x.LeaveID);
                    table.ForeignKey(
                        name: "FK__Leaves__Employee__4D94879B",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applicants_Catagory",
                table: "Applicants",
                column: "Catagory");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantSkills_SkillID",
                table: "ApplicantSkills",
                column: "SkillID");

            migrationBuilder.CreateIndex(
                name: "UQ__Catagori__849518B34E6ED5FB",
                table: "Catagories",
                column: "CatagoryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Catagory",
                table: "Employees",
                column: "Catagory");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSkills_SkillID",
                table: "EmployeeSkills",
                column: "SkillID");

            migrationBuilder.CreateIndex(
                name: "IX_Leaves_EmployeeID",
                table: "Leaves",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "UQ__Skills__B63C6571E83C0DF5",
                table: "Skills",
                column: "SkillName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicantSkills");

            migrationBuilder.DropTable(
                name: "Attendance");

            migrationBuilder.DropTable(
                name: "EmployeeSkills");

            migrationBuilder.DropTable(
                name: "Leaves");

            migrationBuilder.DropTable(
                name: "Applicants");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Catagories");
        }
    }
}
