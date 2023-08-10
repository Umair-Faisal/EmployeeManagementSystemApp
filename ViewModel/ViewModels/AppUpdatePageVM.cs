using Backend;
using Backend.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.VM_Models;

namespace ViewModel.ViewModels
{
    public partial class AppUpdatePageVM : ObservableObject
    {



        public ObservableCollection<CatagoryVM> CatagoryList = new();

        public ObservableCollection<SkillVM> SkillList = new();

        public ObservableCollection<ApplicantVM> ApplicantList = new();

        public ObservableCollection<SkillVM> SelectedSkills = new();

        [ObservableProperty]
        string selectedCVPath;

        [ObservableProperty]
        SkillVM selectedSkill;


        [ObservableProperty]
        int catIndex;

        [ObservableProperty]
        ApplicantVM applicant_current;

        partial void OnApplicant_currentChanged(ApplicantVM value)
        {
            if (value.Catagory != null)
            {
                SelectedSkills.Clear();
                foreach (var skill in value.Skills)
                    SelectedSkills.Add(skill);

                CatIndex = CatagoryList.IndexOf(CatagoryList.First(x => x.CatagoryId == value.Catagory.CatagoryId));

            }
            SelectedCVPath = value.CV;
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
        async Task UpdateApplicant()
        {
            CVCopy();
            Applicant_current.Skills.FromEnum(SelectedSkills.AsEnumerable());
            Applicant Upd_App = Applicant_current.ToDTO();
            await DataAccess.UpdateApplicant(Upd_App);
        }

        private void CVCopy()
        {
            if (SelectedCVPath != Applicant_current.CV)
            {
                StringBuilder Path = new(AppDataPath.PDFPath);
                Path.Append($"{Applicant_current.ApplicantId} CV.pdf");
                if (File.Exists(Path.ToString()))
                {
                    File.Delete(Path.ToString());
                }
                File.Copy(SelectedCVPath, Path.ToString());
                Applicant_current.CV = Path.ToString();
            }
        }

        public AppUpdatePageVM()
        {

            Applicant_current = new();
        }

        public async Task LoadData()
        {
            var skills = await DataAccess.GetSkills();
            foreach (var skill in skills)
            {
                SkillList.Add(new(skill));
            }
            var catagories = await DataAccess.GetCatagories();
            foreach (var catagory in catagories)
                CatagoryList.Add(new(catagory));
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
