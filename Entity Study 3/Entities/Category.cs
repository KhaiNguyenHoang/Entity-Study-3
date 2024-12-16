using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Study_3.Entities
{
    [Table("Category")]
    public class Category
    {
        [Key]
        [Column("Category ID", TypeName = "int")]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("Category Name", TypeName = "nvarchar")]
        public string? Name { get; set; }

        [Required]
        [Column("Category Discription", TypeName = "ntext")]
        public string? Discription { get; set; }
    }
}