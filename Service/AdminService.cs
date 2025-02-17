using User_Login_and_Registration.Model;
using User_Login_and_Registration.Repositories;
using User_Login_and_Registration.Repositories.Interfaces;
using User_Login_and_Registration.Services.Interfaces;
using User_Login_and_Registration.Utility.Enums;
using User_Login_and_Registration.DTOs.CommonResponseDto;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace User_Login_and_Registration.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAdminRepository _adminRepository;

        public AdminService(IUserRepository userRepository, IAdminRepository adminRepository)
        {
            _userRepository = userRepository;
            _adminRepository = adminRepository;
        }


        public async Task VerifyUserAsync(string userId, UserVerificationStatus verificationStatus)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            //if(user.VerificationStatus == UserVerificationStatus.Pending)
            //{
            //    throw new Exception("User Verification is Pending.");
            //}

            //if (user.VerificationStatus == UserVerificationStatus.Rejected)
            //{
            //    throw new Exception("User has been rejected and cannot be verified again.");
            //}

            //if (user.VerificationStatus == UserVerificationStatus.Verified)
            //{
            //    throw new Exception("User Verification is Succesful.");
            //}

            user.VerificationStatus = verificationStatus;
            await _userRepository.UpdateUserAsync(user);
        }

        // Admin login
        public async Task LoginAsync(string adminId, string password)
        {
            var admin = await _adminRepository.GetAdminByIdAsync(adminId);

            if (admin == null || admin.Password != password)
            {
                throw new Exception("AdminId or password is incorrect.");
            }

            // Optional: Add any additional login checks (e.g., inactive admin)
        }

        public async Task<UserModel> UserLoginAsync(string userId, string password)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null)
            {
                throw new Exception("UserId or password is incorrect.");
            }

            if (!user.IsActive)
            {
                throw new Exception("User account is inactive. Please contact support.");
            }

            if (user.Password != password) // Assuming you are not hashing the password
            {
                throw new Exception("UserId or password is incorrect.");
            }

            return user; // Optionally return user details or token generation logic
        }
    }
}
