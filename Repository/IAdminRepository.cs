using User_Login_and_Registration.Model;

namespace User_Login_and_Registration.Repositories.Interfaces
{
    public interface IAdminRepository
    {
        Task<Admin> GetAdminByIdAsync(string adminId);
    }

}
