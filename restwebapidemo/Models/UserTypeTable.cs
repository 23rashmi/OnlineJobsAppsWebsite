using System;
using System.Collections.Generic;

namespace restwebapidemo.Models
{
    public partial class UserTypeTable
    {
        public UserTypeTable()
        {
            UserTables = new HashSet<UserTable>();
        }

        public int UserTypeId { get; set; }
        public string? UserType { get; set; }

        public virtual ICollection<UserTable> UserTables { get; set; }
    }
}
