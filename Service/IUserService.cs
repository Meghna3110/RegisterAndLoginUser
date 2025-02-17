using User_Login_and_Registration.DTOs.UserDto;
using User_Login_and_Registration.DTOs.UserLoginDto;
using User_Login_and_Registration.Model;

namespace User_Login_and_Registration.Services
{
    public interface IUserService
    {
        Task<UserResponseDto> RegisterUserAsync(UserRequestDto userRequestDto);
        Task<UserLoginResponseDto> LoginUserAsync(UserLoginRequestDto userLoginRequestDto);
        Task<UserModel> GetUserByIdAsync(string userId);
    }
}
