using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace Academic.UIComponent
{
    public class VariableGridView : GridView
    {
        public interface IResizable
        {
            int Width { get; set; }
            int Height { get; set; }
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            var viewModel = item as IResizable;

            if (viewModel == null)
            {
                throw new InvalidCastException("Item for VariableGridView must be an IResizable!");
            }

            DoBinding(element, VariableSizedWrapGrid.ColumnSpanProperty, viewModel, "Width");
            DoBinding(element, VariableSizedWrapGrid.RowSpanProperty, viewModel, "Height");

            base.PrepareContainerForItemOverride(element, item);
        }

        private static void DoBinding(DependencyObject ele, DependencyProperty property, object vm, string path)
        {
            var myBinding = new Binding
            {
                Source = vm,
                Path = new PropertyPath(path),
                Mode = BindingMode.OneWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            BindingOperations.SetBinding(ele, property, myBinding);
        }
    }
}
