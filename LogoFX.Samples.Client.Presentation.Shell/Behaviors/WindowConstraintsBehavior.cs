using System;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Threading;

namespace LogoFX.Samples.Client.Presentation.Shell.Behaviors
{
    public class WindowConstraintsBehavior : Behavior<Window>
    {
        #region Fields

        private static readonly Type s_thisType = typeof(WindowConstraintsBehavior);

        private readonly DispatcherTimer _timer;

        #endregion

        #region Constructors

        public WindowConstraintsBehavior()
        {
            _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(2000) };
            _timer.Tick += OnTimer;
        }

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty FitDesktopProperty =
            DependencyProperty.Register(
                "FitDesktop",
                typeof(bool),
                s_thisType,
                new PropertyMetadata(false, FitDesktopChanged));

        #endregion

        #region Public Properties

        public double MinHeight { get; set; }
        public double MinWidth { get; set; }

        public bool FitDesktop
        {
            get { return (bool)GetValue(FitDesktopProperty); }
            set { SetValue(FitDesktopProperty, value); }
        }

        #endregion

        #region Private Members

        private void OnTimer(object sender, EventArgs e)
        {
            OnTimer();
        }

        private void OnTimer()
        {
            if (AssociatedObject == null || !AssociatedObject.IsLoaded)
            {
                return;
            }

            _timer.Stop();

            try
            {
                if (FitDesktop)
                {
                    Rect areaRect = SystemParameters.WorkArea;
                    AssociatedObject.MinWidth = Math.Min(areaRect.Width, MinWidth);
                    AssociatedObject.MinHeight = Math.Min(areaRect.Height, MinHeight);
                }
                else
                {
                    AssociatedObject.MinHeight = MinHeight;
                    AssociatedObject.MinWidth = MinWidth;
                }
            }

            finally
            {
                _timer.Start();
            }
        }

        private static void FitDesktopChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((WindowConstraintsBehavior)d).OnFitDesktop();
        }

        private void OnFitDesktop()
        {
            bool enabled = FitDesktop && AssociatedObject != null && AssociatedObject.IsLoaded;

            if (_timer.IsEnabled == enabled)
            {
                return;
            }

            OnTimer();

            if (enabled)
            {
                _timer.Start();
            }
            else
            {
                _timer.Stop();
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            AssociatedObject.Loaded -= OnLoaded;
            DoAttach();
        }

        private void DoAttach()
        {
            AssociatedObject.Unloaded += OnUnloaded;
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            if (AssociatedObject == null)
            {
                return;
            }

            AssociatedObject.Unloaded -= OnUnloaded;
            Detach();
        }

        #endregion

        #region Overrides

        protected override void OnAttached()
        {
            base.OnAttached();

            if (AssociatedObject.IsLoaded)
            {
                DoAttach();
            }
            else
            {
                AssociatedObject.Loaded += OnLoaded;
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
        }

        #endregion
    }
}
