 using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VKR.Infrastructure.Commands;
using VKR.Models.Oven;
using VKR.Sirvices;
using VKR.ViewModels.Base;

namespace VKR.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        public DataService DatSer { get; }
        private string _state = "dd";
        public string state
        {
            get { return _state; }
            set
            {
                Set(ref _state, value);
            }
        }
        public SerialPort Port { get; }
        public ObservableCollection<Oven> Ovens { get; set; }
        
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
        #region Click
        public ICommand StartTask
        {
            get;
        }
        private void OnStartTask(object b)
        {
            loopAsync(Ovens);
            
        }
        private bool CanStartTask(object b) => true;
        #endregion

        #endregion
        #region Асинхронные методы
        async void loopAsync(ObservableCollection<Oven> o)
        {
            while (true)
            {
                for (int i =0; i <= o.Count; i++)
                {
                    DatSer.setInformation(Port,i);
                    await Task.Delay(100);
                    state = DatSer.getInformation(Port).Substring(3);
                    Ovens[i].Temperature=int.Parse(state);
                    state = Ovens[i].Temperature.ToString() + "dsafafqw";
                }
            }
        }
        #endregion
        public MainWindowViewModel() 
        {
            Port = new SerialPort(
                "COM4",
                9600
                );
            //Port.ReadTimeout = 100;
            Port.Open();
            #region Команды
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecuted);
            StartTask = new LambdaCommand(OnStartTask,CanStartTask);
            #endregion
            DatSer = new DataService();
            
            Ovens = new ObservableCollection<Oven>(Enumerable.Range(1,20).Select(i => new Oven
            {
                Id = i,
                Name = $"Name {i}",
                EndTime = DateTime.Now.AddMinutes(i),
                Temperature = i*10
            }));
        }
    }
}
