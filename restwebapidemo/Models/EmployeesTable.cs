using System;
using System.Collections.Generic;

namespace restwebapidemo.Models
{
    public partial class EmployeesTable
    {
        public int EmployeeId { get; set; }
        public int UserId { get; set; }
        public string EmployeeName { get; set; } = null!;
        public DateTime? Dob { get; set; }
        public string? Education { get; set; }
        public string? WorkExperience { get; set; }
        public string? Skills { get; set; }
        public string EmailAddress { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string? PhotoL { get; set; }
        public string? Qualification { get; set; }
        public string? PermanentAddress { get; set; }
        public string? JobReference { get; set; }
        public string? Description { get; set; }
        public string? ResumeL { get; set; }
        public byte[]? Photo { get; set; }
        public byte[]? Resume { get; set; }

        public virtual UserTable User { get; set; } = null!;
    }
}
