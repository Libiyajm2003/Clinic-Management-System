using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Model
{
    /// <summary>
    /// Represents a category for grouping medicines.
    /// </summary>
    public class MedicineCategory
    {
        /// <summary>
        /// Unique category identifier.
        /// </summary>
        public int MedicineCategoryId { get; set; }

        /// <summary>
        /// Category name.
        /// </summary>
        public string CategoryName { get; set; }
    }
}
