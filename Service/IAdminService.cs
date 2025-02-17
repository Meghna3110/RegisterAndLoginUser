using User_Login_and_Registration.DTOs.CommonResponseDto;
using User_Login_and_Registration.Model;
using User_Login_and_Registration.Utility.Enums;

namespace User_Login_and_Registration.Services.Interfaces
{
    public interface IAdminService
    {
        Task VerifyUserAsync(string userId, UserVerificationStatus verificationStatus);
        Task LoginAsync(string adminId, string password);
    }

}
