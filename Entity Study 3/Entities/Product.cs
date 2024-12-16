using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entity_Study_3.Entities;

[Table("Product")]
public class Product
{
    [Key]
    [Column("ProductID", TypeName = "int")]
    public int Id { get; set; }

    [Required] [MaxLength(50)] public string? Name { get; set; }

    [Required, Column("Price Product", TypeName = "money")]
    public decimal Price { get; set; }

    public Category Category { get; set; }
}