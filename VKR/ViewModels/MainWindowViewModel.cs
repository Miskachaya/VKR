 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKR.ViewModels.Base;

namespace VKR.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private string _Title="VKR";
        public string Title
        {
            get { return _Title; }
            set
            {
                Set(ref _Title, value);
            }
        }
    }
}
