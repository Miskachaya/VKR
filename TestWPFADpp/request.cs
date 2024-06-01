using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Markup;

namespace TestWPFADpp
{
    internal class request
    {
        string s = "";
        Byte[] data = { 35, 48, 48 };
        public  static CancellationTokenSource _cancelTokenSource = new CancellationTokenSource();
        public CancellationToken token = _cancelTokenSource.Token;
        public void Request( SerialPort _port)
        {
            _port.Write(data, 0, data.Length);
            Thread.Sleep(1000);
        }
        public string GetData(SerialPort _port)
        {
            return _port.ReadByte().ToString();
        }
    }
}
