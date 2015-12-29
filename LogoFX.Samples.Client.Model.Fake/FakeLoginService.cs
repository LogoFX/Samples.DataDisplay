using System.Security;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using LogoFX.Samples.Client.Model.Contracts;
using LogoFX.Samples.Client.Model.Fake.UserManagement;
using LogoFX.Samples.Client.Model.Shared;

namespace LogoFX.Samples.Client.Model.Fake
{
    [UsedImplicitly]
    class FakeLoginService : ILoginService
    {        
        public Task<bool> Login(string loginName, string password, bool persist = false)
        {
            return Task.Factory.StartNew(() =>
            {
                Thread.Sleep(100);

                if (loginName == "e")
                {
                    throw new SecurityException("Wrong user name or password.");
                }

                UserContext.Current = new LocalUser
                {
                    Name = loginName,
                    LoginName = loginName,
                };

                return true;
            });
        }

        public Task<bool> Logout()
        {
            return Task.Factory.StartNew(() =>
            {
                Thread.Sleep(100);                
                UserContext.Current = null;
                return true;
            });
        }
    }
}
