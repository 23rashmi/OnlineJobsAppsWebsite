using System;
using System.Collections.Generic;

namespace restwebapidemo.Models
{
    public partial class UserTable
    {
        public UserTable()
        {
            CompanyTables = new HashSet<CompanyTable>();
            EmployeesTables = new HashSet<EmployeesTable>();
            PostJobTables = new HashSet<PostJobTable>();
        }

        public int UserId { get; set; }
        public int UserTypeId { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string ContactNo { get; set; } = null!;

        public virtual UserTypeTable UserType { get; set; } = null!;
        public virtual ICollection<CompanyTable> CompanyTables { get; set; }
        public virtual ICollection<EmployeesTable> EmployeesTables { get; set; }
        public virtual ICollection<PostJobTable> PostJobTables { get; set; }
    }
}
