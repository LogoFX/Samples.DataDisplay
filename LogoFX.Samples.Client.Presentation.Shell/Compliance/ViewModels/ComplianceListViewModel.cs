using System.Collections;
using System.Threading.Tasks;
using Caliburn.Micro;
using LogoFX.Client.Mvvm.ViewModel;
using LogoFX.Samples.Client.Model.Contracts;
using LogoFX.Samples.Client.Model.Contracts.Compliance;

namespace LogoFX.Samples.Client.Presentation.Shell.Compliance.ViewModels
{
    public sealed class ComplianceListViewModel : Screen
    {
        private readonly IDataService _dataService;        

        public ComplianceListViewModel(IDataService dataService)
        {
            _dataService = dataService;
            _items = new WrappingCollection
            {
                FactoryMethod = o => new ComplianceRecordViewModel((IComplianceRecord) o)
            };
        }

        private readonly WrappingCollection _items;
        public IEnumerable Items
        {
            get { return _items; }
        }

        public async Task Update(IComplianceRecordsFilter filter)
        {
            _items.ClearSources();
            var dataItems = await _dataService.GetComplianceRecordsAsync(filter);
            _items.AddSource(dataItems);
        }
    }
}