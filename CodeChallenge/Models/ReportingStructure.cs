using System;

namespace CodeChallenge.Models
{
    public class ReportingStructure
    {
        public String ReportingStructureId { get; set; }
        public Employee Employee { get; set; }
        public int NumberOfReport { get; set; }
    }
}

