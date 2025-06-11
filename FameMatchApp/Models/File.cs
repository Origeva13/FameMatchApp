using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FameMatchApp.Services;

namespace FameMatchApp.Models
{
    public class File
    {
       
        public int FileId { get; set; }
        public int UserId { get; set; }

        public string FileExt { get; set; } = null!;
        public File() { }
       
        public string FileUrl
        {
            get
            {
                return FameMatchWebAPIProxy.ImageBaseAddress + this.FileId + this.FileExt;
            }
        }
        //public virtual ICollection<Picture> Pictures { get; set; } = new List<Picture>();
    }
}
