using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Model
{
    /// <summary>
    /// Represents an application user account.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Unique user identifier.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Full name of the user.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gender character (e.g., M/F).
        /// </summary>
        public char Gender { get; set; }

        /// <summary>
        /// Joining date of the user in the organization.
        /// </summary>
        public DateTime JoiningDate { get; set; }

        /// <summary>
        /// Primary mobile number.
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// Username for login.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Password hash or raw password (to be hashed in production).
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Role identifier mapping to role table.
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Whether the user account is active.
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
}
