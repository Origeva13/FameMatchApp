using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FameMatchApp.Models
{
    public class Age
    {
        public List<int> Kinds1 { get; set; }
        public Age()
        {
            Kinds1 = new List<int>();
            for (int i = 10; i <= 120; i++)
            {
                Kinds1.Add(i);
            }
        }
    }
}
