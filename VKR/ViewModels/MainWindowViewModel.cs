 using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using VKR.Infrastructure.Commands;
using VKR.Models.Oven;
using VKR.Sirvices;
using VKR.ViewModels.Base;
using static System.Net.Mime.MediaTypeNames;

namespace VKR.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        public DataService DatSer { get; }

        public SerialPort Port { get; }
        public ObservableCollection<Oven> Ovens { get; set; }
        #region Select item
        private Oven _selectCB;
        public Oven selectCB
        {
            get { return _selectCB; }
            set
            {
                Set(ref _selectCB, value);
            }
        }
        #endregion
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
        #region Selected time
        private string _selectTB;
        public string selectTB
        {
            get { return _selectTB; }
            set
            {
                Set(ref _selectTB, value);
            }
        }
        #endregion
        #region colorDisplay
        private string _colorDisplay ;
        public string colorDisplay
        {
            get { return _colorDisplay; }
            set
            {
                Set(ref _colorDisplay, value);
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
            //Application.Current.Shutdown();
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
            //Ovens[0].command = "!0100000030";
            //Ovens[0].command = "#01";
            Task.Run(() =>
            {
                Ovens[0].command = "!0100000030";
                Console.WriteLine(Ovens[0].command);
                //Task.Delay(1050).Wait();
                Ovens[0].command = "#01";
            });
        }
        private bool CanStartTask(object b) => true;
        #endregion
        #region ClickPostCommand
        public ICommand PostCommand
        {
            get;
        }
        private void OnPostCommand(object b)
        {
            string time = selectTB;
            string h = time.Substring(0, 2);
            string m = time.Substring(3, 2);
            string s = time.Substring(6, 2);
            string t = time.Substring(9, 3);
            selectCB.MaxTemperature = int.Parse(t);
            int timeI = (int.Parse(h) * 3600) + (int.Parse(m) * 60) + (int.Parse(s));
            selectCB.state = true;
            selectCB.remainTime = timeI;
            Task.Run(() =>
            {
                selectCB.command = $"!{selectCB.Id.ToString("00")}{selectCB.MaxTemperature.ToString("000")}{timeI.ToString("00000")}";
                //Task.Delay(840).Wait();
            });
        }
        private bool CanPostCommand(object b) => true;
        #endregion
        #region Load
        public ICommand Load
        {
            get;
        }
        private void onLoad(object b)
        {
            loopAsync(Ovens);

        }
        private bool CanLoad(object b) => true;
        #endregion
        
        #endregion
        #region Асинхронные методы
        #region Опрос
        void loopAsync(ObservableCollection<Oven> o)
        {
          DatSer.getData(Port, o);
        }
        #endregion
        #region Загрузка
        void LoadAsync(ObservableCollection<Oven> o)
        {
            for (int i = 0; i < 20; i++)
            {
                Task.Run(() =>
                {
                    Ovens[i].command = $"?{o[i].Id.ToString("00")}";
                    
                    //FTask.Delay(840).Wait();

                    Ovens[i].command = $"#{o[i].Id.ToString("00")}";
                    //try
                    //{
                    //    string s = Port.ReadLine();
                    //}
                    //catch
                    //{
                    //    Console.WriteLine("ошибка");
                    //}
                    
                    
                    //if (request[1])
                });
            }
        }
        #endregion
        #endregion
        public MainWindowViewModel() 
        {
            Port = new SerialPort(
                "COM4",
                9600
                );
            Port.ReadTimeout =1;
            Port.Open();
            #region Команды
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecuted);
            StartTask = new LambdaCommand(OnStartTask,CanStartTask);
            PostCommand = new LambdaCommand(OnPostCommand, CanPostCommand);
            #endregion
            DatSer = new DataService();

            Ovens = new ObservableCollection<Oven>(Enumerable.Range(1, 20).Select(i => new Oven
            {
                Id = i,
                Name = $"Name {i}",
                Temperature = i * 10,
                command = $"?{i.ToString("00")}",
                MaxTemperature = 600

            })) ;
           
            loopAsync(Ovens);
            //LoadAsync(Ovens);


        }
    }
}
