using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace Academic
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ScorePage : Page
    {
        public ScorePage()
        {
            this.InitializeComponent();

            Loaded += Page_Loaded;
            Unloaded += Page_Unloaded;
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            PageRoot.SizeChanged -= PageRoot_SizeChanged;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            PageRoot.SizeChanged += PageRoot_SizeChanged;


            var w = PageRoot.ActualWidth;
            var h = PageRoot.ActualHeight;
            CalcGrid(w, h);
        }

        private void PageRoot_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var w = e.NewSize.Width;
            var h = e.NewSize.Height;
            CalcGrid(w, h);
        }

        private void CalcGrid(double w, double h)
        {
            var vgv = DataGrid.ItemsPanelRoot as VariableSizedWrapGrid;
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

            vgv.MaximumRowsOrColumns = columnCount;
            vgv.ItemWidth = bW / columnCount;
        }
    }
}
