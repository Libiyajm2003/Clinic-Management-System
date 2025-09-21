using ConsoleAppCms2025.Model;
using System;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ConsoleAppCms2025.Repository
{
    public class UserRepositoryImpl : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepositoryImpl()
        {
            // Hard-coded connection string with Encrypt=False to avoid SSL certificate errors
            _connectionString = @"Server=localhost;Database=cmsv2025db;Trusted_Connection=True;Encrypt=False;";
        }

        public async Task<User> LoginAsync(string username, string password)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            try
            {
                await con.OpenAsync();

                string query = @"SELECT UserId, FullName, Gender, JoiningDate, MobileNumber, UserName, Password, RoleId, IsActive 
                                 FROM TblUser WHERE UserName=@username AND Password=@password";

                using SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                using SqlDataReader reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new User
                    {
                        UserId = (int)reader["UserId"],
                        FullName = reader["FullName"].ToString(),
                        Gender = Convert.ToChar(reader["Gender"]),
                        JoiningDate = Convert.ToDateTime(reader["JoiningDate"]),
                        MobileNumber = reader["MobileNumber"].ToString(),
                        UserName = reader["UserName"].ToString(),
                        Password = reader["Password"].ToString(),
                        RoleId = (int)reader["RoleId"],
                        IsActive = (bool)reader["IsActive"]
                    };
                }
            }
            catch (SqlException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Database connection failed: " + ex.Message);
                Console.ResetColor();
            }

            return null;
        }

        // CRUD placeholders
        public Task AddUserAsync(User user) => Task.CompletedTask;
        public Task DeleteUserAsync(int userId) => Task.CompletedTask;
        public Task<List<User>> GetAllUsersAsync() => Task.FromResult(new List<User>());
        public Task<User> GetUserByIdAsync(int userId) => Task.FromResult<User>(null);
        public Task UpdateUserAsync(User user) => Task.CompletedTask;
    }
}
