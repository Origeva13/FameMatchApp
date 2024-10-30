using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FameMatchApp.Models
{
    public class Casted:User
    {
        public int UserAge { get; set; }


        public string UserLocation { get; set; } = null!;

        public int UserHigth { get; set; }


        public string UserHair { get; set; } = null!;


        public string UserEyes { get; set; } = null!;


        public string UserBody { get; set; } = null!;


        public string UserSkin { get; set; } = null!;


        public string AboutMe { get; set; } = null!;
        public Casted() { }
    }
}
