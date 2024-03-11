using System;
using System.Collections.Generic;

namespace restwebapidemo.Models
{
    public partial class JobRequirementDetailsTable
    {
        public int JobRequirementDetailsId { get; set; }
        public int JobRequirementId { get; set; }
        public string JobRequirementDetails { get; set; } = null!;
        public int? PostJobId { get; set; }

        public virtual JobRequirementsTable JobRequirement { get; set; } = null!;
        public virtual PostJobTable? PostJob { get; set; }
    }
}
