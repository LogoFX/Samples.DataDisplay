namespace LogoFX.Samples.Client.Model.Contracts.UserManagement
{
    public interface ILocalUser : IUser
    {
        bool IsAdmin { get; }
    }
}
