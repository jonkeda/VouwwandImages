using System.Drawing;
using SkiaSharp;
using VouwwandImages.Models;
using VouwwandImages.Shapes;
using Rectangle = VouwwandImages.Shapes.Rectangle;

namespace VouwwandImages.Products.FoldingDoors
{
    public enum DoorSwingHorizontal
    {
        None,
        Left,
        Right,
    }

    public enum DoorSwingVertical
    {
        None,
        Up,
        Down,
    }

    public class VouwwandFactory
    {
        public ProductModelCollection CreateProducts()
        {
            ProductModelCollection list = new ProductModelCollection();
            ProductModel product = new ProductModel();
            list.Add(product);

            ShapeCollection shapes = product.Shapes;

            int numberOfDoors = 5;
            float width = 100 * numberOfDoors;
            float height = 300;

            CreateFoldingDoors(shapes, width, height, numberOfDoors);
            // CreateDoors(shapes, width, height, numberOfDoors);

            return list;
        }

        private void CreateFoldingDoors(ShapeCollection shapes, float width, float height, int numberOfDoors)
        {
            float left = 100;
            float top = 100;
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
                CreateDoor(shapes, doorLeft, doorTop, doorWidth, doorHeight);

                doorLeft += doorWidth + spine;
            }

        }

        private void CreateDoor(ShapeCollection shapes, float left, float top, float width, float height)
        {
            SKPaint background = new SKPaint { Color = SKColors.Blue };
            background.Shader = SKShader.CreateLinearGradient(
                new SKPoint(left, top),
                new SKPoint(left + width, top + height),
                new SKColor[] { SKColors.White, SKColors.LightBlue },
                new float[] { 0, 1 },
                SKShaderTileMode.Repeat);

            shapes.Add(new Rectangle(left, top, width, height, background));

            const float strokeWidth = 2;
            float[] dashArray = { 0, 2 * strokeWidth };
            SKPathEffect dashEffect = SKPathEffect.CreateDash(dashArray, 1);
            SKPaint swing = new SKPaint() {Color = SKColors.LightGray, PathEffect = dashEffect};

            shapes.Add(new Line(left, top, left + width, top + (height / 2), background));

        }

    }
}
