using LogoFX.Samples.Client.Model.Contracts.UserManagement;

namespace LogoFX.Samples.Client.Model.Fake.UserManagement
{
    class UserBase : DomainModel, IUser
    {
        public string LoginName { get; set; }
        public string Password { get; set; }
    }
}
