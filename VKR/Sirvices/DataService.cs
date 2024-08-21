using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Windows.Media;
using VKR.Models.Oven;
using VKR.ViewModels;

namespace VKR.Sirvices
{
    internal class DataService
    {
        public bool b = true;
        public string d;
        public List<Oven> array;
        public Byte[] getDefaultInput=new byte[11];
        public int[] temp = new int[20];
        public int[] remainTime = new int[20];
         async public void getData(SerialPort serialPort, ObservableCollection<Oven> O)
        {
            while (true)
            {
                for (int i = 0; i < O.Count; i++)
                {
                    if ((O[i].state == true) || (O[i].command == $"?{O[i].Id.ToString("00")}"))
                    {
                        
                        setInformation(serialPort, O[i]);
                        await Task.Delay(18);
                        string s = getInformation(serialPort);
                        serialPort.DiscardInBuffer();
                        if (s == "ошибка") { O[i].color = "Gray"; }
                        else { O[i].color = "Yellow"; }
                        string id = (O[i].Id).ToString("00");
                        string state = (O[i].State).ToString();
                        switch (s[0])
                        {
                            case '!':
                                O[i].command = $"#{id}";
                                break
                                    ;
                            case '#':
                                string st = s.Substring(3);
                                Console.WriteLine(st);
                                temp[i] = int.Parse(st.Substring(0, 3));
                                remainTime[i] = int.Parse(s.Substring(6));
                                if (remainTime[i] == 0) O[i].state = false;
                                Console.WriteLine(O[i].MaxTemperature+ " " + O[i].Temperature);
                                if (O[i].MaxTemperature < O[i].Temperature) { O[i].color ="Red";}
                                else O[i].color ="Green";
                                
                                // для появления данных для каждой печи по отдельности
                                O[i].Temperature = temp[i];
                                O[i].remainTime = remainTime[i];
                                O[i].outOnDisplay = TimeSpan.FromSeconds((double)remainTime[i]).ToString();
                                break;
                            case '?':
                                
                                break;
                        }
                        

                    }
                    else
                    {
                        O[i].color = "#FFFFFF00";
                        O[i].outOnDisplay = "";
                        //await Task.Delay(20);
                    }
                }
                //для появления всех данных раз в секнду
                //for (int i = 0; i < temp.Length; i++)
                //{
                //    O[i].Temperature = temp[i];
                //    O[i].remainTime = remainTime[i];
                //    O[i].outOnDisplay = TimeSpan.FromSeconds((double)remainTime[i]).ToString();
                //}
            }
            
            
        }
        public void getRequest(Oven O,int i)
        {

            
        }
        public void getCommand()
        {

        }
        public void getState()
        {

        }
        public string getInformation(SerialPort serialPort)
        {
            try
            {
                serialPort.Read(getDefaultInput, 0, 11);
                d = Encoding.ASCII.GetString(getDefaultInput) ;
            }
            catch { d = "ошибка"; }
            return d;

        }
        public void setInformation (SerialPort serialPort,Oven o)
        {
            serialPort.Write(o.command);
            
        }
        public void setCommand(SerialPort serialPort, int i)
        {
            #region старый способ
            //if (i < 10)
            //{
            //    serialPort.Write("!0100000040");
            //}
            //if ((i <= 20) && (i >= 10))
            //{
            //    serialPort.Write("!0100000020");
            #endregion
        }
    }
}
