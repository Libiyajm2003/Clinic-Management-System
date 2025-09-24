using ConsoleAppCms2025.Model;
using ConsoleAppCms2025.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Service
{
    /// <summary>
    /// Implements role look-up operations.
    /// </summary>
    public class RoleServiceImpl : IRoleService
    {
        private readonly IRoleRepository _roleRepo;

        /// <summary>
        /// Creates a new instance of <see cref="RoleServiceImpl"/>.
        /// </summary>
        public RoleServiceImpl(IRoleRepository roleRepo)
        {
            _roleRepo = roleRepo;
        }

        /// <inheritdoc />
        public async Task<List<Role>> GetAllRolesAsync()
        {
            return await _roleRepo.GetAllRolesAsync();
        }

        /// <inheritdoc />
        public async Task<Role> GetRoleByIdAsync(int roleId)
        {
            return await _roleRepo.GetRoleByIdAsync(roleId);
        }
    }
}
