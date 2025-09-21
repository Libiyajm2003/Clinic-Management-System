using ConsoleAppCms2025.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ConsoleAppCms2025.Repository
{
    public interface IUserRepository
    {
        Task<User> LoginAsync(string username, string password);
        Task AddUserAsync(User user);
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int userId);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int userId);
    }
}


