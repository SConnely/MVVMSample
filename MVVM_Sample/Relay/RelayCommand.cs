
namespace MVVM_Sample.Relay
{
    using System;
    using System.Diagnostics;
    using System.Windows.Input;

    public class RelayCommand : ICommand
    {
        private readonly Action<object> executeAction;
              
        public RelayCommand(Action<object> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            this.executeAction = action;
        }

        [DebuggerStepThrough]
        public bool CanExecute(object? parameter)
        {
            return this.executeAction != null;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object? parameter = null)
        {
            if (this.executeAction != null)
            {
                this.executeAction(parameter ?? new object());
            }
        }

    }
}
