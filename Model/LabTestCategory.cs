using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Model
{
    /// <summary>
    /// Groups related laboratory tests under a category.
    /// </summary>
    public class LabTestCategory
    {
        /// <summary>
        /// Unique category identifier.
        /// </summary>
        public int LabTestCategoryId { get; set; }

        /// <summary>
        /// Category display name.
        /// </summary>
        public string LabTestCategoryName { get; set; }
    }
}
