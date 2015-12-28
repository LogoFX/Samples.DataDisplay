using System.Threading.Tasks;
using System.Windows.Input;
using Caliburn.Micro;
using LogoFX.Client.Mvvm.Commanding;

namespace LogoFX.Samples.Client.Presentation.Shell.ViewModels
{    
    public sealed class ChildWindowViewModel : Conductor<object>
    {
        private readonly TaskCompletionSource<bool?> _taskCompletionSource;

        public ChildWindowViewModel(
            object rootViewModel, 
            object context, 
            TaskCompletionSource<bool?> taskCompletionSource)
        {
            _taskCompletionSource = taskCompletionSource;
            Context = context;

            ActivateItem(rootViewModel);
        }

        public object Context
        {
            get;
            private set;
        }

        private ICommand _okCommand;
        public ICommand OkCommand
        {
            get
            {
                return _okCommand ??
                       (_okCommand = ActionCommand
                           .Do(() => _taskCompletionSource.SetResult(true)));
            }
        }

        private ICommand _cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand ??
                       (_cancelCommand = ActionCommand
                           .Do(() => _taskCompletionSource.SetResult(true)));
            }
        }

        private ICommand _closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                return _closeCommand ??
                       (_closeCommand = ActionCommand
                           .Do(() => _taskCompletionSource.SetResult(null)));
            }
        }

    }
}