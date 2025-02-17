using System;
using Microsoft.EntityFrameworkCore;
using User_Login_and_Registration.Data;
using User_Login_and_Registration.Model;
using User_Login_and_Registration.Repositories.Interfaces;

namespace User_Login_and_Registration.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly YourDbContext _context;

        public AdminRepository(YourDbContext context)
        {
            _context = context;
        }

        // Get admin by adminId
        public async Task<Admin> GetAdminByIdAsync(string adminId)
        {
            return await _context.Admin
                                 .Where(a => a.AdminId == adminId)
                                 .FirstOrDefaultAsync();
        }
    }

}
