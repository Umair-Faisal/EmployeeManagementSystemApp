using Backend;
using Backend.Collections;
using Backend.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ViewModel.VM_Models;

namespace ViewModel.ViewModels
{
    public partial class OptionVM : ObservableObject
    {
        IEnumerable<Skill> _Skills { get; set; }

        CustomCollection<SkillVM> Skills { get; set; }

        [ObservableProperty]
        ObservableCollection<SkillVM> skillList;


        IEnumerable<Catagory> _Catagories { get; set; }
        CustomCollection<CatagoryVM> Catagories { get; set; }

        [ObservableProperty]
        ObservableCollection<CatagoryVM> catagoryList;

        [ObservableProperty]
        SkillVM selectedSkill;

        [ObservableProperty]
        CatagoryVM selectedCatagory;

        [ObservableProperty]
        string? newCatagory;

        [ObservableProperty]
        string? newSkill;

        async Task RefreshSkills()
        {
            _Skills = await DataAccess.GetSkills();
            LoadSkills();
        }

        async Task RefreshCatagory()
        {
            _Catagories = await DataAccess.GetCatagories();
            LoadCatagories();
        }

        [RelayCommand]
        async Task AddSkill()
        {
            Skill skill = new() { SkillName = NewSkill };
            await DataAccess.AddSkill(skill);
            await RefreshSkills();
        }

        [RelayCommand]
        async Task AddCatagory()
        {
            Catagory catagory = new() { CatagoryName = NewCatagory };
            await DataAccess.AddCatagory(catagory);
            await RefreshCatagory();
        }

        [RelayCommand]
        async Task DelSkill()
        {
            if (SelectedSkill is not null)
            {
                await DataAccess.RemoveSkill(SelectedSkill.SkillId);
                await RefreshSkills();
            }
        }

        [RelayCommand]
        async Task DelCat()
        {
            if (SelectedCatagory is not null)
            {
                await DataAccess.RemoveCatagory(SelectedCatagory.CatagoryId);
                await RefreshCatagory();
            }
        }
        public OptionVM()
        {

        }

        public async Task LoadData()
        {
            await RefreshSkills();
            await RefreshCatagory();
        }

        private void LoadCatagories()
        {
            Catagories = new();
            foreach (Catagory catagory in _Catagories)
            {
                Catagories.Add(new CatagoryVM(catagory));
            }
            CatagoryList = new(Catagories);
        }

        private void LoadSkills()
        {
            Skills = new();
            foreach (Skill skill in _Skills)
            {
                Skills.Add(new(skill));
            }
            SkillList = new(Skills);
        }

    }
}
