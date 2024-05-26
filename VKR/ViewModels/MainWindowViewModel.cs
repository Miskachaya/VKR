 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VKR.Infrastructure.Commands;
using VKR.ViewModels.Base;

namespace VKR.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Заголовок окна
        private string _Title="VKR";
        public string Title
        {
            get { return _Title; }
            set
            {
                Set(ref _Title, value);
            }
        }
        #endregion

        #region Команды

        #region CloseApplicationCommand
        public ICommand CloseApplicationCommand
        {
            get;
        }
        private void OnCloseApplicationCommandExecuted(object b)
        {
            Application.Current.Shutdown();
        }
        private bool CanCloseApplicationCommandExecuted(object b) => true; 
        #endregion


        #endregion
        public MainWindowViewModel() 
        {
            #region Команды
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecuted);
            #endregion
        }
    }
}
