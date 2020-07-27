using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JFlap_version5.Commands
{
    // This is a general command. The real implementation will be made into the viewmodel
    public class Command : ICommand
    {
        // Define a delegate of tipe action
        readonly Action<object> executeMethod;
        readonly Predicate<object> canExecuteMethod;

        public Command(Action<object> executeMethod, Predicate<object> canExecuteMethod)
        {
            this.executeMethod = executeMethod;
            this.canExecuteMethod = canExecuteMethod;
        }


        public event EventHandler CanExecuteChanged
        {
            // va arunca un event in cazul in care pierde focus-ul
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }


        public void Execute(object parameter)
        {
            executeMethod.Invoke(parameter);
        }
    }
}
