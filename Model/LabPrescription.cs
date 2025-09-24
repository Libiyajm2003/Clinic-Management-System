using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Model
{
    /// <summary>
    /// Represents a lab prescription/result associated with an appointment.
    /// </summary>
    public class LabResult
    {
        /// <summary>
        /// Related appointment identifier.
        /// </summary>
        public int AppointmentId { get; set; }

        /// <summary>
        /// The name of the lab test performed.
        /// </summary>
        public string LabTestName { get; set; }

        /// <summary>
        /// The measured value or outcome for the test.
        /// </summary>
        public string LabTestValue { get; set; }

        /// <summary>
        /// Additional remarks or interpretation.
        /// </summary>
        public string Remarks { get; set; }
    }
}

