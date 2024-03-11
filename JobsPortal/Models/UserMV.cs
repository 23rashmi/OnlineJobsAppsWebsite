using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsPortal.Models
{
    public class UserMV
    {
        public UserMV()
        {
            Company = new CompanyMV();
           Employee = new EmployeeMV();
        }

        public int UserID { get; set; }

        public int UserTypeID { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [Index(IsUnique = true)]
        [MaxLength(100, ErrorMessage = "Username can't be longer than 100 characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Contact number is required")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Contact number must be 10 digits")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Contact number must be numeric")]
        public string ContactNo { get; set; }
        public string Skills {  get; set; }
        public string Preferred_location { get; set; }
        public byte[] Resume { get; set; }
        public bool AreYouProvider {  get; set; }
        public CompanyMV Company { get; set; }
        public EmployeeMV Employee { get; set; }
    }
}
