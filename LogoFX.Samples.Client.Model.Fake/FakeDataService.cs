using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using LogoFX.Client.Core;
using LogoFX.Samples.Client.Model.Contracts;
using LogoFX.Samples.Client.Model.Contracts.Compliance;
using LogoFX.Samples.Client.Model.Contracts.UserManagement;
using LogoFX.Samples.Client.Model.Fake.Compliance;
using LogoFX.Samples.Client.Model.Shared;

namespace LogoFX.Samples.Client.Model.Fake
{
    [UsedImplicitly]
    sealed class FakeDataService : NotifyPropertyChangedBase<FakeDataService>, IDataService
    {
        private const int ComplianceRecordCount = 100;

        private static readonly string[] AppNames =
        {
            "Security Update for Windows",
            "Skype",
            "TeamViewer",
            "USB Removable Storage",
            "WebEx",
            "Windows Live Messenger",
            "Windows Messenger",
            "WinPcap",
            "WinSCP",
            "Wireshark",
            "Google Talk",
        };

        private sealed class ComplianceRecord : DomainModel, IComplianceRecord
        {
            public DateTime LastDate { get; set; }
            public string Host { get; set; }
            public IPAddress IpAddress { get; set; }
            public string Object { get; set; }
            public IUser LoggedOnUser { get; set; }
            public ComplianceRecordStatus Status { get; set; }
            public string Information { get; set; }
        }

        public Task<IEnumerable<IComplianceRecord>> GetComplianceRecordsAsync(IComplianceRecordsFilter filter)
        {            
            return Task.Factory.StartNew(
            () =>
            {
                Random rnd = new Random();
                List<IComplianceRecord> result = new List<IComplianceRecord>();

                for (int i = 0; i < ComplianceRecordCount; ++i)
                {
                    result.Add(GenerateComplianceRecord(rnd, i));
                    Thread.Sleep(5);
                }

                return (IEnumerable<IComplianceRecord>)result;
            });
        }

        public IComplianceRecordsFilter CreateComplianceRecordsFilter()
        {
            return new ComplianceRecordsFilter();
        }

        private IComplianceRecord GenerateComplianceRecord(Random rnd, int index)
        {
            byte hostIndex = (byte)rnd.Next(1, 4);

            ComplianceRecord result = new ComplianceRecord
            {
                LastDate = new DateTime(2012, 1, 1) + new TimeSpan(rnd.Next(0, 100000000)*1000),
                Host = "HOST" + hostIndex,
                IpAddress = new IPAddress(new byte[] {192, 168, 0, hostIndex}),
                Object = AppNames[rnd.Next(AppNames.Length)],
                LoggedOnUser = UserContext.Current,
                Status = rnd.Next(2) == 0 ? ComplianceRecordStatus.Installed : ComplianceRecordStatus.NotInstalled,
                Information = "Record N " + (index + 1)
            };

            return result;
        }
    }
}
