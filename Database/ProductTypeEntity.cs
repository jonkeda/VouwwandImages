using System.ComponentModel.DataAnnotations;

namespace VouwwandImages.Database;

public class ProductTypeEntity
{
    [Key]
    public int Id { get; set; }

    public ProductType Type { get; set; }

    [StringLength(100)]
    public string? Name { get; set; }

    public override string ToString()
    {
        return Name;
    }
}