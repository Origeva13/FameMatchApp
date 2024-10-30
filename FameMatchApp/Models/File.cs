using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FameMatchApp.Models
{
    public class File
    {
       
        public int FileId { get; set; }

        
        public string FileExt { get; set; } = null!;
        public File() { }
       

        //public virtual ICollection<Picture> Pictures { get; set; } = new List<Picture>();
    }
}
