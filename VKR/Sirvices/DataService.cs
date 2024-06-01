using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Markup;
using VKR.Models.Oven;
using VKR.ViewModels;

namespace VKR.Sirvices
{
    internal class DataService
    {

        public string d;
        public List<Oven> array;
        public Byte[] getDefaultInput=new byte[6];
        
        async public void getData(SerialPort serialPort)
        {
        }
        public string getInformation(SerialPort serialPort)
        {
            try
            {
                serialPort.Read(getDefaultInput, 0, 6);
                d = Encoding.UTF8.GetString(getDefaultInput) ;
            }
            catch { d = " ошибка "; }
            return d;
        }
        public void setInformation (SerialPort serialPort,int i)
        {
            if (i < 10)
            {
                serialPort.Write("#01");
            }
            if ((i <= 20) && (i >= 10))
            {
                serialPort.Write("#01");
            }
            
        }
        public string getStroka()
        {
            return d;
        }
    }
}
