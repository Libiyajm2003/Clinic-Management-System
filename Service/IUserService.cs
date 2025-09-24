using ConsoleAppCms2025.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ConsoleAppCms2025.Service
{
    /// <summary>
    /// Exposes user authentication and CRUD operations.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Authenticates a user by username and password.
        /// </summary>
        Task<User> LoginAsync(string username, string password);
        /// <summary>
        /// Authenticates and verifies role id.
        /// </summary>
        Task<User> AuthenticateUserByRoleIdAsync(string username, string password, int roleId);
        /// <summary>
        /// Adds a new user.
        /// </summary>
        Task AddUserAsync(User user);
        /// <summary>
        /// Deletes a user by identifier.
        /// </summary>
        Task DeleteUserAsync(int userId);
        /// <summary>
        /// Returns all users.
        /// </summary>
        Task<List<User>> GetAllUsersAsync();
        /// <summary>
        /// Gets a user by identifier.
        /// </summary>
        Task<User> GetUserByIdAsync(int userId);
        /// <summary>
        /// Updates a user record.
        /// </summary>
        Task UpdateUserAsync(User user);
    }
}


