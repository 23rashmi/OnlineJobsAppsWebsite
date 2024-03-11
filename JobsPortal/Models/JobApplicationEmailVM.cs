using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobsPortal.ViewModels
{
    public class JobApplicationEmailVM
    {
        [Key]
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public DateTime? DOB { get; set; }
        public string Education { get; set; }
        public string WorkExperience { get; set; }
        public string Skills { get; set; }
        public string EmailAddress { get; set; }
        public string Gender { get; set; }
        public string PhotoPath { get; set; }
        public string Qualification { get; set; }
        public string PermanentAddress { get; set; }
        public string JobReference { get; set; }
        public string Description { get; set; }
        public string ResumePath { get; set; }
        public string CompanyEmail { get; set; } // The email of the company where the application will be sent.
    }
}
