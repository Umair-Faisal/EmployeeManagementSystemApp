using Backend;
using Backend.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ViewModel.VM_Models;

namespace ViewModel.ViewModels;

public partial class EmpAddPageVM : ObservableObject
{

    public ObservableCollection<CatagoryVM> CatagoryList = new();

    public ObservableCollection<SkillVM> SkillList = new();

    public ObservableCollection<SkillVM> SelectedSkills = new();

    [ObservableProperty]
    SkillVM selectedSkill;

    [ObservableProperty]
    DateTimeOffset selectedDate;


    [ObservableProperty]
    EmployeeVM employee;


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
    async Task AddEmployee()
    {
        Employee.Skills.FromEnum(SelectedSkills.AsEnumerable());
        Employee.Startdate = SelectedDate.DateTime.Date;
        Employee added_emp = Employee.ToDTO();
        await DataAccess.AddEmployee(added_emp);
    }


    public EmpAddPageVM()
    {

        employee = new();
    }

    public async Task LoadData()
    {
        var _CatagoryList = await DataAccess.GetCatagories();
        var _SkillList = await DataAccess.GetSkills();
        foreach (var skill in _SkillList)
            SkillList.Add(new(skill));
        foreach (var catagory in _CatagoryList)
            CatagoryList.Add(new(catagory));



    }

}
