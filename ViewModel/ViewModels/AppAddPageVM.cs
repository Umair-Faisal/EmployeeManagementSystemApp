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

namespace ViewModel.ViewModels;

public partial class AppAddPageVM : ObservableObject
{



    public ObservableCollection<CatagoryVM> CatagoryList = new();

    public ObservableCollection<SkillVM> SkillList = new();

    public ObservableCollection<SkillVM> SelectedSkills = new();


    [ObservableProperty]
    SkillVM selectedSkill;

    [ObservableProperty]
    string selectedCVPath;

    [ObservableProperty]
    ApplicantVM applicant_Current;


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
    public async Task AddApplicant()
    {
        CopyCV();
        Applicant_Current.Skills.FromEnum(SelectedSkills.AsEnumerable());
        Applicant added_applicant = Applicant_Current.ToDTO();
        await DataAccess.AddApplicant(added_applicant);
    }

    private void CopyCV()
    {
        StringBuilder Path = new(AppDataPath.PDFPath);
        Path.Append($"{Applicant_Current.ApplicantId}_CV.pdf");
        File.Copy(SelectedCVPath, Path.ToString());
        Applicant_Current.CV = Path.ToString();
    }

    public AppAddPageVM()
    {
        applicant_Current = new();
    }

    public async Task LoadData()
    {
        var DbSkills = await DataAccess.GetSkills();
        foreach (var skill in DbSkills)
            SkillList.Add(new(skill));
        var DbCats = await DataAccess.GetCatagories();
        foreach (var cat in DbCats)
            CatagoryList.Add(new(cat));
    }
}
