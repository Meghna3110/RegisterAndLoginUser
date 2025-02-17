using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using User_Login_and_Registration.DTOs.UserDto;
using User_Login_and_Registration.DTOs.UserLoginDto;
using User_Login_and_Registration.Model;
using User_Login_and_Registration.Repositories;
using User_Login_and_Registration.Utility.Enums;
using User_Login_and_Registration.Utility.MarriageStatusEnum;

namespace User_Login_and_Registration.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository,IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<UserModel> GetUserByIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("UserId cannot be null or empty.");
            }

            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            return user;
        }

        public async Task<UserResponseDto> RegisterUserAsync(UserRequestDto userRequestDto)
        {
            // Validation: Phone number must have 10 digits
            if (userRequestDto.Phno.Length != 10 || !userRequestDto.Phno.All(char.IsDigit))
            {
                throw new Exception("Phone number must be exactly 10 digits.");
            }

            // Validation: Email must contain "@" and ".com"
            if (string.IsNullOrEmpty(userRequestDto.Email) || !userRequestDto.Email.Contains("@") || !userRequestDto.Email.EndsWith(".com"))
            {
                throw new Exception("Invalid email address.");
            }

            // Validation: Password must be alphanumeric
            if (string.IsNullOrEmpty(userRequestDto.Password) || !userRequestDto.Password.All(c => Char.IsLetterOrDigit(c)))
            {
                throw new Exception("Password must be alphanumeric.");
            }

            // Generate UserId
            string userId = GenerateUserId(userRequestDto.UserName, userRequestDto.Phno);

            // Check if user already exists
            var existingUser = await _userRepository.GetUserByEmailAsync(userRequestDto.Email);
            if (existingUser != null)
            {
                throw new Exception("User already exists with this email.");
            }

            // Create new user
            var newUser = new UserModel
            {
                UserId = userId,
                UserName = userRequestDto.UserName,
                Phno = userRequestDto.Phno,
                Email = userRequestDto.Email,
                DOB = userRequestDto.DOB,
                Gender = userRequestDto.Gender,
                Address = userRequestDto.Address,
                MaritalStatus = Enum.Parse<MarriageStatus>(userRequestDto.MaritalStatus),
                Password = userRequestDto.Password, // You should hash the password in real applications
                ReEnterPassword = userRequestDto.ReEnterPassword,
                IsActive = userRequestDto.IsActive,
                VerificationStatus = UserVerificationStatus.Pending,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };

            // Add user to the repository
            await _userRepository.AddUserAsync(newUser);

            return new UserResponseDto
            {
                UserName = newUser.UserName,
                UserId = newUser.UserId,
                IsActive = newUser.IsActive,
                CreateDate = newUser.CreateDate,
                UpdateDate = newUser.UpdateDate
            };
        }

        public async Task<UserLoginResponseDto> LoginUserAsync(UserLoginRequestDto userLoginRequestDto)
        {
            var user = await _userRepository.GetUserByEmailAsync(userLoginRequestDto.Email);

            if (user == null || user.Password != userLoginRequestDto.Password) // Check hashed passwords in production
            {
                throw new Exception("Invalid email or password.");
            }

            // Generate JWT token (token generation logic goes here)
            string token = GenerateJwtToken(user);

            return new UserLoginResponseDto
            {
                Token = token,
                Message = "Login Successful."
            };
        }

        //Utility methods
        private string GenerateUserId(string userName, string phoneNumber)
        {
            // First 4 characters from UserName (trim to 4)
            string namePart = new string(userName.Take(4).ToArray());

            // Next 5 digits from PhoneNumber
            string phonePart = phoneNumber.Substring(0, 5);

            // Next 4 random digits
            Random random = new Random();
            string randomPart = random.Next(1000, 9999).ToString();

            return $"{namePart}{phonePart}{randomPart}";
        }

        
        private string GenerateJwtToken(UserModel user)
        {
            var jwtKey = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(jwtKey) || Encoding.UTF8.GetBytes(jwtKey).Length < 32)
            {
                throw new InvalidOperationException("JWT Key must be at least 32 bytes long.");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim("userId", user.UserId),
        new Claim("userName", user.UserName)
    };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }




    }
}
