using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Common.EntityModels
{
    [Index(nameof(UserMail)), Index(nameof(UserName), Name = "UserPseudo")]
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        [MaxLength(40)]
        public string UserName { get; set; } = String.Empty;
        [MaxLength(40)]
        public string UserMail { get; set; } = String.Empty;
        public ICollection<Blogs> Blogs { get; set; } = new List<Blogs>();
        public ICollection<Comments> Comments { get; set; } = new List<Comments>();
    }
}