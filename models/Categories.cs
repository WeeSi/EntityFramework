using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PostgreSQL.Data
{
    public class Categories : BaseModel
    {
        [Key]
        public int CategoryId { get; set; }
        [Required, MaxLength(40)]
        public string CategoryName { get; set; } = String.Empty;
    }
}
