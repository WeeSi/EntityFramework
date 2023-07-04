using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PostgreSQL.Data
{
    [Index(nameof(CustomerMail)),Index(nameof(CustomerName), Name = "UserPseudo")]
    public class Customers: BaseModel
    {
        [Required, Key, MaxLength(5)]
        public int CustomerId { get; set; }
        [Required, MaxLength(40)]
        public string CustomerName { get; set; } = String.Empty;
        [Required, MaxLength(40)]
        public string CustomerMail { get; set; } = String.Empty;
        public ICollection<Blogs> Blogs { get; } = new List<Blogs>();
        public ICollection<Comments> Comments { get; } = new List<Comments>();
    }
}