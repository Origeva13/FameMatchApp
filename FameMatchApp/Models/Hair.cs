using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FameMatchApp.Models
{
    public class Hair
    {
        public List<string> Kinds3 { get; set; }
        public Hair()
        {
            Kinds3 = new List<string>();
            Kinds3.Add("Blonde");
            Kinds3.Add("Brunette");
            Kinds3.Add("Red");
            Kinds3.Add("Black");
            Kinds3.Add("Auburn");
            Kinds3.Add("Gray");

        }
    }
}
