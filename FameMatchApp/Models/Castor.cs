using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FameMatchApp.Models;

namespace FameMatchApp.Models
{
    public class Castor:User
    {

       
        public string CompanyName { get; set; } = null!;

        public int NumOfLisence { get; set; }

        public bool IsApproved { get; set; }
        public Castor() { }
       
        //public virtual ICollection<Audition> Auditions { get; set; } = new List<Audition>();


        //public virtual User User { get; set; } = null!; לשאול את עופר
    }
}
