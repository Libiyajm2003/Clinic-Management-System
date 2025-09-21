using ConsoleAppCms2025.Model;
using ConsoleAppCms2025.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Service
{
    public class RoleServiceImpl : IRoleService
    {
        private readonly IRoleRepository _roleRepo;

        public RoleServiceImpl(IRoleRepository roleRepo)
        {
            _roleRepo = roleRepo;
        }

        public async Task<List<Role>> GetAllRolesAsync()
        {
            return await _roleRepo.GetAllRolesAsync();
        }

        public async Task<Role> GetRoleByIdAsync(int roleId)
        {
            return await _roleRepo.GetRoleByIdAsync(roleId);
        }
    }
}
