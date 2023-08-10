using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.SQLServerMigrations
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
                    MONTH(AttendanceDate) AS [Month],
	                YEAR(AttendanceDate) AS [Year],
                    SUM(TotalHoursWorked) AS [NoOfHoursWorked],
                    COUNT(AttendanceDate) AS [DaysPresent],
                    (SELECT COUNT(*) FROM Leaves l WHERE l.EmployeeID = a.EmployeeID AND MONTH(LeaveStartDate) = MONTH(a.AttendanceDate)) AS [Leaves]
                FROM Attendance a
                GROUP BY EmployeeID, YEAR(AttendanceDate), MONTH(AttendanceDate);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(sql: @"DROP TABLE MonthlyAttendance");

        }
    }
}
