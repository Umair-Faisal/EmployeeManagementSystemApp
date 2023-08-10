using Backend.Collections;
using Backend.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ViewModel.VM_Models;

public partial class SkillVM : ObservableObject
{
    [ObservableProperty]
    int skillId;

    [ObservableProperty]
    string skillName;


    [ObservableProperty]
    CustomCollection<Employee> employees;

    [ObservableProperty]
    CustomCollection<Applicant> applicants;


    public override string ToString()
    {
        return this.SkillName;
    }

    public SkillVM()
    {
        skillId = 0;
        skillName = string.Empty;
        employees = new();
        applicants = new();
    }

    public SkillVM(Skill skill)
    {
        skillId = skill.SkillId;
        skillName = skill.SkillName;
        employees = new(skill.Employees);
        applicants = new(skill.Applicants);
    }

    public void FromSkill(Skill skill)
    {
        this.SkillName = skill.SkillName;
        this.SkillId = skill.SkillId;
        this.Employees = new(skill.Employees);
        this.Applicants = new(skill.Applicants);
    }
    public Skill ToDTO()
    {
        return new()
        {
            SkillId = this.SkillId,
            SkillName = this.SkillName,
            Employees = this.Employees,
            Applicants = this.Applicants
        };
    }
}
