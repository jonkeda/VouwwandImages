using System.ComponentModel.DataAnnotations;

namespace VouwwandImages.Database;

public class DistributionEntity
{
    [Key]
    public int Id { get; set; }

    public int ProductTypeId { get; set; }
    public virtual ProductTypeEntity ProductType { get; set; }

    [StringLength(100)]
    public string? Name { get; set; }

    public override string ToString()
    {
        return Name;
    }
}