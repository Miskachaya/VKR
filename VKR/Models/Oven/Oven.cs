using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace VKR.Models.Oven 
{
    internal class Oven : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }

        }
        private string _Name;
        private DateTime _EndTime,_StartTime;
        private bool _State;
       
        public bool State 
        {
            get;set;
        } 
        public int Id 
        {
            get; set;
        }
        public string Name { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime StartTime { get; set; } = DateTime.Now;
        public TimeSpan RemainingTime { get; set; }

        private int _Temperature;

        public int Temperature
        {
            get { return _Temperature; }
            set
            {
                if (_Temperature != value)
                {
                    _Temperature = value;
                    OnPropertyChanged("Temperature");
                }
            }
        }
    }

}
