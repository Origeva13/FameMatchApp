using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FameMatchApp.Models
{
    public class Hight
    {
        public List<int> Kinds6 { get; set; }
        public Hight()
        {
            Kinds6 = new List<int>();
            for (int i = 10; i <= 300; i++)
            {
                Kinds6.Add(i);
            }
        }
    }
}
