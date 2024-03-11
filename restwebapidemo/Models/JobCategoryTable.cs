using System;
using System.Collections.Generic;

namespace restwebapidemo.Models
{
    public partial class JobCategoryTable
    {
        public JobCategoryTable()
        {
            PostJobTables = new HashSet<PostJobTable>();
        }

        public int JobCategoryId { get; set; }
        public string JobCategory { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<PostJobTable> PostJobTables { get; set; }
    }
}
