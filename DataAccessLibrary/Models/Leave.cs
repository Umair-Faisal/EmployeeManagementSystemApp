﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class Leave
    {
        public int LeaveId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime? LeaveStartDate { get; set; }
        public DateTime? LeaveEndDate { get; set; }
        public string Reason { get; set; }

        public virtual Employee Employee { get; set; }
    }
}