using Backend;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ViewModel.VM_Models;

namespace ViewModel.ViewModels
{
    public partial class AppMainPageVM : ObservableObject
    {

        ObservableCollection<ApplicantVM> ApplicantList = new();
        public IEnumerable<ApplicantVM> Applicants => ApplicantList.Where(x => FilterApplicant(x));

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Applicants))]
        string searchText;

        private bool FilterApplicant(ApplicantVM applicant)
        {
            return
            applicant.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)
         || applicant.Catagory.ToString().Contains(SearchText, StringComparison.OrdinalIgnoreCase)
         || applicant.Skills.ToString().Contains(SearchText, StringComparison.OrdinalIgnoreCase)
         || applicant.ApplicantId.ToString().Contains(SearchText, StringComparison.OrdinalIgnoreCase);
        }

        public AppMainPageVM()
        {
            searchText = string.Empty;
            ApplicantList.CollectionChanged += RefreshApplicants;
        }

        private void RefreshApplicants(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
            => OnPropertyChanged(nameof(Applicants));


        public async Task LoadData()
        {
            var applicants = await DataAccess.GetApplicants();
            foreach (var applicant in applicants)
            {
                ApplicantList.Add(new(applicant));
            }
            await LoadImages();
        }
        private async Task LoadImages()
        {
            foreach (var app in ApplicantList)
                app.ApplicatImg = await FileHandler.GetImgSourceAsync(app.ImgData);
        }


    }
}
