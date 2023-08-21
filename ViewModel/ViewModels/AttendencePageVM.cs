using Backend;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ViewModel.VM_Models;

namespace ViewModel.ViewModels
{
    public enum DateType
    {
        Day,
        Month,
        Year
    }

    public partial class AttendencePageVM : ObservableObject
    {


        List<AttendenceVM> Attendances = new();

        public ObservableCollection<AttendenceVM> AttendanceList = new();


        List<EmployeeVM> Employees = new();

        public ObservableCollection<EmployeeVM> EmployeeList = new();

        [ObservableProperty]
        EmployeeVM? selectedEmployee;
        partial void OnSelectedEmployeeChanged(EmployeeVM? value)
            => FilterDataGrid();

        public IEnumerable<DateType> DateFilters = Enum.GetValues(typeof(DateType)).Cast<DateType>();
        
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ByDay))]
        [NotifyPropertyChangedFor(nameof(ByMonth))]
        DateType datefilter;


        public bool ByDay => Datefilter == DateType.Day;
        public bool ByMonth => Datefilter != DateType.Year;
        public bool ByYear = true;


        [ObservableProperty]
        DateTimeOffset? selectedDate;

        partial void OnSelectedDateChanged(DateTimeOffset? value)
            => FilterDataGrid();

        [ObservableProperty]
        TimeSpan? checkInTime;

        [ObservableProperty]
        TimeSpan? checkOutTime;

        [ObservableProperty]
        string searchText;

        partial void OnSearchTextChanged(string value)
        {
            FilterEmp();

        }



        [RelayCommand]
        void RefreshEmp() => SelectedEmployee = null;

        [RelayCommand]
        void RefreshDate() => SelectedDate = null;

        [ObservableProperty]
        DateTimeOffset? date;


        [RelayCommand]
        async Task CheckIn()
        {
            if (CheckInTime is not null)
            {

                await DataAccess.CheckIn(CheckInTime.Value, Date.Value.ToLocalTime().DateTime, SelectedEmployee.EmployeeId);
                await RefreshAttendances();
            }
        }

        private async Task RefreshAttendances()
        {
            Attendances.Clear();
            var DbAtt = await DataAccess.GetAttendances();
            foreach (var attendance in DbAtt)
                Attendances.Add(new(attendance));
            foreach (var Attendance in Attendances)
                Attendance.EmployeeName = Employees.Where(x => x.EmployeeId == Attendance.EmployeeId).Select(x => x.Name).First();
            FilterDataGrid();
        }

        [RelayCommand]
        async Task CheckOut()
        {
            if (CheckOutTime is not null)
            {
                await DataAccess.CheckOut(CheckOutTime.Value, Date.Value.ToLocalTime().DateTime, SelectedEmployee.EmployeeId);
                await RefreshAttendances();
            }
        }

        public AttendencePageVM()
        {

            SearchText = string.Empty;
            selectedDate = null;
            selectedEmployee = null;
            date = DateTimeOffset.Now;
            CheckInTime = DateTime.Now.TimeOfDay;
            checkOutTime = DateTime.Now.TimeOfDay;
        }



        public async Task LoadData()
        {
            var DbEmp = await DataAccess.GetEmployees(false, false);
            foreach (var employee in DbEmp)
                Employees.Add(new(employee));
            await RefreshAttendances();

            FilterDataGrid();
            FilterEmp();
        }

        private void FilterEmp()
        {
            EmployeeList.Clear();
            foreach (var emp in Employees.Where(x => FilterationPredicate(x)))
                EmployeeList.Add(emp);
        }

        void FilterDataGrid()
        {
            List<AttendenceVM> attens = new(Attendances);
            if (SelectedEmployee != null)
                attens = attens.Where(x => x.EmployeeId == SelectedEmployee.EmployeeId).ToList();
            if (SelectedDate != null)
            {
                if (Datefilter == DateType.Day)
                    attens = attens.Where(x => x.AttendenceDate == DateOnly.FromDateTime(SelectedDate.Value.LocalDateTime)).ToList();
                else if (Datefilter == DateType.Month)
                    attens = attens.Where(x => x.AttendenceDate.Month == DateOnly.FromDateTime(SelectedDate.Value.LocalDateTime).Month).ToList();
                else if (Datefilter == DateType.Year)
                    attens = attens.Where(x => x.AttendenceDate.Year == DateOnly.FromDateTime(SelectedDate.Value.LocalDateTime).Year).ToList();

            }
            AttendanceList.Clear();
            foreach (var att in attens)
                AttendanceList.Add(att);
        }


        private bool FilterationPredicate(EmployeeVM x)
        {
            return x.Name.Contains(SearchText, StringComparison.CurrentCultureIgnoreCase);
        }
        public record AttendencePrint(int ID, string EmployeeName, string Date, string StartTime, string EndTime, int HoursWorked);

        [ObservableProperty]
        string excelFile;

        [RelayCommand]
        async Task SaveToExcel()
        {
            List<AttendencePrint> attendences = new();

            foreach (var att in AttendanceList)
            {
                string CheckIn = att.CheckInTime.Value.ToString("g");
                string CheckOut = att.CheckOutTime.Value.ToString("g");
                string Date = att.AttendenceDate.ToShortDateString();
                attendences.Add(new(att.EmployeeId, att.EmployeeName, Date, CheckIn, CheckOut, att.HoursWorked));
            }
            var type = typeof(AttendencePrint);


            await FileHandler.SaveToExcel(ExcelFile, attendences, "Attedances",
                new MemberInfo[]
                {
                    type.GetProperty("ID"),
                    type.GetProperty("EmployeeName"),
                    type.GetProperty("Date"),
                    type.GetProperty("StartTime"),
                    type.GetProperty("EndTime"),
                    type.GetProperty("HoursWorked")
                });
        }


    }
}
