using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FameMatchApp.Models
{
    public class Gender
    {
        public List<string> Kinds5 { get; set; }
        public Gender()
        {
            Kinds5 = new List<string>();
            Kinds5.Add("Male");
            Kinds5.Add("Female");
            Kinds5.Add("Other");
        }
    }
}
