using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Common.EntityModels
{
    public class Categories : BaseModel
    {
        [Key]
        public int CategoryId { get; set; }
        [MaxLength(40)]
        public string CategoryName { get; set; } = String.Empty;
    }
}
