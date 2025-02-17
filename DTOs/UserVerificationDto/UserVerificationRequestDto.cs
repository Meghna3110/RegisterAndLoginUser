using User_Login_and_Registration.Utility.Enums;

namespace User_Login_and_Registration.DTOs.UserVerificationDto
{
    public class UserVerificationRequestDto
    {
        public string UserId { get; set; }
        public UserVerificationStatus VerificationStatus { get; set; }// Enum values as string: "Pending", "Verified", "Rejected"
    }
}
