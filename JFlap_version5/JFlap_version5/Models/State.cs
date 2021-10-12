using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// this is a test for the git config
namespace JFlap_version5.Models
{
    class State : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int id;
        private string content;
        private double x;
        private double y;
        private float width;
        private float height;
        private bool isDragable;

        public State()
        {
            isDragable = false;
        }

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                content = "S" + id;
                OnPropertyChanged("Id");
            }
        }

        public string Content
        {
            get
            {
                return content;
            }

        }

        public double X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
                OnPropertyChanged("X");
            }
        }
        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
                OnPropertyChanged("Y");
            }
        }
        public float Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
                OnPropertyChanged("Width");
            }
        }
        public float Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
                OnPropertyChanged("Height");
            }
        }

        public bool IsDragable
        {
            get
            {
                return isDragable;
            }
            set
            {
                isDragable = value;
                OnPropertyChanged("IsDragable");
            }
        }

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
