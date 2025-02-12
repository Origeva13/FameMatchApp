using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FameMatchApp.Models
{
    public class Audition
    {
        public int? UserId { get; set; }


        public int AudId { get; set; }


        public string Description { get; set; } = null!;

        public int AudAge { get; set; }

        public string? AudName { get; set; }
        public string? AudLocation { get; set; }

        public int? AudHigth { get; set; }


        public string? AudHair { get; set; }


        public string? AudEyes { get; set; }


        public string? UserBody { get; set; }


        public string? AudSkin { get; set; }

        public string? AudGender { get; set; }

        public bool IsPublic { get; set; }
        public Audition() { }

    }
}
