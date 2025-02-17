using Microsoft.AspNetCore.Mvc;
using Umbraco.Core.Services.Implement;
using User_Login_and_Registration.DTOs.AdminLoginDto;
using User_Login_and_Registration.DTOs.UserVerificationDto;
using User_Login_and_Registration.Services;
using User_Login_and_Registration.Services.Interfaces;
using User_Login_and_Registration.Utility.Enums;

namespace User_Login_and_Registration.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService adminService;
        private readonly IUserService userService;

        public AdminController(IAdminService adminService)
        {
            adminService = adminService?? throw new ArgumentNullException(nameof(adminService));
            userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AdminLoginRequestDto adminLoginDto)
        {
            if (adminLoginDto == null || string.IsNullOrEmpty(adminLoginDto.AdminId) || string.IsNullOrEmpty(adminLoginDto.Password))
            {
                return BadRequest("AdminId and Password are required.");
            }

            try
            {
                await adminService.LoginAsync(adminLoginDto.AdminId, adminLoginDto.Password);
                return Ok("Login successful.");
            }
            catch (Exception ex)
            {
                return Unauthorized($"Login failed: {ex.Message}");
            }
        }



        [HttpPost("verifyUser")]
        public async Task<IActionResult> VerifyUser([FromBody] UserVerificationRequestDto userVerificationDto)
        {
            if (userVerificationDto == null || string.IsNullOrEmpty(userVerificationDto.UserId))
            {
                return BadRequest("UserId is required.");
            }

            try
            {
                var user = await userService.GetUserByIdAsync(userVerificationDto.UserId);
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                // Check the verification status
                if (user.VerificationStatus == UserVerificationStatus.Pending)
                {
                    return BadRequest("User verification is pending.");
                }

                if (user.VerificationStatus == UserVerificationStatus.Rejected)
                {
                    return BadRequest("User has been rejected and cannot be verified again.");
                }

                if (user.VerificationStatus == UserVerificationStatus.Verified)
                {
                    return Ok("User has already been verified.");
                }

                await adminService.VerifyUserAsync(userVerificationDto.UserId, userVerificationDto.VerificationStatus);
                return Ok("User verification successful.");
            }
            catch (Exception ex)
            {
                return BadRequest($"User verification failed: {ex.Message}");
            }
        }
    }
}
