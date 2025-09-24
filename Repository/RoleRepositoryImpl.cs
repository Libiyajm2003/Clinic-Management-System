using ConsoleAppCms2025.Model;
using ConsoleAppCms2025.Model;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Repository
{
    /// <summary>
    /// SQL Server implementation of role data access operations.
    /// </summary>
    public class RoleRepositoryImpl : IRoleRepository
    {
        private readonly string _connectionString;

        public RoleRepositoryImpl()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["Cmsv2025Db"].ConnectionString;
        }

        /// <inheritdoc />
        public async Task<List<Role>> GetAllRolesAsync()
        {
            var roles = new List<Role>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT RoleId, RoleName, IsActive FROM TblRole", conn);
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    roles.Add(new Role
                    {
                        RoleId = reader.GetInt32(0),
                        RoleName = reader.GetString(1),
                        IsActive = reader.GetBoolean(2)
                    });
                }
            }
            return roles;
        }

        /// <inheritdoc />
        public async Task<Role> GetRoleByIdAsync(int roleId)
        {
            Role role = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT RoleId, RoleName, IsActive FROM TblRole WHERE RoleId=@RoleId", conn);
                cmd.Parameters.AddWithValue("@RoleId", roleId);

                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    role = new Role
                    {
                        RoleId = reader.GetInt32(0),
                        RoleName = reader.GetString(1),
                        IsActive = reader.GetBoolean(2)
                    };
                }
            }
            return role;
        }
    }
}
