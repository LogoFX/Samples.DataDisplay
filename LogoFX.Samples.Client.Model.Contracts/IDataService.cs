using System.Collections.Generic;
using System.Threading.Tasks;
using LogoFX.Samples.Client.Model.Contracts.Compliance;

namespace LogoFX.Samples.Client.Model.Contracts
{
    public interface IDataService
    {                
        Task<IEnumerable<IComplianceRecord>> GetComplianceRecordsAsync(IComplianceRecordsFilter filter);
        IComplianceRecordsFilter CreateComplianceRecordsFilter();
    }
}
