using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestWPFADpp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        
        public static CancellationTokenSource _cancelTokenSource = new CancellationTokenSource();
        public CancellationToken token = _cancelTokenSource.Token;
        SerialPort port {  get; set; }
        Byte[] Data = { 35,48,48};
        string stroka { get; set; } = "ff";
        int i = 0;
        public MainWindow()
        {
            InitializeComponent();
            port = new SerialPort("COM4",9600);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            label1.Content = "d";
            if (port.IsOpen)
            {

            }
            else port.Open();
            doTask();
        }
        
        
        public void Read()
        {
            _cancelTokenSource.Cancel();
        }
        public void Write() { }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
        }
        public void doTask()
        {
            label1.Content = "de";
            request r = new request();
            Task t = new Task(() => {
                while (true)
                {
                    label1.Content = "da";
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }
                    else
                    {
                        r.Request(port);
                        string s=r.GetData(port);
                        label1.Content = s;
                    }
                }
            },token);

            t.Start();
        }
    }
}
