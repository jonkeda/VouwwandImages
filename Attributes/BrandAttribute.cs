using System;
using VouwwandImages.Database;

namespace VouwwandImages.Attributes;

public class BrandAttribute : Attribute
{
    public BrandAttribute(Brand brand)
    {
        Brand = brand;
    }

    public Brand Brand { get; set; }
}
