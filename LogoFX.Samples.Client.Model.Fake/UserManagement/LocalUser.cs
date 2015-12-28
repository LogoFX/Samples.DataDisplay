using LogoFX.Samples.Client.Model.Contracts.UserManagement;

namespace LogoFX.Samples.Client.Model.Fake.UserManagement
{
    sealed class LocalUser : UserBase, ILocalUser
    {
        public bool IsAdmin { get; set; }
    }
}
