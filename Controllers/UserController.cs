using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User_Login_and_Registration.DTOs.UserDto;
using User_Login_and_Registration.DTOs.UserLoginDto;
using User_Login_and_Registration.Services;
using User_Login_and_Registration.Model;
using System;
using User_Login_and_Registration.Repositories;
using User_Login_and_Registration.Utility.MarriageStatusEnum;

namespace User_Login_and_Registration.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;

        public UserController(IUserService userService, IUserRepository userRepository)
        {
            _userService = userService;
            _userRepository = userRepository;
        }

        // Register User
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRequestDto userRequestDto)
        {
            try
            {
                var userResponse = await _userService.RegisterUserAsync(userRequestDto);
                return Ok(userResponse); // This should return UserResponseDto
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Login User
        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginRequestDto userLoginRequestDto)
        {
            try
            {
                // Ensure the login process checks the user's active status
                var loginResponse = await _userService.LoginUserAsync(userLoginRequestDto);

                // If the user is inactive, throw an exception or return an appropriate response
                if (loginResponse.IsActive)
                {
                    return Unauthorized(new { Message = "User is inactive and cannot log in." });
                }

                return Ok(new { Message = "Login successful.", Data = loginResponse }); // Return UserLoginResponseDto
            }
            catch (Exception ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
        }

        // Get All Users
        [HttpGet("GetUserList")]
        public async Task<IActionResult> GetUserList()
        {
            try
            {
                var users = await _userRepository.GetAllUsersAsync();
                return Ok(users); // You can customize the response to return a specific DTO if needed
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Get User by UserId
        [HttpGet("GetUserByUserId/{userId}")]
        public async Task<IActionResult> GetUserByUserId(string userId)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(userId);
                if (user == null)
                {
                    return NotFound("User not found.");
                }
                return Ok(user); // You can customize the response to return a specific DTO if needed
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Get User by EmailId
        [HttpGet("GetUserByEmailId/{email}")]
        public async Task<IActionResult> GetUserByEmailId(string email)
        {
            try
            {
                var user = await _userRepository.GetUserByEmailAsync(email);
                if (user == null)
                {
                    return NotFound("User not found.");
                }
                return Ok(user); // You can customize the response to return a specific DTO if needed
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Get User by DOB
        [HttpGet("GetUserByDOB/{dob}")]
        public async Task<IActionResult> GetUserByDOB(string dob)
        {
            try
            {
                var users = await _userRepository.GetAllUsersAsync(); // Fetch all users
                var filteredUsers = users.Where(u => u.DOB == dob).ToList(); // Filter users by DOB

                if (filteredUsers.Count == 0)
                {
                    return NotFound("No users found with this DOB.");
                }

                return Ok(filteredUsers); // You can customize the response to return a specific DTO if needed
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Edit User by UserId
        [HttpPut("EditUserByUserId/{userId}")]
        public async Task<IActionResult> EditUserByUserId(string userId, [FromBody] UserRequestDto userRequestDto)
        {
            try
            {
                var existingUser = await _userRepository.GetUserByIdAsync(userId);
                if (existingUser == null)
                {
                    return NotFound("User not found.");
                }

                // Update user details here (excluding the UserId)
                existingUser.UserName = userRequestDto.UserName;
                existingUser.Phno = userRequestDto.Phno;
                existingUser.Email = userRequestDto.Email;
                existingUser.DOB = userRequestDto.DOB;
                existingUser.Gender = userRequestDto.Gender;
                existingUser.Address = userRequestDto.Address;
                existingUser.MaritalStatus = Enum.Parse<MarriageStatus>(userRequestDto.MaritalStatus);
                existingUser.Password = userRequestDto.Password; // You should hash the password in real applications
                existingUser.ReEnterPassword = userRequestDto.ReEnterPassword;
                existingUser.IsActive = userRequestDto.IsActive;
                existingUser.UpdateDate = DateTime.Now;

                // Update user in repository
                await _userRepository.UpdateUserAsync(existingUser);

                return Ok(existingUser); // You can return a specific DTO if needed
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Delete User by UserId
        [HttpDelete("DeleteUserByUserId/{userId}")]
        public async Task<IActionResult> DeleteUserByUserId(string userId)
        {
            try
            {
                var existingUser = await _userRepository.GetUserByIdAsync(userId);
                if (existingUser == null)
                {
                    return NotFound("User not found.");
                }

                // Delete user from repository
                await _userRepository.DeleteUserAsync(userId);

                return Ok("User deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
