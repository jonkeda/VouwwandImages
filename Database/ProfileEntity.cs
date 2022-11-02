using System.ComponentModel.DataAnnotations;

namespace VouwwandImages.Database;

public class ProfileEntity
{
    [Key]
    public int Id { get; set; }

    public int BrandId { get; set; }
    public virtual BrandEntity Brand { get; set; }

    [StringLength(100)]
    public string? Code { get; set; }

    public Profile Profile { get; set; }
    
    [StringLength(100)]
    public string? Name { get; set; }

    public override string ToString()
    {
        return Name;
    }
}