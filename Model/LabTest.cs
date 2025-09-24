using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Model
{
    /// <summary>
    /// Represents a laboratory test definition.
    /// </summary>
    public class LabTest
    {
        /// <summary>
        /// Unique lab test identifier.
        /// </summary>
        public int LabTestId { get; set; }

        /// <summary>
        /// Human-readable test name.
        /// </summary>
        public string TestName { get; set; }

        /// <summary>
        /// Standard price for the test.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Reference minimum range for the test result.
        /// </summary>
        public string MinRange { get; set; }

        /// <summary>
        /// Reference maximum range for the test result.
        /// </summary>
        public string MaxRange { get; set; }

        /// <summary>
        /// Sample required for this test.
        /// </summary>
        public string SampleRequired { get; set; }

        /// <summary>
        /// Category identifier this test belongs to.
        /// </summary>
        public int LabTestCategoryId { get; set; }
    }
}
