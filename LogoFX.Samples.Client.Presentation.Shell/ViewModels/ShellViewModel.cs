using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Caliburn.Micro;
using LogoFX.Client.Mvvm.Commanding;
using LogoFX.Client.Mvvm.Navigation;
using LogoFX.Samples.Client.Model.Contracts;
using LogoFX.Samples.Client.Model.Shared;
using LogoFX.Samples.Client.Presentation.Shell.UiServices;

namespace LogoFX.Samples.Client.Presentation.Shell.ViewModels
{    	
    public sealed class ShellViewModel : Conductor<IScreen>, 
        INavigationConductor, 
        IWindowManager,        
        IShellCloseService
	{
	    private readonly ILoginService _loginService;
        private readonly INavigationService _navigationService;	    

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
                               await _loginService.LogOut();
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

        private void GotoLogin()
        {
            Task.Factory.StartNew(() => Execute.BeginOnUIThread(() => _navigationService.Navigate<LoginViewModel>()));            
        }

        private void OnDeactivated(object sender, DeactivationEventArgs e)
        {
            ((IDeactivate)sender).Deactivated -= OnDeactivated;
            GotoLogin();
        }

        private async void Logout()
        {
            await _loginService.LogOut();
            Application.Current.Shutdown();
        }

	    protected override void OnActivate()
        {
            base.OnActivate();
            ScreenExtensions.TryActivate(ChildWindow);
            GotoLogin();
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            GC.Collect();
        }

        protected override void OnDeactivate(bool close)
        {
            ScreenExtensions.TryDeactivate(ChildWindow, close);
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

	    private ChildWindowViewModel _childWindow;
        public ChildWindowViewModel ChildWindow
        {
            get { return _childWindow; }
            set
            {
                if (_childWindow == value)
                {
                    return;
                }

                if (_childWindow != null)
                {
                    ScreenExtensions.TryDeactivate(_childWindow, true);
                }

                _childWindow = value;

                if (_childWindow != null)
                {
                    ScreenExtensions.TryActivate(_childWindow);
                }

                NotifyOfPropertyChange();
            }
        }

        private Task<bool?> ShowWindowAsync(object rootModel, object context, IDictionary<string, object> settings)
        {
            var taskCompletionSource = new TaskCompletionSource<bool?>();
            ChildWindow = new ChildWindowViewModel(rootModel, context, taskCompletionSource);

            Task.Factory.StartNew(() =>
            {
                taskCompletionSource.Task.Wait();
                ChildWindow = null;
            });            

            return taskCompletionSource.Task;
        }

        public bool? ShowDialog(object rootModel, object context = null, IDictionary<string, object> settings = null)
        {
            bool? result = ShowWindowAsync(rootModel, context, settings).Result;
            return result;
        }

        public async void ShowWindow(object rootModel, object context = null, IDictionary<string, object> settings = null)
        {
            await ShowWindowAsync(rootModel, context, settings);
        }

        public void ShowPopup(object rootModel, object context = null, IDictionary<string, object> settings = null)
        {
            throw new NotImplementedException();
        }

	    public void Close()
	    {
	        TryClose();
	    }
	}
}