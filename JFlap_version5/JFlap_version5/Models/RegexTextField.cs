using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace JFlap_version5.Models
{
    public class RegexTextField : INotifyPropertyChanged
    {
        private string pattern;
        public string Pattern 
        {
            get
            {
                return pattern;
            }
            set
            {
                pattern = value;
                OnPropertyChanged("Pattern");
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
