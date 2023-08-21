﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Attendances = new HashSet<Attendance>();
            Leaves = new HashSet<Leave>();
            Skills = new HashSet<Skill>();
        }

        public int EmployeeId { get; set; }
        public byte[] EmployeeImg { get; set; }
        public string Name { get; set; }

        public DateTime? StartingDate { get; set; }
        public DateTime? LeavingDate { get; set; }
        public string FatherName { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public int? Catagory { get; set; }
        public decimal? Salary { get; set; }

        public virtual Catagory CatagoryNavigation { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<Leave> Leaves { get; set; }

        public virtual ICollection<Skill> Skills { get; set; }
    }
}