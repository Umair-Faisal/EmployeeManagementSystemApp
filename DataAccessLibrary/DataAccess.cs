using Backend.Contexts;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Backend
{
    public static class DataAccess
    {

        public static async Task<IEnumerable<Employee>> GetEmployees(bool WithSKills = true, bool WithCatagories = true,
            bool WithAttendences = false, bool WithLeaves = false,bool LeftAlso = false)
        {
            using LocalDB db = new();

            var query = db.Employees.AsQueryable();
            if (WithSKills) { query = query.Include(x => x.Skills); }
            if (WithCatagories) { query = query.Include(x => x.CatagoryNavigation); }
            if (WithAttendences) { query = query.Include(x => x.Attendances); }
            if (WithLeaves) { query = query.Include(x => x.Leaves); }
            if (!LeftAlso) { query = query.Where(x => x.LeavingDate == null); }

            return await query.ToListAsync();
        }

        public static async Task<Employee> GetEmployee(int id, bool WithSKills = true, bool WithCatagories = true,
            bool WithAttendences = false, bool WithLeaves = false)
        {
            using LocalDB db = new();

            var query = db.Employees.AsQueryable();
            if (WithSKills) { query = query.Include(x => x.Skills); }
            if (WithCatagories) { query = query.Include(x => x.CatagoryNavigation); }
            if (WithAttendences) { query = query.Include(x => x.Attendances); }
            if (WithLeaves) { query = query.Include(x => x.Leaves); }

            return await query.SingleAsync(x => x.EmployeeId == id);
        }

        public static async Task AddEmployee(Employee employee)
        {
            List<Skill> skills = employee.Skills.ToList();
            employee.Skills = new List<Skill>();
            employee.CatagoryNavigation = null;
            employee.Attendances = new List<Attendance>();
            employee.Leaves = new List<Leave>();

            using LocalDB db = new();
            db.Employees.Add(employee);
            foreach (Skill skill in skills)
            {
                Skill? db_skill = db.Skills.Find(skill.SkillId);
                if (db_skill != null)
                {
                    employee.Skills.Add(db_skill);
                }
                else
                {
                    skill.SkillId = new();
                    db.Skills.Add(skill);
                    employee.Skills.Add(skill);
                }
            }

            await db.SaveChangesAsync();
        }

        public static async Task UpdateEmployee(Employee employee)
        {
            var skills = employee.Skills;
            employee.Skills = new List<Skill>();
            employee.CatagoryNavigation = null;

            List<Skill> db_skills = new();

            using LocalDB db = new();
            await db.Employees.Where(x => x.EmployeeId == employee.EmployeeId).ExecuteUpdateAsync(Update => Update
             .SetProperty(x => x.EmployeeImg, employee.EmployeeImg)
             .SetProperty(x => x.Name, employee.Name)
             .SetProperty(x => x.FatherName, employee.FatherName)
             .SetProperty(x => x.Salary, employee.Salary)
             .SetProperty(x => x.PhoneNo, employee.PhoneNo)
             .SetProperty(x => x.Email, employee.Email)
             .SetProperty(x => x.Catagory, employee.Catagory)
             .SetProperty(x => x.StartingDate,employee.StartingDate)
             .SetProperty(x => x.LeavingDate,employee.LeavingDate)
             );
            var db_employee = await db.Employees.Where(x => x.EmployeeId == employee.EmployeeId).Include(x => x.CatagoryNavigation).Include(x => x.Skills).FirstAsync();

            foreach (var skill in skills)
            {
                if (db.Skills.Find(skill.SkillId) is not null and Skill dbskill)
                {
                    db_skills.Add(dbskill);
                }
            }

            if (db_employee != null)
            {
                db_employee.Skills = db_skills;
            }
            await db.SaveChangesAsync();

        }

        public static async Task DeleteEmployee(int id)
        {
            using LocalDB db = new();
            await db.Employees.Where(x => x.EmployeeId == id).ExecuteDeleteAsync();

        }


        public static async Task<IEnumerable<Catagory>> GetCatagories(bool WithEmployees = false, bool WithApplicants = false)
        {
            using LocalDB db = new();
            var query = db.Catagories.AsQueryable();
            if (WithEmployees) query = query.Include(x => x.Employees);
            if (WithApplicants) query = query.Include(x => x.Applicants);
            return await query.ToListAsync();
        }

        public static async Task<IEnumerable<Skill>> GetSkills(bool WithEmployees = false, bool WithApplicants = false)
        {
            using LocalDB db = new();
            var query = db.Skills.AsQueryable();
            if (WithEmployees) query = query.Include(x => x.Employees);
            if (WithApplicants) query = query.Include(x => x.Applicants);
            return await query.ToListAsync();
        }



        public static async Task<IEnumerable<Applicant>> GetApplicants(bool WithCatagory = true, bool WithSkills = true)
        {
            using LocalDB db = new();
            var query = db.Applicants.AsQueryable();
            if (WithCatagory) query = query.Include(x => x.CatagoryNavigation);
            if (WithSkills) query = query.Include(x => x.Skills);
            return await query.ToListAsync();
        }


        public static async Task<Applicant> GetApplicant(int id, bool WithCatagory = true, bool WithSkills = true)
        {
            using LocalDB db = new();
            var query = db.Applicants.Where(x => x.ApplicantId == id).AsQueryable();
            if (WithCatagory) query = query.Include(x => x.CatagoryNavigation);
            if (WithSkills) query = query.Include(x => x.Skills);
            return await query.FirstAsync();
        }
        public static async Task AddApplicant(Applicant applicant)
        {
            List<Skill> skills = applicant.Skills.ToList();
            applicant.Skills = new List<Skill>();
            applicant.CatagoryNavigation = null;

            using LocalDB db = new();
            await db.Applicants.AddAsync(applicant);
            foreach (Skill skill in skills)
            {
                Skill? db_skill = await db.Skills.FindAsync(skill.SkillId);
                if (db_skill != null)
                {
                    applicant.Skills.Add(db_skill);
                }
                else
                {
                    skill.SkillId = new();
                    db.Skills.Add(skill);
                    applicant.Skills.Add(skill);
                }
            }
            await db.SaveChangesAsync();
        }

        public static async Task DeleteApplicant(int id)
        {
            using LocalDB db = new();
            await db.Applicants.Where(x => x.ApplicantId == id).ExecuteDeleteAsync();
        }

        public static async Task UpdateApplicant(Applicant applicant)
        {
            var skills = applicant.Skills;
            applicant.Skills = new List<Skill>();
            applicant.CatagoryNavigation = null;

            List<Skill> db_skills = new();

            using LocalDB db = new();
            await db.Applicants.Where(x => x.ApplicantId == applicant.ApplicantId).ExecuteUpdateAsync(Update => Update
            .SetProperty(x => x.Name, applicant.Name)
            .SetProperty(x => x.PhoneNo, applicant.PhoneNo)
            .SetProperty(x => x.Email, applicant.Email)
            .SetProperty(x => x.Catagory, applicant.Catagory)
            .SetProperty(x => x.Cv, applicant.Cv)
            .SetProperty(x => x.ApplicantImg, applicant.ApplicantImg)
            );
            var db_applicant = await db.Applicants.Where(x => x.ApplicantId == applicant.ApplicantId).Include(x => x.CatagoryNavigation).Include(x => x.Skills).FirstAsync();

            foreach (var skill in skills)
            {
                if (db.Skills.Find(skill.SkillId) is not null and Skill dbskill)
                {
                    db_skills.Add(dbskill);
                }
            }

            if (db_applicant != null)
            {
                db_applicant.Skills = db_skills;
            }
            await db.SaveChangesAsync();

        }


        public static async Task<IEnumerable<Attendance>> GetAttendances()
        {
            using LocalDB db = new();
            var query = db.Attendances.OrderBy(x => x.EmployeeId).ThenBy(x => x.AttendanceDate).AsQueryable();

            return await query.ToListAsync();
        }
        public static IEnumerable<Attendance> GetAttendances(int employeeId)
        {
            using var db = new LocalDB();
            var query = db.Attendances.Where(x => x.EmployeeId == employeeId).AsQueryable();

            return query.AsEnumerable();
        }

        public static async Task AddAttendence(Attendance attendance)
        {
            using var db = new LocalDB();
            await db.Attendances.AddAsync(attendance);
            await db.SaveChangesAsync();
        }

        public static async Task UpdateAttendence(Attendance attendance)
        {
            using var db = new LocalDB();
            var _attendence = await db.Attendances.Where(x => x.EmployeeId == attendance.EmployeeId && x.AttendanceDate == attendance.AttendanceDate).SingleAsync();
            _attendence.CheckInTime = attendance.CheckInTime;
            _attendence.CheckOutTime = attendance.CheckOutTime;
            await db.SaveChangesAsync();
        }

        public static async Task CheckIn(TimeSpan checkInTime, DateTime date, int EmployeeId)
        {

            Attendance attendance = new()
            {
                EmployeeId = EmployeeId,
                AttendanceDate = date.Date,
                CheckInTime = checkInTime
            };
            

            using var db = new LocalDB();
            bool AttendanceExistsInDB = await db.Attendances.AnyAsync(x => x.AttendanceDate.Date == attendance.AttendanceDate.Date && x.EmployeeId == attendance.EmployeeId);
            if (AttendanceExistsInDB)
            {
                var dbAttendance = await db.Attendances.FirstAsync(x => x.AttendanceDate.Date == attendance.AttendanceDate.Date && x.EmployeeId == attendance.EmployeeId);
                if (dbAttendance.CheckOutTime is null) dbAttendance.CheckInTime = checkInTime;
                else
                {
                    if (attendance.CheckInTime < dbAttendance.CheckOutTime)
                    {
                        dbAttendance.CheckInTime = checkInTime;
                    }
                }
            }
            else
            {
                await db.Attendances.AddAsync(attendance);
            }
            await db.SaveChangesAsync();
        }

        public static async Task CheckOut(TimeSpan checkOutTime, DateTime date, int EmployeeId)
        {
            using var db = new LocalDB();
            bool CheckedInOnSameDate = db.Attendances.Any(x => x.EmployeeId == EmployeeId && x.AttendanceDate.Date == date.Date);
            if(CheckedInOnSameDate)
            {
            var att = await db.Attendances.Where(x => x.EmployeeId == EmployeeId && x.AttendanceDate.Date == date.Date).FirstAsync();
           
                if (att.CheckInTime!.Value < checkOutTime)
                {
                    att.CheckOutTime = checkOutTime;
                    await db.SaveChangesAsync();
                }

            }
        }


        public static async Task<IEnumerable<MonthlyAttendance>> GetMonthlyAttendances(int? EmployeeID = null)
        {
            using var db = new LocalDB();
            var query = db.MonthlyAttendances.OrderBy(x => x.EmployeeId).ThenBy(x => x.Year).ThenBy(x => x.Month).AsQueryable();
            if (EmployeeID != null) query = query.Where(x => x.EmployeeId == EmployeeID);
            return await query.ToListAsync();
        }


        public static async Task AddSkill(Skill skill)
        {
            using var db = new LocalDB();
            await db.Skills.AddAsync(skill);
            await db.SaveChangesAsync();
        }

        public static async Task RemoveSkill(int skillId)
        {
            using var db = new LocalDB();
            await db.Skills.Where(x => x.SkillId == skillId).ExecuteDeleteAsync();
        }

        public static async Task AddCatagory(Catagory catagory)
        {
            using var db = new LocalDB();
            await db.Catagories.AddAsync(catagory);
            await db.SaveChangesAsync();
        }

        public static async Task RemoveCatagory(int CatagoryId)
        {
            using var db = new LocalDB();
            await db.Catagories.Where(x => x.CatagoryId == CatagoryId).ExecuteDeleteAsync();
        }
    }
}
