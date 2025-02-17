namespace User_Login_and_Registration.DTOs.UserDto
{
    public class UserResponseDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; } = DateTime.Now;
    }
}
