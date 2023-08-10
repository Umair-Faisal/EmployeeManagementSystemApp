using Backend;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ViewModel.VM_Models;

namespace ViewModel.ViewModels;

public partial class EmpMainPageVM : ObservableObject
{

    ObservableCollection<EmployeeVM> EmployeeList { get; set; }



    public IEnumerable<EmployeeVM> Employees => EmployeeList.Where(employee => Filter_Employees(employee));

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Employees))]
    string searchText;
    private bool Filter_Employees(EmployeeVM employee)
    {
        return
            employee.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)
         || employee.Catagory.ToString().Contains(SearchText, StringComparison.OrdinalIgnoreCase)
         || employee.Skills.ToString().Contains(SearchText, StringComparison.OrdinalIgnoreCase)
         || employee.EmployeeId.ToString().Contains(SearchText, StringComparison.OrdinalIgnoreCase);

    }
    public EmpMainPageVM()
    {
        EmployeeList = new();

        searchText = string.Empty;
        EmployeeList.CollectionChanged += EmployeeList_CollectionChanged;

    }

    private void EmployeeList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        OnPropertyChanged(nameof(Employees));
    }

    public async Task LoadData()
    {
        var _EmployeeList = await DataAccess.GetEmployees();
        foreach (var emp in _EmployeeList)
        {
            EmployeeVM employee = new(emp);
            EmployeeList.Add(employee);
        }
        await LoadImages();
    }

    private async Task LoadImages()
    {
        foreach (var emp in EmployeeList)
        {
            emp.Image = await FileHandler.GetImgSourceAsync(emp.EmployeeImg);
        }
    }
}
