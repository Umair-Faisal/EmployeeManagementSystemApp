using Backend.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Globalization;

namespace ViewModel.VM_Models
{
    public partial class MonthlyAttendanceVM : ObservableObject
    {
        [ObservableProperty]
        int employeeId;

        [ObservableProperty]
        string employeeName;

        [ObservableProperty]
        string? month;

        [ObservableProperty]
        int year;

        [ObservableProperty]
        int hoursWorked;

        [ObservableProperty]
        int daysPresent;

        [ObservableProperty]
        int leaves;


        public MonthlyAttendanceVM()
        {
        }

        public MonthlyAttendanceVM(MonthlyAttendance monthly)
        {
            employeeId = monthly.EmployeeId;
            month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monthly.Month ?? 1);
            hoursWorked = monthly.NoOfHoursWorked ?? 0;
            daysPresent = monthly.DaysPresent ?? 0;
            leaves = monthly.Leaves ?? 0;
            employeeName = string.Empty;
            year = monthly.Year ?? 0;
        }

        public MonthlyAttendance ToDTO()
        {
            return new()
            {
                EmployeeId = EmployeeId,
                Month = DateTime.ParseExact(this.Month, "MMMM", CultureInfo.CurrentCulture).Month,
                NoOfHoursWorked = HoursWorked,
                DaysPresent = DaysPresent,
                Leaves = Leaves,
                Year = Year
            };
        }

    }
}
