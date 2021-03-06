﻿using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Caliburn.Micro;
using LogoFX.Client.Mvvm.Commanding;
using LogoFX.Client.Mvvm.Navigation;
using LogoFX.Samples.Client.Model.Contracts;
using LogoFX.Samples.Client.Model.Shared;
using LogoFX.Samples.Client.Presentation.Shell.UiServices;
using Solid.Practices.Scheduling;

namespace LogoFX.Samples.Client.Presentation.Shell.ViewModels
{    	
    public sealed class ShellViewModel : 
        Conductor<IScreen>, 
        INavigationConductor,         
        IShellCloseService
	{
	    private readonly ILoginService _loginService;
        private readonly INavigationService _navigationService;
        private readonly TaskFactory _taskFactory = TaskFactoryFactory.CreateTaskFactory();

	    public ShellViewModel(
            ILoginService loginService, 
            INavigationService navigationService)
        {            
            _loginService = loginService;
            _navigationService = navigationService;

            UserContext.CurrentChanged += CurrentChanged;
        }

	    private ICommand _logoutCommand;
        public ICommand LogoutCommand
        {
            get
            {
                return _logoutCommand ??
                       (_logoutCommand = ActionCommand
                           .When(() => UserContext.Current != null)
                           .Do(async () =>
                           {
                               await _loginService.Logout();
                               ActiveItem.Deactivated += OnDeactivated;
                               ActivateItem(null);
                           }));
            }
        }

	    public override string DisplayName
		{
			get { return "Logo FX - Data Display"; }
			set {  }
		}

	    public bool IsLoggedIn
	    {
	        get { return UserContext.Current != null; }
	    }

	    public string CurrentUser
	    {
	        get { return UserContext.Current == null ? string.Empty : UserContext.Current.LoginName; }
	    }

	    private void CurrentChanged(object sender, EventArgs eventArgs)
        {
            NotifyOfPropertyChange(() => IsLoggedIn);
            NotifyOfPropertyChange(() => CurrentUser);            
        }

        private async Task GotoLogin()
        {
            await
                _taskFactory.StartNew(
                    () => Dispatch.Current.BeginOnUiThread(() => _navigationService.Navigate<LoginViewModel>()));            
        }

        private async void OnDeactivated(object sender, DeactivationEventArgs e)
        {
            ((IDeactivate)sender).Deactivated -= OnDeactivated;
            await GotoLogin();
        }

        private async void Logout()
        {
            await _loginService.Logout();
            Application.Current.Shutdown();
        }

	    protected async override void OnActivate()
        {
            base.OnActivate();            
            await GotoLogin();
        }

        protected override void OnDeactivate(bool close)
        {           
            base.OnDeactivate(close);
// ReSharper disable DelegateSubtraction
            UserContext.CurrentChanged -= CurrentChanged;
// ReSharper restore DelegateSubtraction
        }

        public override void CanClose(Action<bool> callback)
        {
            base.CanClose(canClose =>
            {
                if (canClose && UserContext.Current != null)
                {
                    callback(false);
                    Logout();
                }
                else
                {
                    callback(canClose);
                }
            });
        }

	    public void NavigateTo(object viewModel, object argument)
        {
            ActivateItem((IScreen)viewModel);
        }	   

	    public async void Close()
	    {
	        await CloseAsync();
	    }

        private async Task CloseAsync()
        {
            await _taskFactory.StartNew(() => Dispatch.Current.OnUiThread(() =>
            {
                TryClose();
            }));
        }
	}
}