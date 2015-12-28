using System;
using LogoFX.Client.Mvvm.Model.Contracts;

namespace LogoFX.Samples.Client.Model.Contracts.Compliance
{
    public interface IComplianceRecordsFilter : IFilterModel
    {
        DateTime? StartTime { get; set; }
        DateTime? EndTime { get; set; }
    }
}