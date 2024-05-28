using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKR.Models.Oven
{
    internal class Oven
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime EndTime { get; set; }
        public int Temperature { get; set; }
       // public TimeSpan RemainingTime { get; set; }  
    }

}
