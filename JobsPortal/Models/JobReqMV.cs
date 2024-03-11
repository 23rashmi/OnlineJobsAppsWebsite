using DatabaseAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobsPortal.Models
{
    public class JobReqMV
    {
        public JobReqMV()
        {
            Details = new List<JobRequirementDetailsTable>();
        }
        [Required(ErrorMessage ="*Required")]
        public int JobRequirementID { get; set; }
        [Required(ErrorMessage = "*Required")]
        public string JobRequirementDetails { get; set; }
        public int PostJobID { get; set; }
        public List<JobRequirementDetailsTable> Details { get; set; }
    }
}