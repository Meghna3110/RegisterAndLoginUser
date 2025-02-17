using User_Login_and_Registration.Utility.Enums;

namespace User_Login_and_Registration.DTOs.UserVerificationDto
{
    public class UserVerificationResponseDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public UserVerificationStatus VerificationStatus { get; set; }
        public string Message { get; set; }
    }
}
