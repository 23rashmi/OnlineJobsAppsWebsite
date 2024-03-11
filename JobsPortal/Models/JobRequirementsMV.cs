using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobsPortal.Models
{
    public class JobRequirementsMV
    {
        public JobRequirementsMV()
        {
            Details = new List<JobRequirementDetailsMV>();
        }
        public int JobRequirementID { get; set; }
        public string JobRequirement { get; set; }
        public List<JobRequirementDetailsMV> Details { get; set; }
    }
}