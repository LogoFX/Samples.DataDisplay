using LogoFX.Client.Mvvm.ViewModel;
using LogoFX.Samples.Client.Model.Contracts.Compliance;

namespace LogoFX.Samples.Client.Presentation.Shell.Compliance.ViewModels
{
    public sealed class ComplianceRecordViewModel : ObjectViewModel<IComplianceRecord>       
    {
        public ComplianceRecordViewModel(IComplianceRecord model)
            : base(model)
        {
            
        }
    }
}