using Backend;
using Backend.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ViewModel.VM_Models;

namespace ViewModel.ViewModels
{
    public partial class EmpUpdatePageVM : ObservableObject
    {

        public ObservableCollection<CatagoryVM> CatagoryList = new();

        public ObservableCollection<SkillVM> SkillList = new();

        public ObservableCollection<EmployeeVM> EmployeeList = new();

        public ObservableCollection<SkillVM> SelectedSkills = new();

        [ObservableProperty]
        SkillVM selectedSkill;

        [ObservableProperty]
        DateTimeOffset? selectedDate;

        [ObservableProperty]
        DateTimeOffset? leftDate;

        [ObservableProperty]
        int catIndex;

        [ObservableProperty]
        EmployeeVM employee;



        partial void OnEmployeeChanged(EmployeeVM value)
        {

            SelectedSkills.Clear();
            foreach (var skill in value.Skills)
            {
                SelectedSkills.Add(skill);
            }
            CatIndex = CatagoryList.IndexOf(CatagoryList.First(x => x.CatagoryId == value.Catagory.CatagoryId));
            SelectedDate = Employee.Startdate;
            LeftDate = Employee.Leavedate;

        }

        partial void OnSelectedSkillChanged(SkillVM value)
        {
            if (value != null)
            {
                if (!SelectedSkills.Select(skill => skill.SkillId).Contains(value.SkillId))
                {
                    SelectedSkills.Add(value);
                }
            }
        }



        [RelayCommand]
        async Task UpdateEmployee()
        {
            Employee.Skills.FromEnum(SelectedSkills.AsEnumerable());
            Employee.Startdate = SelectedDate.Value.Date;
            if(LeftDate is not null)
            {

            Employee.Leavedate = LeftDate.Value.Date;
            }
            Employee Upd_emp = Employee.ToDTO();
            await DataAccess.UpdateEmployee(Upd_emp);
        }


        public EmpUpdatePageVM()
        {

            employee = new();
            selectedDate = null;
            leftDate = null;
        }

        public async Task LoadData()
        {
            var DbSkills = await DataAccess.GetSkills();
            foreach (var skill in DbSkills)
                SkillList.Add(new(skill));
            var DbCatagories = await DataAccess.GetCatagories();
            foreach (var category in DbCatagories)
                CatagoryList.Add(new(category));
            var DbEmployees = await DataAccess.GetEmployees();
            foreach (var employee in DbEmployees)
                EmployeeList.Add(new(employee));

        }

    }
}
