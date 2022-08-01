using System.IO;
using System.Windows;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using SkiaSharp.Views.WPF;
using VouwwandImages.Models;
using VouwwandImages.ViewModels;

namespace VouwwandImages.Views;

public class ProductsView : SKElement
{
    public static readonly DependencyProperty ProductsProperty = DependencyProperty.Register(
        nameof(Products), typeof(ProductsViewModel), typeof(ProductsView), 
        new PropertyMetadata(default(ProductsViewModel?), PropertyChangedCallback));

    public ProductsViewModel? Products
    {
        get { return (ProductsViewModel) GetValue(ProductsProperty); }
        set { SetValue(ProductsProperty, value); }
    }

    private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((ProductsView)d).InvalidateVisual();
    }

    public void SaveImages()
    {
        int width = 100;
        int height = 100;
        SKBitmap bitmap = new SKBitmap(width, height);
        SKCanvas canvas = new SKCanvas(bitmap);

        //Draw on canvas from stored commands DrawPath, etc.
        //Get the file to store to. Then save the bitmap to file.

        using Stream s = File.Create("");
        SKData d = SKImage.FromBitmap(bitmap).Encode(SKEncodedImageFormat.Png, 100);
        d.SaveTo(s);
    }

    protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
    {
        SKCanvas canvas = e.Surface.Canvas;
        
        canvas.Clear(SKColors.White);

        if (Products == null)
        {
            return;
        }

        foreach (ProductModel product in Products.Products)
        {
            product.Draw(canvas);
        }
    }
}