using System.ComponentModel.DataAnnotations;
using User_Login_and_Registration.Utility.Enums;
using User_Login_and_Registration.Utility.MarriageStatusEnum;

namespace User_Login_and_Registration.Model
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Phno { get; set; }
        public string Email { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public MarriageStatus MaritalStatus { get; set; }
        public string Password { get; set; }
        public string ReEnterPassword { get; set; }
        public bool IsActive { get; set; }
        public UserVerificationStatus VerificationStatus { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }

}
