using System;
using System.Collections.Generic;

namespace restwebapidemo.Models
{
    public partial class JobNatureTable
    {
        public JobNatureTable()
        {
            PostJobTables = new HashSet<PostJobTable>();
        }

        public int JobNatureId { get; set; }
        public string JobNature { get; set; } = null!;

        public virtual ICollection<PostJobTable> PostJobTables { get; set; }
    }
}
