using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Model
{
    public class LabResult
    {
        public int AppointmentId { get; set; }
        public string LabTestName { get; set; }
        public string LabTestValue { get; set; }
        public string Remarks { get; set; }
    }
}

