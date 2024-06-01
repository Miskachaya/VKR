using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace VKRCons
{
    internal class Program
    {
        private static Byte[] _string = { 35, 48, 48 };
        static void Main(string[] args)
        {
            SerialPort p = new SerialPort("COM4",9600);
            setInformation(p);
        }
        async public static void setInformation(SerialPort serialPort)
        {
            while (true)
            {
                serialPort.Write(_string, 0, _string.Length);
                await Task.Delay(1000);
                Console.WriteLine(serialPort.ReadByte().ToString());
            }
        }
    }
}
