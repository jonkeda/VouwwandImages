using System.Windows;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using SkiaSharp.Views.WPF;
using VouwwandImages.Models.ProductDrawings;

namespace VouwwandImages.Views;

public class DrawingView : SKElement
{
/*    public static readonly DependencyProperty ProductsProperty = DependencyProperty.Register(
        nameof(Products), typeof(DrawingsViewModel), typeof(DrawingView), 
        new PropertyMetadata(default(DrawingsViewModel?), PropertyChangedCallback));

    public DrawingsViewModel? Products
    {
        get { return (DrawingsViewModel) GetValue(ProductsProperty); }
        set { SetValue(ProductsProperty, value); }
    }

    private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((DrawingView)d).InvalidateVisual();
    }*/

    public static readonly DependencyProperty DrawingProperty = DependencyProperty.Register(
        nameof(Drawing), typeof(DrawingModel), typeof(DrawingView), 
        new PropertyMetadata(default(DrawingModel?), PropertyChangedCallback));

    public DrawingModel? Drawing
    {
        get { return (DrawingModel?) GetValue(DrawingProperty); }
        set { SetValue(DrawingProperty, value); }
    }

    private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((DrawingView)d).InvalidateVisual();
    }

    protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
    {
        SKCanvas canvas = e.Surface.Canvas;
        
        canvas.Clear(SKColors.White);

        if (Drawing == null)
        {
            return;
        }

        Drawing.Draw(canvas);
    }
}