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
using System.Windows.Shapes;

namespace JFlap_version5.Views
{
    /// <summary>
    /// Interaction logic for FiniteAutomationWindow.xaml
    /// </summary>
    public partial class FiniteAutomationWindow : Window
    {
        public FiniteAutomationWindow()
        {
            InitializeComponent();
            StateViewModel stateViewModel = new StateViewModel();
            DataContext = stateViewModel;
        }
    }
}
