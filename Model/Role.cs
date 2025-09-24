using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Model
{
    /// <summary>
    /// Represents a user role for access control.
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Unique role identifier.
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Role name (e.g., Receptionist, Doctor).
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Whether the role is active.
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
}
