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

        public bool State
        { get; set; } = false;
        public int Id 
        {
            get; set;
        }
        public string Name {  get; set; }

        private int _remainTime;
        public int remainTime
        {
            get { return _remainTime; }
            set
            {
                if (_remainTime != value)
                {
                    _remainTime = value;
                    OnPropertyChanged("remainTime");
                }
            }
        }

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
        private int _MaxTemperature;
        public int MaxTemperature
        {
            get { return _MaxTemperature; }
            set
            {
                if (_MaxTemperature != value)
                {
                    _MaxTemperature = value;
                    OnPropertyChanged("MaxTemperature");
                }
            }
        }
        private string _command;
        public string command
        {
            get { return _command; }
            set
            {
                if (_command != value)
                {
                    _command = value;
                    OnPropertyChanged("command");
                }
            }
        }
        private bool _state = false;
        public bool state
        {
            get { return _state; }
            set
            {
                if (_state != value)
                {
                    _state = value;
                    OnPropertyChanged("state");
                }
            }
        }

        private string _outOnDisplay;
        public string outOnDisplay
        {
            get { return _outOnDisplay; }
            set
            {
                if (_outOnDisplay != value)
                {
                    _outOnDisplay = value;
                    OnPropertyChanged("outOnDisplay");
                }
            }
        }

        private string _color ;
        public string color
        {
            get { return _color; }
            set
            {
                if (_color != value)
                {
                    _color = value;
                    OnPropertyChanged("color");
                }
            }
        }
    }

}
