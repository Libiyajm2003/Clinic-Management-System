using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Model
{
    /// <summary>
    /// Represents a medical specialization for doctors (e.g., Cardiology).
    /// </summary>
    public class Specialization
    {
        /// <summary>
        /// Unique specialization identifier.
        /// </summary>
        public int SpecializationId { get; set; }
        /// <summary>
        /// Display name of the specialization.
        /// </summary>
        public string SpecializationName { get; set; }
        /// <summary>
        /// Whether this specialization is active.
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
}
