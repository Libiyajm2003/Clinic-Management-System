using ConsoleAppCms2025.Model;
using ConsoleAppCms2025.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Service
{
    /// <summary>
    /// Implements user authentication and CRUD operations by delegating to repository.
    /// </summary>
    public class UserServiceImpl : IUserService
    {
        private readonly IUserRepository _userRepo;

        /// <summary>
        /// Default constructor wiring to concrete repository.
        /// </summary>
        public UserServiceImpl() { _userRepo = new UserRepositoryImpl(); }
        /// <summary>
        /// DI-friendly constructor accepting repository.
        /// </summary>
        public UserServiceImpl(IUserRepository userRepo) { _userRepo = userRepo; }

        /// <inheritdoc />
        public Task<User> LoginAsync(string username, string password) => _userRepo.LoginAsync(username, password);
        /// <inheritdoc />
        public Task AddUserAsync(User user) => _userRepo.AddUserAsync(user);
        /// <inheritdoc />
        public Task DeleteUserAsync(int userId) => _userRepo.DeleteUserAsync(userId);
        /// <inheritdoc />
        public Task<List<User>> GetAllUsersAsync() => _userRepo.GetAllUsersAsync();
        /// <inheritdoc />
        public Task<User> GetUserByIdAsync(int userId) => _userRepo.GetUserByIdAsync(userId);
        /// <inheritdoc />
        public Task UpdateUserAsync(User user) => _userRepo.UpdateUserAsync(user);

        public async Task<User> AuthenticateUserByRoleIdAsync(string username, string password, int roleId)
        {
            var user = await _userRepo.LoginAsync(username, password);
            if (user != null && user.RoleId == roleId)
                return user;
            return null;
        }
    }
}
