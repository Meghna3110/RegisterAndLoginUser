using User_Login_and_Registration.Model;

namespace User_Login_and_Registration.Repositories
{
    public interface IUserRepository
    {
        Task<UserModel> GetUserByEmailAsync(string email);
        Task<UserModel> GetUserByIdAsync(string userId);
        Task<IEnumerable<UserModel>> GetAllUsersAsync();
        Task AddUserAsync(UserModel user);
        Task UpdateUserAsync(UserModel user);
        Task DeleteUserAsync(string userId);

        UserModel GetUserById(int userId);
        void SaveChanges();
    }
}
