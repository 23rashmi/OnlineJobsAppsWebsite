using System;
using System.Collections.Generic;

namespace restwebapidemo.Models
{
    public partial class JobStatusTable
    {
        public JobStatusTable()
        {
            PostJobTables = new HashSet<PostJobTable>();
        }

        public int JobStatusId { get; set; }
        public string JobStatus { get; set; } = null!;
        public string? StatusMessage { get; set; }

        public virtual ICollection<PostJobTable> PostJobTables { get; set; }
    }
}
