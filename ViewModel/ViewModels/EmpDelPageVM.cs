using Backend;
using Backend.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ViewModel.VM_Models;

namespace ViewModel.ViewModels
{
    public partial class EmpDelPageVM : ObservableObject
    {

        public ObservableCollection<EmployeeVM> EmployeeList = new();


        [ObservableProperty]
        EmployeeVM selectedemp;


        [RelayCommand]
        public async Task DelButton()
        {
            await DataAccess.DeleteEmployee(Selectedemp.EmployeeId);
        }

        public EmpDelPageVM()
        {

            selectedemp = new();
        }

        public async Task LoadData()
        {
            var DbEmployees = await DataAccess.GetEmployees(false, false);
            foreach (Employee employee in DbEmployees)
            {
                EmployeeList.Add(new(employee));
            }
        }
    }
}
