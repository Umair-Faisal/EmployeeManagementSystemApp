using Backend.Collections;
using Backend.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace ViewModel.VM_Models;

public partial class EmployeeVM : ObservableObject
{
    [ObservableProperty]
    SoftwareBitmapSource image;

    [ObservableProperty]
    int employeeId;

    [ObservableProperty]
    byte[]? employeeImg;

    [ObservableProperty]
    string name;

    [ObservableProperty]
    string fathername;

    [ObservableProperty]
    string phone;

    [ObservableProperty]
    string email;

    [ObservableProperty]
    decimal salary;

    [ObservableProperty]
    CustomCollection<Attendance> attendances;

    [ObservableProperty]
    CatagoryVM catagory;

    [ObservableProperty]
    DateTime? startdate;


    [ObservableProperty]
    DateTime? leavedate;

    public bool HasLeft => Leavedate == null;

    [ObservableProperty]
    CustomCollection<Leave> leaves;


    [ObservableProperty]
    CustomCollection<SkillVM> skills;
    public override string ToString()
    {
        return this.Name;
    }

    public EmployeeVM()
    {
        this.employeeImg = null;
        this.EmployeeId = 0;
        this.name = string.Empty;
        fathername = string.Empty;
        phone = string.Empty;
        email = string.Empty;
        salary = 0;
        catagory = new CatagoryVM();
        skills = new CustomCollection<SkillVM>();
        leaves = new CustomCollection<Leave>();
        attendances = new CustomCollection<Attendance>();
        employeeImg = new byte[0];
    }

    public EmployeeVM(Employee employee)
    {
        employeeImg = employee.EmployeeImg ?? null;
        employeeId = employee.EmployeeId;
        name = employee.Name;
        fathername = employee.FatherName ?? string.Empty;
        phone = employee.PhoneNo ?? string.Empty;
        email = employee.Email ?? string.Empty;
        salary = employee.Salary ?? 0;
        startdate = employee.StartingDate;
        leavedate = employee.LeavingDate ;
        if (employee.CatagoryNavigation != null)
        {

            catagory = new(employee.CatagoryNavigation);
        }
        CustomCollection<Attendance> _attendances = new();
        CustomCollection<SkillVM> _skills = new();
        CustomCollection<Leave> _leaves = new CustomCollection<Leave>();
        foreach (var attendance in employee.Attendances) _attendances.Add(attendance);
        foreach (var skill in employee.Skills) _skills.Add(new(skill));
        foreach (var leave in employee.Leaves) _leaves.Add(leave);
        skills = _skills;
        attendances = _attendances;
        leaves = _leaves;
    }

    public Employee ToDTO()
    {
        CustomCollection<Attendance> _attendances = new();
        CustomCollection<Skill> _skills = new();
        CustomCollection<Leave> _leaves = new CustomCollection<Leave>();
        foreach (var attendance in this.Attendances) _attendances.Add(attendance);

        foreach (var skill in this.Skills) _skills.Add(skill.ToDTO());
        foreach (var leave in this.Leaves) _leaves.Add(leave);
        return new()
        {
            EmployeeImg = this.EmployeeImg,
            EmployeeId = this.EmployeeId,
            Name = this.Name,
            FatherName = this.Fathername,
            PhoneNo = this.Phone,
            Email = this.Email,
            Catagory = this.Catagory.CatagoryId,
            CatagoryNavigation = this.Catagory.ToDTO(),
            Salary = this.Salary,
            Skills = _skills,
            Attendances = _attendances,
            Leaves = _leaves,
            LeavingDate = Leavedate,
            StartingDate = Startdate,
        };
    }

    public static async Task<EmployeeVM> LoadAsync(Employee employee)
    {
        EmployeeVM result = new EmployeeVM(employee);
        result.Image = await FileHandler.GetImgSourceAsync(result.EmployeeImg);
        return result;
    }


    public async Task LoadImg()
    {
        this.Image = await FileHandler.GetImgSourceAsync(this.EmployeeImg);
    }
}
