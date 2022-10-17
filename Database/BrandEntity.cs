using System.ComponentModel.DataAnnotations;

namespace VouwwandImages.Database
{
    public class BrandEntity 
    {
        [Key] 
        public int Id { get; set; }

        public Brand Brand { get; set; }

        [StringLength(100)]
        public string? Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}