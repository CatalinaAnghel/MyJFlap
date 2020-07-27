using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFlap_version5.Models
{
    class MyTextBox : INotifyPropertyChanged
    {
        private int numberOfStates;
        public int NumberOfStates
        {
            get
            {
                return numberOfStates;
            }
            set
            {
                numberOfStates = value;
                OnPropertyChanged("NumberOfStates");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #region INotifyPropertyChanged Members  

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
