using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FameMatchApp.Models
{
    public class Picture
    {
        public int UserId { get; set; }

        public int? FileId { get; set; }
        public Picture() { }
        

        //public virtual File? File { get; set; }

        //public virtual User User { get; set; } = null!; לשאול את עופר
    }
}
