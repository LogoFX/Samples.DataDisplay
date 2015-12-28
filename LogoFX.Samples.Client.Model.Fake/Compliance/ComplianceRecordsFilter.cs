using System;
using LogoFX.Samples.Client.Model.Contracts.Compliance;

namespace LogoFX.Samples.Client.Model.Fake.Compliance
{
    public sealed class ComplianceRecordsFilter : IComplianceRecordsFilter
    {
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}