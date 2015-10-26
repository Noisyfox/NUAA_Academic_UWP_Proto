using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
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

            var av = ApplicationView.GetForCurrentView();
            av.VisibleBoundsChanged += MainPage_VisibleBoundsChanged;
            Loaded += DedNotificationGrid_Loaded;
        }

        private void DedNotificationGrid_Loaded(object sender, RoutedEventArgs e)
        {
            CalcDedNotificationGrid(ApplicationView.GetForCurrentView());
        }

        private void MainPage_VisibleBoundsChanged(ApplicationView sender, object args)
        {
            CalcDedNotificationGrid(sender);
        }

        public TestDedNotificationViewModel DedNotificationViewModel { get; } = new TestDedNotificationViewModel();

        public AcademicToolsListsViewModel AcademicToolsViewModel { get; } = new AcademicToolsListsViewModel();

        private void CalcDedNotificationGrid(ApplicationView v)
        {
            var vgv = DedNotificationGrid.ItemsPanelRoot as VariableSizedWrapGrid;
            if (vgv == null)
            {
                throw new InvalidCastException("");
            }

            var bW = v.VisibleBounds.Width;

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
    }
}
