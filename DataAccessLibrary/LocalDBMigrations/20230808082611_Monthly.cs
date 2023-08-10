using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.LocalDBMigrations
{
    /// <inheritdoc />
    public partial class Monthly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE VIEW MonthlyAttendance AS
                SELECT 
                    EmployeeID,
					STRFTIME('%m',AttendanceDate) AS [Month],
	                STRFTIME('%Y',AttendanceDate) AS [Year],
                    SUM(TotalHoursWorked) AS [NoOfHoursWorked],
                    COUNT(AttendanceDate) AS [DaysPresent],
                    (SELECT COUNT(*) FROM Leaves l WHERE l.EmployeeID = a.EmployeeID AND strftime('%m',LeaveStartDate) = strftime('%m',a.AttendanceDate)) AS [Leaves]
                FROM Attendance a
                GROUP BY EmployeeID, STRFTIME('%Y',AttendanceDate), STRFTIME('%m',AttendanceDate)");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(sql: @"DROP VIEW main.MonthlyAttendance");

        }
    }
}
