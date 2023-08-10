using Backend;
using Backend.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ViewModel.VM_Models;

namespace ViewModel.ViewModels
{
    public partial class AppDelPageVM : ObservableObject
    {

        public ObservableCollection<ApplicantVM> ApplicantList = new();


        [ObservableProperty]
        ApplicantVM selectedapplicant;


        [RelayCommand]
        async Task DelButton()
        {
            await DataAccess.DeleteApplicant(Selectedapplicant.ApplicantId);
        }

        public AppDelPageVM()
        {

            selectedapplicant = new();
        }

        public async Task LoadData()
        {
            var applicants = await DataAccess.GetApplicants();
            foreach (Applicant applicant in applicants)
            {
                ApplicantList.Add(new(applicant));
            }
        }
    }
}
