using Microsoft.EntityFrameworkCore;
using User_Login_and_Registration.Data;
using User_Login_and_Registration.Model;

namespace User_Login_and_Registration.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly YourDbContext _context;

        public UserRepository(YourDbContext context)
        {
            _context = context;
        }
        public UserModel GetUserById(int userId)
        {
            return _context.UserModel.FirstOrDefault(u => u.Id == userId); // Example
        }

        public void SaveChanges()
        {
            _context.SaveChanges(); // Commits changes to the database
        }


        public async Task<UserModel> GetUserByEmailAsync(string email)
        {
            return await _context.UserModel
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<UserModel> GetUserByIdAsync(string userId)
        {
            return await _context.UserModel
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
        {
            return await _context.UserModel
                .ToListAsync();
        }

        public async Task AddUserAsync(UserModel user)
        {
            await _context.UserModel.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(UserModel user)
        {
            _context.UserModel.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = await GetUserByIdAsync(userId);
            if (user != null)
            {
                _context.UserModel.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
