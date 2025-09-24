using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Model
{
    /// <summary>
    /// Represents a membership plan or type assigned to patients.
    /// </summary>
    public class Membership
    {
        /// <summary>
        /// Unique membership identifier.
        /// </summary>
        public int MembershipId { get; set; }

        /// <summary>
        /// Membership type or plan name.
        /// </summary>
        public string MembershipType { get; set; }

        /// <summary>
        /// Whether the membership is active.
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
}
