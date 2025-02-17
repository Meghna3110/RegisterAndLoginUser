using System.ComponentModel.DataAnnotations;
using User_Login_and_Registration.Utility.Enums;

namespace User_Login_and_Registration.Model
{
    public class Admin
    {
        [Key]
        public string AdminId { get; set; }
        public string Password { get; set; }
        //public UserVerificationStatus VerificationStatus { get; set; } = UserVerificationStatus.Pending;
    }
}
