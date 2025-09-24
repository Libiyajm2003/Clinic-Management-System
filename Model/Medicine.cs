using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Model
{
    /// <summary>
    /// Represents a medicine item that can be prescribed.
    /// </summary>
    public class Medicine
    {
        /// <summary>
        /// Unique medicine identifier.
        /// </summary>
        public int MedicineId { get; set; }

        /// <summary>
        /// Name of the medicine.
        /// </summary>
        public string MedicineName { get; set; }

        /// <summary>
        /// Category identifier the medicine belongs to.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Unit of measure (e.g., tablet, ml).
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Created timestamp.
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Whether the medicine is active.
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
}
