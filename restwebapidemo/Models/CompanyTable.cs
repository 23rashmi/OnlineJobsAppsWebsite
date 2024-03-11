using System;
using System.Collections.Generic;

namespace restwebapidemo.Models
{
    public partial class CompanyTable
    {
        public CompanyTable()
        {
            PostJobTables = new HashSet<PostJobTable>();
        }

        public int CompanyId { get; set; }
        public int UserId { get; set; }
        public string CompanyName { get; set; } = null!;
        public string ContactNo { get; set; } = null!;
        public string PhoneNo { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string Logo { get; set; } = null!;
        public string? Description { get; set; }

        public virtual UserTable User { get; set; } = null!;
        public virtual ICollection<PostJobTable> PostJobTables { get; set; }
    }
}
