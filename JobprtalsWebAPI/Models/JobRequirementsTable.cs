using System;
using System.Collections.Generic;

namespace JobprtalsWebAPI.Models
{
    public partial class JobRequirementsTable
    {
        //public JobRequirementsTable()
        //{
        //    JobRequirementDetailsTables = new HashSet<JobRequirementDetailsTable>();
        //}

        public int JobRequirementId { get; set; }
        public string JobRequirement { get; set; } = null!;

        //public virtual ICollection<JobRequirementDetailsTable> JobRequirementDetailsTables { get; set; }
    }
}
