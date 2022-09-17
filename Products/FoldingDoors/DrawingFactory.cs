using System.Drawing;
using SkiaSharp;
using VouwwandImages.Images;
using VouwwandImages.Models.Products;
using VouwwandImages.Shapes;
using Bitmap = VouwwandImages.Shapes.Bitmap;
using Line = VouwwandImages.Shapes.Line;
using Rectangle = VouwwandImages.Shapes.Rectangle;

namespace VouwwandImages.Products.FoldingDoors
{
    public class DrawingFactory
    {
        public DrawingModelCollection CreateProducts()
        {
            DrawingModelCollection list = new DrawingModelCollection();
            DrawingModel drawing = new DrawingModel();
            list.Add(drawing);

            ShapeCollection shapes = drawing.Shapes;

            int numberOfDoors = 1;
            float width = 500 * numberOfDoors;
            float height = 500;

            CreateFoldingDoors(shapes, width, height, numberOfDoors, SwingHorizontal.None, SwingVertical.None);

            return list;
        }

        private void FoldingDoor5(ShapeCollection shapes)
        {
            int numberOfDoors = 5;
            float width = 100 * numberOfDoors;
            float height = 300;

            CreateFoldingDoors(shapes, width, height, numberOfDoors, SwingHorizontal.None, SwingVertical.None);
        }

        private void CreateFoldingDoors(ShapeCollection shapes, 
            float width, float height, 
            int numberOfDoors,
            SwingHorizontal swingHorizontal, SwingVertical swingVertical)
        {
            float left = 0;
            float top = 0;
            float spine = 10;

            SKPaint background = new SKPaint { Color = SKColors.White };

            float stroke1Width = spine;
            SKPaint background1 = new SKPaint { Color = SKColors.DarkGray };
            SKPaint stroke1 = new SKPaint { Color = SKColors.DarkGray, StrokeWidth = stroke1Width, Style = SKPaintStyle.Stroke };

            float stroke2Width = 4;
            SKPaint stroke2 = new SKPaint { Color = SKColors.Black, StrokeWidth = stroke2Width, Style = SKPaintStyle.Stroke };

/*            int stroke3Width = 4;
            SKPaint stroke3 = new SKPaint { Color = SKColors.Black, StrokeWidth = stroke2Width, Style = SKPaintStyle.Stroke };
*/
            shapes.Add(new Rectangle(left, top, width, height, background));
            // shapes.Add(new Rectangle(left, top, width, height, stroke1, stroke1Width));
            /*           shapes.Add(new Rectangle(left + stroke1Width, top + stroke1Width,
                           width - 2 * stroke1Width, height - 2 * stroke1Width, stroke3, stroke3Width));
            */
            shapes.Add(new Rectangle(left, top, width, height, stroke1, stroke1Width));

            float doorWidth = (width - spine * (numberOfDoors + 1)) / numberOfDoors;
            float spineLeft = left;
            for (float i = 0; i < numberOfDoors + 1; i++)
            {
                shapes.Add(new Rectangle(spineLeft, top, spine, height, background1));
                spineLeft += spine + doorWidth;
            }

            shapes.Add(new Rectangle(left, top, width, height, stroke2, stroke2Width));

            float doorLeft = left + spine;
            float doorTop = top + spine;
            float doorHeight = height - spine * 2;
            for (float i = 0; i < numberOfDoors; i++)
            {
                CreateDoor(shapes, doorLeft, doorTop, doorWidth, doorHeight, swingHorizontal, swingVertical);

                doorLeft += doorWidth + spine;
            }

        }

        private void CreateDoor(ShapeCollection shapes, float left, float top, float width, float height,
            SwingHorizontal swingHorizontal, SwingVertical swingVertical)
        {
            SKPaint background = new SKPaint { Color = SKColors.Blue };
            background.Shader = SKShader.CreateLinearGradient(
                new SKPoint(left, top),
                new SKPoint(left + width, top + height),
                new SKColor[] { SKColors.White, SKColors.LightBlue },
                new float[] { 0, 1 },
                SKShaderTileMode.Repeat);

            shapes.Add(new Rectangle(left, top, width, height, background));

            // shapes.Add(new Bitmap(left, top, width, height, null, ImageResource.LoadImage()));

            const float strokeWidth = 4;
            float[] dashArray = { 10, 2 * strokeWidth };
            SKPathEffect dashEffect = SKPathEffect.CreateDash(dashArray, 1);
            SKPaint swing = new SKPaint() {Color = SKColors.Black, StrokeWidth = strokeWidth, 
                Style = SKPaintStyle.Stroke, PathEffect = dashEffect};

            if (swingHorizontal == SwingHorizontal.Left)
            {
                shapes.Add(new Line(left, top, left + width, top + (height / 2), swing));
                shapes.Add(new Line(left, top + height, left + width, top + (height / 2), swing));
            }
            else if (swingHorizontal == SwingHorizontal.Right)
            {
                shapes.Add(new Line(left, top + (height / 2), left + width, top, swing));
                shapes.Add(new Line(left, top + (height / 2), left + width, top + height, swing));
            }

            if (swingVertical == SwingVertical.Up)
            {
                shapes.Add(new Line(left, top + height, left + (width / 2), top, swing));
                shapes.Add(new Line(left + width, top + height, left + (width / 2), top, swing));
            }
            else if (swingVertical == SwingVertical.Down)
            {
                shapes.Add(new Line(left, top, left + (width / 2), top + height, swing));
                shapes.Add(new Line(left + width, top, left + (width / 2), top + height, swing));
            }
        }

        public DrawingModel CreateDrawing(Window? window)
        {
            DrawingModel drawing = new DrawingModel();
            if (window == null)
            {
                return drawing;
            }

            ShapeCollection shapes = drawing.Shapes;

            int numberOfDoors = 1;
            float width = window.Width;
            float height = window.Height;

            CreateFoldingDoors(shapes, width, height, numberOfDoors, window.SwingHorizontal, window.SwingVertical);

            return drawing;
        }
    }
}
