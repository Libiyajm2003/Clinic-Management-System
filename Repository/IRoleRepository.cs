using ConsoleAppCms2025.Model;
using ConsoleAppCms2025.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Repository
{
    /// <summary>
    /// Data access operations for roles.
    /// </summary>
    public interface IRoleRepository
    {
        /// <summary>
        /// Returns all roles.
        /// </summary>
        Task<List<Role>> GetAllRolesAsync();
        /// <summary>
        /// Retrieves role by identifier.
        /// </summary>
        Task<Role> GetRoleByIdAsync(int roleId);
    }
}
