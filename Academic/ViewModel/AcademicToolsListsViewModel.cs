using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Academic.Common;

namespace Academic.ViewModel
{
    public sealed class AcademicToolsItem
    {

        public AcademicToolsItem(string title, string icon, uint color, Type sourcePageType)
        {
            Title = title;
            Icon = icon;
            ColorBrush = new SolidColorBrush();
            SourcePage = sourcePageType;
            ColorBrush.Color = ToColor(color);
        }

        public string Title { get; private set; }

        public string Icon { get; private set; }

        public SolidColorBrush ColorBrush { get; private set; }

        public Type SourcePage { get; private set; }

        private static Color ToColor(uint value)
        {

            return Color.FromArgb((byte)((value >> 24) & 0xFF),

                       (byte)((value >> 16) & 0xFF),

                       (byte)((value >> 8) & 0xFF),

                       (byte)(value & 0xFF));

        }
    }

    /// <summary>
    ///  教务功能列表定义
    /// </summary>
    public sealed class AcademicToolsListsViewModel
    {
        private readonly ObservableCollection<AcademicToolsItem> _items = new ObservableCollection<AcademicToolsItem>();

        public AcademicToolsListsViewModel()
        {
            _items.Add(new AcademicToolsItem("空闲教室", "Assets/ToolIcons/Room Filled-64.png", 0xffa3bfd7, null));
            _items.Add(new AcademicToolsItem("我的成绩", "Assets/ToolIcons/Ratings Filled-64.png", 0xff77c9d1, null));
            _items.Add(new AcademicToolsItem("修读课程", "Assets/ToolIcons/Class Filled-64.png", 0xffd4a0cd, null));
            _items.Add(new AcademicToolsItem("其它课表", "Assets/ToolIcons/Week View Filled-64.png", 0xfff9c11b, null));
            _items.Add(new AcademicToolsItem("考试安排", "Assets/ToolIcons/Day View Filled-64.png", 0xffa3bfd7, null));
            _items.Add(new AcademicToolsItem("选课", "Assets/ToolIcons/Checklist Filled-64.png", 0xffff4c00, null));
            _items.Add(new AcademicToolsItem("校车时刻", "Assets/ToolIcons/Bus Filled-64.png", 0xfff9c11b, null));
            _items.Add(new AcademicToolsItem("校历", "Assets/ToolIcons/Calendar Filled-64.png", 0xffd4a0cd, null));
            _items.Add(new AcademicToolsItem("常用电话", "Assets/ToolIcons/Phone Filled-64.png", 0xff77c9d1, null));
        }

        public ObservableCollection<AcademicToolsItem> Items { get { return _items; } }

        private void NavigateTo(object item)
        {
            AcademicToolsItem clickedItem = item as AcademicToolsItem;
            if (clickedItem != null && clickedItem.SourcePage != null)
            {
                //NavigationService.Navigate(clickedItem.SourcePage);
            }
        }

        private bool CanNavigateTo(object item)
        {
            AcademicToolsItem clickedItem = item as AcademicToolsItem;
            if (clickedItem != null && clickedItem.SourcePage != null)
            {
                return true;
            }
            return false;
        }

        RelayCommand _navigateCommand;

        public RelayCommand NavigateCommand
        {
            get
            {
                return _navigateCommand ?? (_navigateCommand = new RelayCommand(
                    NavigateTo, CanNavigateTo));
            }
            set
            {
                _navigateCommand = value;
            }
        }
    }
}
