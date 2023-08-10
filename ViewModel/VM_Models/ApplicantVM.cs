using Backend.Collections;
using Backend.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Media.Imaging;
using System.Threading.Tasks;
using Windows.Data.Pdf;

namespace ViewModel.VM_Models
{
    public partial class ApplicantVM : ObservableObject
    {


        [ObservableProperty]
        int applicantId;

        [ObservableProperty]
        SoftwareBitmapSource applicatImg;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        string phoneNo;

        [ObservableProperty]
        string email;

        [ObservableProperty]
        CatagoryVM catagory;

        [ObservableProperty]
        string cV;

        [ObservableProperty]
        CustomCollection<SkillVM> skills;

        public byte[] ImgData { get; set; }

        public override string ToString()
        {
            return this.Name;
        }

        public ApplicantVM()
        {
            skills = new();
        }

        public ApplicantVM(Applicant applicant)
        {
            applicantId = applicant.ApplicantId;
            name = applicant.Name;
            phoneNo = applicant.PhoneNo;
            email = applicant.Email;
            if (applicant.CatagoryNavigation != null)
            {

                catagory = new(applicant.CatagoryNavigation);
            }
            var s = new CustomCollection<SkillVM>();
            foreach (var skill in applicant.Skills)
            {
                s.Add(new(skill));
            }
            skills = s;
            CV = applicant.Cv;
            ImgData = applicant.ApplicantImg;
        }

        public Applicant ToDTO()
        {
            var s = new CustomCollection<Skill>();
            foreach (var skill in this.Skills)
            {
                s.Add(skill.ToDTO());
            }
            return new()
            {
                ApplicantId = this.ApplicantId,
                Name = this.Name,
                PhoneNo = this.PhoneNo,
                Email = this.Email,
                Catagory = this.Catagory.CatagoryId,
                CatagoryNavigation = this.Catagory.ToDTO(),
                Skills = s,
                Cv = CV,
                ApplicantImg = ImgData
            };
        }

        public async Task<ApplicantVM> LoadAsync(Applicant applicant)
        {
            var Result = new ApplicantVM(applicant);
            Result.ApplicatImg = await FileHandler.GetImgSourceAsync(ImgData);
            Result.CV = applicant.Cv;
            return Result;
        }



    }
}
