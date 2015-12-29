using LogoFX.Client.Bootstrapping.Adapters.Unity;
using LogoFX.Client.Mvvm.Navigation;
using LogoFX.Client.Mvvm.ViewModel.Contracts;
using LogoFX.Client.Mvvm.ViewModel.Services;
using LogoFX.Client.Mvvm.ViewModelFactory.Unity;
using LogoFX.Samples.Client.Presentation.Shell.UiServices;
using LogoFX.Samples.Client.Presentation.Shell.ViewModels;

namespace LogoFX.Samples.Client.Presentation.Shell
{
    /// <summary>
    /// Entry point for the app. The bootstrapper is responsible for 
    /// all initial setup and displaying the root view bound to its data context.
    /// The bootstrapper will work with any IoC container adapter, making replacing 
    /// the IoC container easier.
    /// </summary>
	public sealed class AppBootstrapper : NavigationBootstrapper<ShellViewModel, UnityContainerAdapter>
	{
        public AppBootstrapper()
            :base(new UnityContainerAdapter())
        {
            
        }

	    protected override void OnConfigure(UnityContainerAdapter container)
		{
            base.OnConfigure(container);            
            
            container.RegisterSingleton<IShellCloseService, ShellViewModel>(); 
            container.RegisterSingleton<IViewModelCreatorService, ViewModelCreatorService>();
            container.RegisterSingleton<IViewModelFactory, ViewModelFactory>();
        }	    
	}
}