using System;
using System.Collections.Generic;

namespace JobprtalsWebAPI.Models
{
    public partial class PostJobTable
    {
        //public PostJobTable()
        //{
        //    JobRequirementDetailsTables = new HashSet<JobRequirementDetailsTable>();
        //}

        public int PostJobId { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public int JobCategoryId { get; set; }
        public string JobTitle { get; set; } = null!;
        public string JobDescription { get; set; } = null!;
        public int MinSalary { get; set; }
        public int MaxSalary { get; set; }
        public string Location { get; set; } = null!;
        public int Vacancy { get; set; }
        public int JobNatureId { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime ApplicationDeadline { get; set; }
        public DateTime LastDate { get; set; }
        public int JobStatusId { get; set; }

        public virtual CompanyTable Company { get; set; } = null!;
        public virtual JobCategoryTable JobCategory { get; set; } = null!;
        public virtual JobNatureTable JobNature { get; set; } = null!;
        public virtual JobStatusTable JobStatus { get; set; } = null!;
        public virtual UserTable User { get; set; } = null!;
        //public virtual ICollection<JobRequirementDetailsTable> JobRequirementDetailsTables { get; set; }
    }
}
