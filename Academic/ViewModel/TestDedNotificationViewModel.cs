using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Academic.UIComponent;

namespace Academic.ViewModel
{
    public class TestDedNotificationViewModel : BindableBase
    {
        public class DedNotification : BindableBase, VariableGridView.IResizable
        {
            private int _width;

            public string Name { get; set; }
            public string ImagePath { get; set; }

            public int Width
            {
                get {return _width;}
                set { SetProperty(ref _width, value); }
            }

            public int Height { get; set; }

            public ImageSource Image
            {
                get
                {
                    return new BitmapImage(new Uri("ms-appx://" + this.ImagePath));
                }
            }
        }

        public ObservableCollection<DedNotification> Notifications { get; } = new ObservableCollection<DedNotification>();

        public TestDedNotificationViewModel()
        {
            Notifications.Add(new DedNotification() { Name = "Beer", Height = 2, Width = 2, ImagePath = @"/Assets/Images/beer.jpg" });
            Notifications.Add(new DedNotification() { Name = "Hops", Height = 2, Width = 2, ImagePath = @"/Assets/Images/hops.jpg" });
            Notifications.Add(new DedNotification() { Name = "Malt", Height = 1, Width = 1, ImagePath = @"/Assets/Images/malt.jpg" });
            Notifications.Add(new DedNotification() { Name = "Water", Height = 1, Width = 1, ImagePath = @"/Assets/Images/water.jpg" });
            Notifications.Add(new DedNotification() { Name = "Yeast", Height = 1, Width = 1, ImagePath = @"/Assets/Images/yeast.jpg" });
            Notifications.Add(new DedNotification() { Name = "Sugar", Height = 2, Width = 1, ImagePath = @"/Assets/Images/sugars.jpg" });
            Notifications.Add(new DedNotification() { Name = "Herbs", Height = 2, Width = 1, ImagePath = @"/Assets/Images/herbs.jpg" });
            Notifications.Add(new DedNotification() { Name = "Beer", Height = 2, Width = 1, ImagePath = @"/Assets/Images/beer.jpg" });
            Notifications.Add(new DedNotification() { Name = "Hops", Height = 1, Width = 1, ImagePath = @"/Assets/Images/hops.jpg" });
            Notifications.Add(new DedNotification() { Name = "Malt", Height = 1, Width = 1, ImagePath = @"/Assets/Images/malt.jpg" });
            Notifications.Add(new DedNotification() { Name = "Water", Height = 2, Width = 1, ImagePath = @"/Assets/Images/water.jpg" });
            Notifications.Add(new DedNotification() { Name = "Yeast", Height = 1, Width = 1, ImagePath = @"/Assets/Images/yeast.jpg" });
            Notifications.Add(new DedNotification() { Name = "Sugar", Height = 1, Width = 1, ImagePath = @"/Assets/Images/sugars.jpg" });
            Notifications.Add(new DedNotification() { Name = "Herbs", Height = 2, Width = 1, ImagePath = @"/Assets/Images/herbs.jpg" });
            Notifications.Add(new DedNotification() { Name = "Beer", Height = 1, Width = 1, ImagePath = @"/Assets/Images/beer.jpg" });
            Notifications.Add(new DedNotification() { Name = "Hops", Height = 1, Width = 1, ImagePath = @"/Assets/Images/hops.jpg" });
            Notifications.Add(new DedNotification() { Name = "Malt", Height = 2, Width = 1, ImagePath = @"/Assets/Images/malt.jpg" });
            Notifications.Add(new DedNotification() { Name = "Water", Height = 1, Width = 1, ImagePath = @"/Assets/Images/water.jpg" });
            Notifications.Add(new DedNotification() { Name = "Sugar", Height = 1, Width = 1, ImagePath = @"/Assets/Images/sugars.jpg" });
            Notifications.Add(new DedNotification() { Name = "Yeast", Height = 2, Width = 1, ImagePath = @"/Assets/Images/yeast.jpg" });
            Notifications.Add(new DedNotification() { Name = "Herbs", Height = 2, Width = 1, ImagePath = @"/Assets/Images/herbs.jpg" });
        }

        private int _columnCount = 1;

        public int ColumnCount
        {
            get { return _columnCount; }
            set
            {
                if (value != _columnCount)
                {
                    _columnCount = value;
                    CalculateItemWidth();
                }
            }
        }

        private void CalculateItemWidth()
        {
            var titleCount = Math.Min(2, Notifications.Count);

            switch (titleCount)
            {
                case 0:
                    return;
                case 1:
                    Notifications[0].Width = 1;
                    break;
                default:
                    if (ColumnCount%2 == 0)
                    {
                        Notifications[0].Width = Notifications[1].Width = ColumnCount/2;
                    }
                    else
                    {
                        var h = Notifications[1].Width = ColumnCount/2;
                        Notifications[0].Width = h + 1;
                    }
                    break;
            }
        }
    }
}
