using System;
using System.Collections.ObjectModel;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Academic.Common;
using Academic.UIComponent;

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
            return Color.FromArgb((byte) ((value >> 24) & 0xFF),
                (byte) ((value >> 16) & 0xFF),
                (byte) ((value >> 8) & 0xFF),
                (byte) (value & 0xFF));
        }
    }

    /// <summary>
    ///  教务功能列表定义
    /// </summary>
    public sealed class AcademicToolsListsViewModel
    {
        public AcademicToolsListsViewModel()
        {
            Items.Add(new AcademicToolsItem("空闲教室", "Assets/ToolIcons/Room Filled-64.png", 0xffa3bfd7, typeof(BlankPage1)));
            Items.Add(new AcademicToolsItem("我的成绩", "Assets/ToolIcons/Ratings Filled-64.png", 0xff77c9d1, typeof(BlankPage1)));
            Items.Add(new AcademicToolsItem("修读课程", "Assets/ToolIcons/Class Filled-64.png", 0xffd4a0cd, typeof(BlankPage1)));
            Items.Add(new AcademicToolsItem("其它课表", "Assets/ToolIcons/Week View Filled-64.png", 0xfff9c11b, typeof(BlankPage1)));
            Items.Add(new AcademicToolsItem("考试安排", "Assets/ToolIcons/Day View Filled-64.png", 0xff77c9d1, typeof(BlankPage1)));
            Items.Add(new AcademicToolsItem("选课", "Assets/ToolIcons/Checklist Filled-64.png", 0xffff4c00, typeof(BlankPage1)));
            Items.Add(new AcademicToolsItem("校车时刻", "Assets/ToolIcons/Bus Filled-64.png", 0xfff9c11b, typeof(BlankPage1)));
            Items.Add(new AcademicToolsItem("校历", "Assets/ToolIcons/Calendar Filled-64.png", 0xffd4a0cd, typeof(BlankPage1)));
            Items.Add(new AcademicToolsItem("常用电话", "Assets/ToolIcons/Phone Filled-64.png", 0xffa3bfd7, typeof(BlankPage1)));
        }

        public ObservableCollection<AcademicToolsItem> Items { get; } = new ObservableCollection<AcademicToolsItem>();

        private void NavigateTo(object item)
        {
            var clickedItem = item as AcademicToolsItem;
            if (clickedItem?.SourcePage != null)
            {
                GlobalNavigationManager.Instance.ChildNavigate(clickedItem.SourcePage);
            }
        }

        private bool CanNavigateTo(object item)
        {
            var clickedItem = item as AcademicToolsItem;
            return clickedItem?.SourcePage != null;
        }

        private RelayCommand _navigateCommand;

        public RelayCommand NavigateCommand
        {
            get
            {
                return _navigateCommand ?? (_navigateCommand = new RelayCommand(
                    NavigateTo, CanNavigateTo));
            }
            set { _navigateCommand = value; }
        }
    }
}
