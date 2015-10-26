using System;
using System.Windows.Input;

namespace Academic.Common
{
    /// <summary>
    /// 专门用于中继自身功能的命令
    /// 通过调用委托分配给其他对象。
    ///CanExecute 方法的默认返回值为“true”。
    /// 在下列情况中，始终需要调用 <see cref="RaiseCanExecuteChanged"/>
    /// <see cref="CanExecute"/> 应返回其他的值。
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        /// <summary>
        /// 调用 RaiseCanExecuteChanged 时引发。
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// 创建新命令。
        /// </summary>
        /// <param name="execute">执行逻辑。</param>
        /// <param name="canExecute">执行状态逻辑。</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// 确定此 <see cref="RelayCommand"/> 是否可在其当前状态下执行。
        /// </summary>
        /// <param name="parameter">
        /// 命令使用的数据。如果不需要向命令传递数据，则可将此对象设置为 null。
        /// </param>
        /// <returns>如果可执行此命令，则返回 true；否则返回 false。</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        /// <summary>
        /// 对当前命令目标执行 <see cref="RelayCommand"/>。
        /// </summary>
        /// <param name="parameter">
        /// 命令使用的数据。如果不需要向命令传递数据，则可将此对象设置为 null。
        /// </param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        /// <summary>
        /// 用于引发 <see cref="CanExecuteChanged"/> 事件的方法
        /// 执行 <see cref="CanExecute"/> 的返回值
        /// 方法已更改。
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            handler?.Invoke(this, EventArgs.Empty);
        }
    }
}
