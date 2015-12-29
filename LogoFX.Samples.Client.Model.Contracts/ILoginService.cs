using System.Threading.Tasks;

namespace LogoFX.Samples.Client.Model.Contracts
{
    public interface ILoginService
    {
        Task<bool> Login(string loginName, string password, bool persist = false);
        Task<bool> Logout();
    }
}
