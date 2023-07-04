using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PostgreSQL.Data
{
    [Index(nameof(UserMail)), Index(nameof(UserName), Name = "UserPseudo")]
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        [Required, MaxLength(40)]
        public string UserName { get; set; } = String.Empty;
        [Required, MaxLength(40)]
        public string UserMail { get; set; } = String.Empty;
        public ICollection<Blogs> Blogs { get; } = new List<Blogs>();
        public ICollection<Comments> Comments { get; } = new List<Comments>();
    }
}