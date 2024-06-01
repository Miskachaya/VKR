using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VKRCons
{
    internal class Input
    {
        private byte[] Data= {35,48,48};
        
        public void Write(SerialPort port, int i)
        {
            port.Write(Data,0,Data.Length);
            Console.WriteLine("Вызов" + i.ToString());
            Task.Delay(10000).Wait();
        }
        public void Read(SerialPort port, int i)
        {
            string s = port.ReadByte().ToString();
            Console.WriteLine($"Результат {i} "+s) ;
        }
    }
}
