using JFlap_version5.Commands;
using JFlap_version5.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFlap_version5.ViewModels
{
    class MainWindowViewModel
    {
        // The action used to close the main window
        public Action CloseAction { get; set; }

        public Command FiniteAutomationCommand { get; private set; }
        public Command RegularExpressionCommand { get; private set; }

        public MainWindowViewModel()
        {
            FiniteAutomationCommand = new Command(OpenFiniteAutomationWindow, canOpenWindow);
            RegularExpressionCommand = new Command(OpenRegularExpressionWindow, canOpenWindow);

        }

        private bool canOpenWindow(object parameter)
        {
            return true;
        }

        private void OpenRegularExpressionWindow(object parameter)
        {
            RegularExpressionWindow reWindow = new RegularExpressionWindow();

            // Show the new window
            reWindow.Show();

            // Close the old window
            CloseAction();
        }

        private void OpenFiniteAutomationWindow(object parameter)
        {
            FiniteAutomationWindow faWindow = new FiniteAutomationWindow();

            // Show the new window
            faWindow.Show();

            // Close the old window
            CloseAction();
        }

    }
}
