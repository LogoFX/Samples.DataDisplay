using System.ComponentModel.Composition;
using LogoFX.Client.Bootstrapping.Adapters.Unity;
using LogoFX.Samples.Client.Model.Contracts;
using Solid.Practices.Modularity;

namespace LogoFX.Samples.Client.Model.Fake
{ 
    [Export(typeof(ICompositionModule))]
    class Module : ICompositionModule<UnityIocContainer>
    {
        public void RegisterModule(UnityIocContainer container)
        {
            container.RegisterSingleton<ILoginService, FakeLoginService>();
            container.RegisterSingleton<IDataService, FakeDataService>();
        }
    }
}
