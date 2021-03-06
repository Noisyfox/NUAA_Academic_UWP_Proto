using System;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Academic.Common;
using Academic.ViewModel;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace Academic
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            Loaded += MainPage_Loaded;
            Unloaded += MainPage_Unloaded;
        }

        private readonly Frame _dedFrame = new Frame();

        private void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
            var av = ApplicationView.GetForCurrentView();
            av.VisibleBoundsChanged -= MainPage_VisibleBoundsChanged;
            GlobalNavigationManager.Instance.SetChlidFrame(null);
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            var av = ApplicationView.GetForCurrentView();
            av.VisibleBoundsChanged += MainPage_VisibleBoundsChanged;


            var w = av.VisibleBounds.Width;
            var h = av.VisibleBounds.Height;
            CalcDedNotificationGrid(w, h);
            CalcDedToolsView(w, h);
            _dedFrame.Navigate(typeof(EmptyPage));
            Pivot_SelectionChanged();
        }

        private void MainPage_VisibleBoundsChanged(ApplicationView sender, object args)
        {
            var w = sender.VisibleBounds.Width;
            var h = sender.VisibleBounds.Height;

            CalcDedNotificationGrid(w, h);
            CalcDedToolsView(w, h);
        }

        public TestDedNotificationViewModel DedNotificationViewModel { get; } = new TestDedNotificationViewModel();

        public AcademicToolsListsViewModel AcademicToolsViewModel { get; } = new AcademicToolsListsViewModel();

        private void CalcDedNotificationGrid(double w, double h)
        {
            var vgv = DedNotificationGrid.ItemsPanelRoot as VariableSizedWrapGrid;
            if (vgv == null)
            {
                throw new InvalidCastException("");
            }

            var bW = w;

            int columnCount;

            if (bW < 640)
            {
                columnCount = 1;
            }
            else
            {
                var columnMax = bW / 300.0;
                columnCount = (int)columnMax;
            }

            DedNotificationViewModel.ColumnCount = columnCount;
            vgv.MaximumRowsOrColumns = columnCount;
            vgv.ItemWidth = bW / columnCount;
        }

        private void CalcDedToolsView(double w, double h)
        {
            
        }

        private void Pivot_SelectionChanged(object sender = null, SelectionChangedEventArgs e = null)
        {
            if (MainPivot.SelectedIndex == 2)
            {
                DedToolsFullRoot.Visibility = Visibility.Visible;
                UpdateDedToolsPage(AdaptiveStates.CurrentState);

                GlobalNavigationManager.Instance.SetChlidFrame(_dedFrame);
            }
            else
            {
                DedToolsFullRoot.Visibility = Visibility.Collapsed;
                UpdateDedToolsPage(DefaultState);

                GlobalNavigationManager.Instance.SetChlidFrame(null);
            }
        }

        private void AdaptiveStates_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            UpdateDedToolsPage(e.NewState);
        }

        private void UpdateDedToolsPage(VisualState newState)
        {
            if (newState == DefaultState)
            {
                DedToolsFullRoot.Child = null;
                DedToolsSplitRoot.Child = _dedFrame;
                AcademicToolsViewModel.CanSelect = true;
            }
            else
            {
                DedToolsSplitRoot.Child = null;
                DedToolsFullRoot.Child = _dedFrame;
                AcademicToolsViewModel.CanSelect = false;
            }
        }
    }
}
