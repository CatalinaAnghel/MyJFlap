using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace JFlap_version5.Models
{
    class Arrow : INotifyPropertyChanged
    {
        private State state1;
        private State state2;
        private Line line;
        private TextBox label;

        public State State1
        {
            get
            {
                return state1;
            }
            set
            {
                state1 = value;
                OnPropertyChanged("State1");
            }
        }

        public State State2
        {
            get
            {
                return state2;
            }
            set
            {
                state2 = value;
                OnPropertyChanged("State2");
            }
        }

        public Line Line
        {
            get
            {
                return line;
            }
            set
            {
                line = value;
                OnPropertyChanged("Line");
            }
        }

        public TextBox Label
        {
            get
            {
                return label;
            }
            set
            {
                label = value;
                OnPropertyChanged("Label");
            }
        }

        #region INotifyPropertyChanged Members  

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
