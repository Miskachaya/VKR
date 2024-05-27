using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKRCons
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var cmd = new RelayCommand(o => { Console.WriteLine("Команда" + o.ToString()); });
            cmd.Execute("1");
        }
    }
}
