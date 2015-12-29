using System.ComponentModel.Composition;
using LogoFX.Client.Bootstrapping.Adapters.Unity;
using LogoFX.Samples.Client.Model.Contracts;
using Solid.Practices.Modularity;

namespace LogoFX.Samples.Client.Model.Fake
{ 
    [Export(typeof(ICompositionModule))]
    class Module : ICompositionModule<UnityContainerAdapter>
    {
        public void RegisterModule(UnityContainerAdapter container)
        {
            container.RegisterSingleton<ILoginService, FakeLoginService>();
            container.RegisterSingleton<IDataService, FakeDataService>();
        }
    }
}
