using DatabaseAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobsPortal.Models
{
    public class FilteruserMV
    {
        public FilteruserMV()
        {
            Result = new List<UserTable>();
        }
        public int Skills { get; set; }
        public int Preferred_location { get; set; }
        
        public List<UserTable> Result { get; set; }
    }
}