using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductManager.Domain;


[Index(nameof(Sku), IsUnique = true)]
public class Product
{
  public int Id { get; set; }

  [MaxLength(50)]
  public required string Name { get; set; }

  [MaxLength(12)]
  public required string Sku { get; set; }

  [MaxLength(500)]
  public required string Description { get; set; }

  [MaxLength(100)]
  public required string Image { get; set; }

  [MaxLength(20)]
  public required string Price { get; set; }

}