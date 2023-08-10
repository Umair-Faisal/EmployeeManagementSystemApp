using Backend.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace ViewModel.VM_Models
{
    public partial class AttendenceVM : ObservableObject
    {
        [ObservableProperty]
        int employeeId;

        [ObservableProperty]
        string employeeName;

        [ObservableProperty]
        DateOnly attendenceDate;

        [ObservableProperty]
        TimeSpan? checkInTime;

        [ObservableProperty]
        TimeSpan? checkOutTime;

        [ObservableProperty]
        int hoursWorked;


        public override string ToString()
        {
            return $"{this.EmployeeId} - {this.AttendenceDate.ToString("MM/dd/yyyy")}";
        }

        public AttendenceVM()
        {

        }

        public AttendenceVM(Attendance attendance)
        {
            EmployeeId = attendance.EmployeeId;
            AttendenceDate = DateOnly.FromDateTime(attendance.AttendanceDate);
            HoursWorked = attendance.TotalHoursWorked ?? 0;
            CheckInTime = attendance.CheckInTime;
            checkOutTime = attendance.CheckOutTime;
            if (attendance.Employee is not null)
            {
                employeeName = attendance.Employee.Name ?? string.Empty;
            }

        }

        public Attendance ToDTO()
        {
            return new()
            {
                EmployeeId = this.EmployeeId,
                AttendanceDate = this.AttendenceDate.ToDateTime(TimeOnly.FromTimeSpan(CheckInTime ?? TimeSpan.MinValue)),
                TotalHoursWorked = HoursWorked,
                CheckInTime = this.CheckInTime,
                CheckOutTime = this.CheckOutTime,
            };
        }

    }
}
