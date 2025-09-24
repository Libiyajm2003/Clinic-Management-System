using ConsoleAppCms2025.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Service
{
    /// <summary>
    /// Exposes role look-up operations.
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// Returns all roles.
        /// </summary>
        Task<List<Role>> GetAllRolesAsync();
        /// <summary>
        /// Retrieves a role by identifier.
        /// </summary>
        Task<Role> GetRoleByIdAsync(int roleId);
    }
}
