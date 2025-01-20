using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FameMatchApp.Models
{
    public class Skin
    {
        public List<string> Kinds4 { get; set; }
        public Skin()
        {
            Kinds4 = new List<string>();
            Kinds4.Add("Tan");
            Kinds4.Add("Fair");
            Kinds4.Add("Light");
            Kinds4.Add("Olive");
            Kinds4.Add("Dark");
        }
    }
}
