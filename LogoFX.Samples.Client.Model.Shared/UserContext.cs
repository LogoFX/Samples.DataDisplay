using System;
using LogoFX.Samples.Client.Model.Contracts.UserManagement;

namespace LogoFX.Samples.Client.Model.Shared
{
    public static class UserContext
    {
        private static IUser _currentUser;

        public static event EventHandler CurrentChanged = delegate { };

        public static IUser Current
        {
            get { return _currentUser; }
            set
            {
                if (_currentUser == value)
                {
                    return;
                }

                _currentUser = value;
                OnCurrentChanged();
            }
        }

        private static void OnCurrentChanged()
        {
            CurrentChanged(null, EventArgs.Empty);
        }
    }
}
