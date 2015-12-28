using System;
using LogoFX.Client.Mvvm.ViewModel;
using LogoFX.Samples.Client.Model.Contracts.Compliance;

namespace LogoFX.Samples.Client.Presentation.Shell.Compliance.ViewModels
{
    public sealed class ComplianceRecordsFilterViewModel : ObjectViewModel<IComplianceRecordsFilter>        
    {
        private readonly string _name;

        public ComplianceRecordsFilterViewModel(string name, IComplianceRecordsFilter model)
            : base(model)
        {
            _name = name;
        }

        public override string DisplayName
        {
            get { return _name; }
        }

        private bool _enabledDateFiltering;
        public bool EnabledDateFiltering
        {
            get { return _enabledDateFiltering; }
            set
            {
                if (_enabledDateFiltering == value)
                {
                    return;
                }

                _enabledDateFiltering = value;
                NotifyOfPropertyChange();
            }
        }

        private bool _lastDays;
        public bool LastDays
        {
            get { return _lastDays; }
            set
            {
                if (_lastDays == value)
                {
                    return;
                }

                _lastDays = value;
                if (_lastDays)
                {
                    DaysToRange();
                }
                NotifyOfPropertyChange();
            }
        }

        private bool _dateRange;
        public bool DateRange
        {
            get { return _dateRange; }
            set
            {
                if (_dateRange == value)
                {
                    return;
                }

                _dateRange = value;
                NotifyOfPropertyChange();
            }
        }

        public DateTime StartTime
        {
            get { return Model.StartTime ?? DateTime.Now; }
            set
            {
                if (Model.StartTime.HasValue && StartTime == value)
                {
                    return;
                }

                Model.StartTime = value;
                NotifyOfPropertyChange();
            }
        }

        public DateTime EndTime
        {
            get { return Model.EndTime ?? DateTime.Now; }
            set
            {
                if (Model.EndTime.HasValue && EndTime == value)
                {
                    return;
                }

                Model.EndTime = value;
                NotifyOfPropertyChange();
            }
        }

        private int _lastDaysCount;
        public int LastDaysCount
        {
            get { return _lastDaysCount; }
            set
            {
                if (_lastDaysCount == value)
                {
                    return;
                }

                _lastDaysCount = value;
                if (LastDays)
                {
                    DaysToRange();
                }
                NotifyOfPropertyChange();
            }
        }

        private void DaysToRange()
        {
            EndTime = DateTime.Now;
            StartTime = EndTime - TimeSpan.FromDays(LastDaysCount);
        }        
    }
}