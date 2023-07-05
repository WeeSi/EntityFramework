using System.ComponentModel.DataAnnotations;

namespace EFCore.Common.EntityModels
{
    public class Tags : BaseModel
    {
        [Key]
        public int TagId { get; set; }
        [MaxLength(40)]
        public string TagName { get; set; } = String.Empty;
    }
}
