using System;
using System.ComponentModel;
using System.Net;
using LogoFX.Samples.Client.Model.Contracts.UserManagement;

namespace LogoFX.Samples.Client.Model.Contracts.Compliance
{
    public interface IComplianceRecord : IDomainModel
    {
        DateTime LastDate { get; }

        string Host { get; }

        IPAddress IpAddress { get; }

        string Object { get; }

        IUser LoggedOnUser { get; }

        ComplianceRecordStatus Status { get; }

        string Information { get; }
    }

    public enum ComplianceRecordStatus
    {
        [Description("Not Installed")]
        NotInstalled,
        [Description("Installed")]
        Installed,
    }
}
