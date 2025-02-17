namespace User_Login_and_Registration.DTOs.UserDto
{
    public class UserRequestDto
    {
        public string UserName { get; set; }
        public string Phno { get; set; }
        public string Email { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string MaritalStatus { get; set; }
        public string Password { get; set; }
        public string ReEnterPassword { get; set; }
        public bool IsActive { get; set; }

    }
}
