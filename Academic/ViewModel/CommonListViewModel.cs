using System;
using Academic.UIComponent;

namespace Academic.ViewModel
{

    public class CommonListItemViewModel : BindableBase, VariableGridView.IResizable
    {
        private string _title;
        private string _detail;
        private string _titleFirst;

        public string TitleFirst
        {
            get { return _titleFirst; }
            set
            {
                if (_titleFirst != value)
                {
                    _titleFirst = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Detail
        {
            get { return _detail; }
            set
            {
                if (_detail != value)
                {
                    _detail = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Width { get; set; } = 1;

        public int Height { get; set; } = 1;
    }

}
