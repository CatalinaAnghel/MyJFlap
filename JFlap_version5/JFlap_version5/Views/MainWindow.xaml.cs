using JFlap_version5.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JFlap_version5.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();
            // Get an instance of the main window view model
            MainWindowViewModel vm = new MainWindowViewModel();
            // set the datacontext
            this.DataContext = vm;
            // the action to close the window
            if (vm.CloseAction == null)
                vm.CloseAction = new Action(this.Close);
        }
    }
}
