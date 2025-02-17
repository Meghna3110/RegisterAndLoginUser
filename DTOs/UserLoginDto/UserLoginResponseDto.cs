using User_Login_and_Registration.DTOs.UserDto;

namespace User_Login_and_Registration.DTOs.UserLoginDto
{
    public class UserLoginResponseDto
    {
        public string Token { get; set; }
        public string Message { get; set; }
        public UserResponseDto User { get; set; } // Include user data along with token
        public bool IsActive { get; set; }
    }
}
