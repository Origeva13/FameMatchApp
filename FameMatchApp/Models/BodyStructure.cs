using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FameMatchApp.Models
{
    public class BodyStructure
    {
        public List<string> Kinds { get; set; }
        public BodyStructure()
        {
            Kinds = new List<string>();
            Kinds.Add("Ectomorph");
            Kinds.Add("Mesomorph");
            Kinds.Add("Endomorph");
            Kinds.Add("Apple-shaped");
            Kinds.Add("Pear-shaped");
            Kinds.Add("Inverted Triangle");
        }
    }
}
