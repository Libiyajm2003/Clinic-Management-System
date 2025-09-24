using ConsoleAppCms2025.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ConsoleAppCms2025.Repository
{
    /// <summary>
    /// Data access operations for users.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Authenticates a user by username and password.
        /// </summary>
        Task<User> LoginAsync(string username, string password);

        /// <summary>
        /// Adds a new user.
        /// </summary>
        Task AddUserAsync(User user);

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        Task<List<User>> GetAllUsersAsync();

        /// <summary>
        /// Retrieves a user by identifier.
        /// </summary>
        Task<User> GetUserByIdAsync(int userId);

        /// <summary>
        /// Updates a user record.
        /// </summary>
        Task UpdateUserAsync(User user);

        /// <summary>
        /// Deletes a user by identifier.
        /// </summary>
        Task DeleteUserAsync(int userId);
    }
}


