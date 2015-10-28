using System;
using System.Runtime.InteropServices;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Academic.Common
{
    public sealed class GlobalNavigationManager
    {
        private GlobalNavigationManager()
        {
            // Register a handler for BackRequested events and set the
            // visibility of the Back button
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
        }

        public static GlobalNavigationManager Instance { get; } = new GlobalNavigationManager();

        public Frame RootFrame { get; private set; }

        public void RegisterRootFrame(Frame f)
        {
            RootFrame = f;

            f.Navigated += OnNavigated;

            UpdateNavigateState();
        }

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            UpdateNavigateState();
        }

        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            if (_childFrame != null)
            {
                if (_childFrame.CanGoBack)
                {
                    e.Handled = true;
                    _childFrame.GoBack();
                    return;
                }
            }

            if (RootFrame.CanGoBack)
            {
                e.Handled = true;
                RootFrame.GoBack();
            }
        }

        public void ClearBackStack()
        {
            RootFrame.BackStack.Clear();
            _childFrame?.BackStack.Clear();

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                AppViewBackButtonVisibility.Collapsed;
        }

        public void Navigate([In] Type sourcePageType)
        {
            RootFrame.Navigate(sourcePageType);
        }

        public void Navigate([In] Type sourcePageType, [In] object parameter)
        {
            RootFrame.Navigate(sourcePageType, parameter);
        }

        private Frame _childFrame;

        public void SetChlidFrame(Frame f)
        {
            if (_childFrame != null)
            {
                _childFrame.Navigated -= OnNavigated;
            }
            _childFrame = f;
            _childFrame.Navigated += OnNavigated;
            UpdateNavigateState();
        }

        public void ChildNavigate([In] Type sourcePageType)
        {
            if (_childFrame != null)
            {
                _childFrame.Navigate(sourcePageType);
            }
            else
            {
                Navigate(sourcePageType);
            }
        }

        public void ChildNavigate([In] Type sourcePageType, [In] object parameter)
        {
            if (_childFrame != null)
            {
                _childFrame.Navigate(sourcePageType, parameter);
            }
            else
            {
                Navigate(sourcePageType, parameter);
            }
        }

        private void UpdateNavigateState()
        {
            // Each time a navigation event occurs, update the Back button's visibility
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                CanGoBack()
                    ? AppViewBackButtonVisibility.Visible
                    : AppViewBackButtonVisibility.Collapsed;
        }

        private bool CanGoBack()
        {
            if (_childFrame != null)
            {
                if (_childFrame.CanGoBack)
                {
                    return true;
                }
            }

            return RootFrame.CanGoBack;
        }
    }
}
