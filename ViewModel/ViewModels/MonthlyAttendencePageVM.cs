using Backend;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ViewModel.VM_Models;

namespace ViewModel.ViewModels
{
    public partial class MonthlyAttendencePageVM : ObservableObject
    {
        List<MonthlyAttendanceVM> MonthlyAttendances = new();

        public ObservableCollection<MonthlyAttendanceVM> MonthlyAttendanceList = new();


        List<EmployeeVM> Employees = new();

        public ObservableCollection<EmployeeVM> EmployeeList = new();

        [ObservableProperty]
        EmployeeVM selectedEmployee;

        partial void OnSelectedEmployeeChanged(EmployeeVM value)
            => FilterGrid();


        [ObservableProperty]
        DateTimeOffset? selectedDate;

        partial void OnSelectedDateChanged(DateTimeOffset? value)
            => FilterGrid();

        [ObservableProperty]
        string searchText;

        partial void OnSearchTextChanged(string value) => FilterEmp();

        private void FilterEmp()
        {
            EmployeeList.Clear();
            foreach (var emp in Employees.Where(x => FilterationPredicate(x)))
                EmployeeList.Add(emp);
        }


        private bool FilterationPredicate(EmployeeVM x)
        {
            return x.Name.Contains(SearchText, StringComparison.CurrentCultureIgnoreCase);
        }

        public string FilePath { get; set; }


        [RelayCommand]
        void RefreshEmp() => SelectedEmployee = null;

        [RelayCommand]
        void RefreshDate() => SelectedDate = null;

        public MonthlyAttendencePageVM()
        {

            SearchText = string.Empty;
            SelectedDate = null;
            SelectedEmployee = null;

        }



        public async Task LoadData()
        {
            var DbEmployees = await DataAccess.GetEmployees(false, false);
            foreach (var Employee in DbEmployees)
                Employees.Add(new(Employee));
            var DbMonthlyAttendances = await DataAccess.GetMonthlyAttendances();
            foreach (var Attendance in DbMonthlyAttendances)
                MonthlyAttendances.Add(new(Attendance));

            FilterGrid();
            FilterEmp();
        }

        private void FilterGrid()
        {
            List<MonthlyAttendanceVM> attens = MonthlyAttendances;
            if (SelectedEmployee != null)
                attens = attens.Where(x => x.EmployeeId == SelectedEmployee.EmployeeId).ToList();
            if (SelectedDate != null)
            {

                attens = attens.Where(x => DateTime.ParseExact(x.Month, "MMMM", CultureInfo.CurrentCulture).Month == DateOnly.FromDateTime(SelectedDate.Value.LocalDateTime).Month).ToList();
            }
            MonthlyAttendanceList.Clear();
            foreach (var att in attens)
                MonthlyAttendanceList.Add(att);
        }



        public record MAPrint(int ID, string Name, string Month, int Year, int DaysPresent, int WorkHours, int Leaves);
        [RelayCommand]
        async Task SaveToExcel()
        {
            List<MAPrint> prints = new List<MAPrint>();
            Type type = typeof(MAPrint);
            foreach (var item in MonthlyAttendanceList)
            {
                prints.Add(new(item.EmployeeId, item.EmployeeName, item.Month ?? string.Empty, item.Year, item.DaysPresent, item.HoursWorked, item.Leaves));
            }
            await FileHandler.SaveToExcel(FilePath, prints, "MonthlyAttendance", new MemberInfo[]
            {
                type.GetProperty(nameof(MAPrint.ID)),
                type.GetProperty(nameof(MAPrint.Name)),
                type.GetProperty(nameof(MAPrint.Month)),
                type.GetProperty(nameof(MAPrint.Year)),
                type.GetProperty(nameof(MAPrint.DaysPresent)),
                type.GetProperty(nameof(MAPrint.WorkHours)),
                type.GetProperty(nameof (MAPrint.Leaves)),
            });
        }
    }
}
