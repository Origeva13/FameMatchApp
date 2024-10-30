using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FameMatchApp.Models
{
    public class Reporet
    {
        public int? UserId { get; set; }

        public int ReporetId { get; set; }

        public int? ReportedId { get; set; }
        public string Content { get; set; } = null!;
        public Reporet() { }
        
    }
}
