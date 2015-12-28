namespace LogoFX.Samples.Client.Model.Contracts.UserManagement
{
    public interface IUser : IDomainModel
    {
        string LoginName { get; }
    }
}
