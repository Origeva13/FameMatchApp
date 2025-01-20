using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FameMatchApp.Models
{
    public class Eyes
    {
        public List<string> Kinds2 { get; set; }
        public Eyes()
        {
            Kinds2 = new List<string>();
            Kinds2.Add("Blue");
            Kinds2.Add("Green");
            Kinds2.Add("Brown");
            
        }
    }
}