using System.Linq;
using Caliburn.Micro;
using LogoFX.Samples.Client.Model.Contracts;

namespace LogoFX.Samples.Client.Presentation.Shell.Compliance.ViewModels
{
    public sealed class ComplianceRecordsFiltersViewModel :
        Conductor<ComplianceRecordsFilterViewModel>.Collection.OneActive        
    {
        private readonly IDataService _dataService;

        public ComplianceRecordsFiltersViewModel(IDataService dataService)
        {
            _dataService = dataService;
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            ActivateItem(new ComplianceRecordsFilterViewModel("Default", _dataService.CreateComplianceRecordsFilter())
            {
                EnabledDateFiltering = true,
                LastDays = true,
            });

            ActivateItem(new ComplianceRecordsFilterViewModel("Not default", _dataService.CreateComplianceRecordsFilter())
            {
                EnabledDateFiltering = false,
                DateRange = true,
            });

            ActivateItem(Items.First());
        }        
    }
}