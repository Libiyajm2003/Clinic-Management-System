using ConsoleAppCms2025.Model;
using ConsoleAppCms2025.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Service
{
    public class UserServiceImpl : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserServiceImpl() { _userRepo = new UserRepositoryImpl(); }
        public UserServiceImpl(IUserRepository userRepo) { _userRepo = userRepo; }

        public Task<User> LoginAsync(string username, string password) => _userRepo.LoginAsync(username, password);
        public Task AddUserAsync(User user) => _userRepo.AddUserAsync(user);
        public Task DeleteUserAsync(int userId) => _userRepo.DeleteUserAsync(userId);
        public Task<List<User>> GetAllUsersAsync() => _userRepo.GetAllUsersAsync();
        public Task<User> GetUserByIdAsync(int userId) => _userRepo.GetUserByIdAsync(userId);
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
