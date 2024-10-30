using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FameMatchApp.Models
{
    public class Tip
    {
       
        public int TipId { get; set; }

        public int? UserId { get; set; }

        public int TipLevel { get; set; }

       
        public string Question { get; set; } = null!;

        public string Answer1 { get; set; } = null!;

     
        public string Answer2 { get; set; } = null!;

        
        public string Answer3 { get; set; } = null!;

        public string Answer4 { get; set; } = null!;

        public Tip() { }
        
    }
}
