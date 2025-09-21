using ConsoleAppCms2025.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ConsoleAppCms2025.Service
{
    public interface IUserService
    {
        Task<User> LoginAsync(string username, string password);
        Task<User> AuthenticateUserByRoleIdAsync(string username, string password, int roleId);
        Task AddUserAsync(User user);
        Task DeleteUserAsync(int userId);
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int userId);
        Task UpdateUserAsync(User user);
    }
}


