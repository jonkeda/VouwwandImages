using System.Threading.Channels;
using SkiaSharp;
using VouwwandImages.Images;
using VouwwandImages.Models.Products;
using VouwwandImages.Shapes;
using Line = VouwwandImages.Shapes.Line;
using Rectangle = VouwwandImages.Shapes.Rectangle;

namespace VouwwandImages.Factories
{
    public class DrawingsFactory
    {
        private void CreateFrame(ShapeCollection shapes,
            Frame frame)
        {
            var (width, height) = frame.GetMaximum();

            float left = 0;
            float top = 0;
            float spine = 10;
            
            float frameSize = 5;
            width += frameSize * 2;
            height += frameSize * 2;

            SKPaint background = new SKPaint { Color = SKColors.White };
            shapes.Add(new Rectangle(left, top, width, height, background));

            SKPaint framePaint = new SKPaint { Color = SKColors.Black, StrokeWidth = frameSize, Style = SKPaintStyle.Stroke };
            if (frame.Dimension == Dimensions.D3)
            {
                shapes.Add(new Bitmap(left, top, width, height, null, ImageResource.LoadImage()));
            }
            shapes.Add(new Rectangle(left, top, width, height, framePaint, frameSize));
            bool closed = true;
            float doorLeft = frameSize;
            foreach (Sash sash in frame.Sashes)
            {
                float doorTop = frameSize;
                foreach (Window window in sash.WindowCollection)
                {
                    CreateWindow(shapes, doorLeft, doorTop, window.Width, window.Height, spine, ref closed, window, frame.Dimension);
                    doorTop += window.Height;
                }

                var(sashWidth, _) = sash.GetMaximum();
                doorLeft += sashWidth;
            }
        }


        private void CreateWindow(ShapeCollection shapes, 
            float left, float top, float width, float height, float spine, ref bool closed,
            Window window, Dimensions dimension)
        {
            if (dimension == Dimensions.D2)
                CreateDoor2dDimension(shapes, left, top, width, height, spine, window);
            else
                CreateDoor3dDimension(shapes, left, top, width, height, spine, ref closed, window);
        }

        private static void CreateDoor3dDimension(ShapeCollection shapes, 
            float left, float top, float width, float height, float spine, ref bool closed,
            Window window)
        {
            left += spine / 2;
            top += spine / 2;
            width -= spine;
            height -= spine;
            
            SKPaint windowPaint = new SKPaint { Color = SKColors.DarkGray, StrokeWidth = spine, Style = SKPaintStyle.Stroke};
            SKPaint background = new SKPaint { Color = SKColors.Blue };
            background.Shader = SKShader.CreateLinearGradient(
                new SKPoint(left, top),
                new SKPoint(left + width * 1.25f, top + height* 1.25f),
                new SKColor[] { SKColors.White.WithAlpha(0x80), SKColors.LightBlue.WithAlpha(0x80) },
                new float[] { 0, 1 },
                SKShaderTileMode.Repeat);
            
            if (window.SwingHorizontal == SwingHorizontal.Left)
            {
                if (window.SwingDirection == SwingDirection.Outside)
                {
                    shapes.Add(new Path(
                        new SKPoint(left, top),
                        new SKPoint(left + width * 0.75f, top + height * 0.25f),
                        new SKPoint(left + width * 0.75f, top + height * 0.75f),
                        new SKPoint(left, top + height), windowPaint, background
                    ));
                }
                else
                {
                    shapes.Add(new Path(
                        new SKPoint(left, top),
                        new SKPoint(left + width * 0.75f, top - height * 0.25f),
                        new SKPoint(left + width * 0.75f, top + height * 1.25f),
                        new SKPoint(left, top + height), windowPaint, background
                    ));
                }
                closed = true;
            }
            else if (window.SwingHorizontal == SwingHorizontal.Right)
            {
                if (window.SwingDirection == SwingDirection.Outside)
                {
                    shapes.Add(new Path(
                        new SKPoint(left + width, top),
                        new SKPoint(left + width * 0.25f, top + height * 0.25f),
                        new SKPoint(left + width * 0.25f, top + height * 0.75f),
                        new SKPoint(left + width, top + height), windowPaint, background
                    ));
                }
                else
                {
                    shapes.Add(new Path(
                        new SKPoint(left + width, top),
                        new SKPoint(left + width * 0.25f, top - height * 0.25f),
                        new SKPoint(left + width * 0.25f, top + height * 1.25f),
                        new SKPoint(left + width, top + height), windowPaint, background
                    ));
                }
                closed = true;
            }
            else
            if (window.SwingVertical == SwingVertical.Up)
            {

            }
            else if (window.SwingVertical == SwingVertical.Down)
            {

            }
            else if (window.FoldHorizontal == FoldHorizontal.Left
                     || window.FoldHorizontal == FoldHorizontal.Right)
            {
                float height1;
                float height2;
                if (window.SwingDirection == SwingDirection.Outside)
                {
                    height1 = 0.25f;
                    height2 = 0.75f;
                }
                else
                {
                    height1 = -0.25f;
                    height2 = 1.25f;
                }

                float shoveLeft = 0;
                float shoveRight = 0;
                if (window.FirstFold)
                {
                    if (window.FoldHorizontal == FoldHorizontal.Right)
                    {
                        shoveLeft = width * 0.25f;
                    }
                    else
                    {
                        shoveRight = width * 0.25f;
                    }
                }

                if (closed)
                {
                    shapes.Add(new Path(
                        new SKPoint(left + shoveLeft, top),
                        new SKPoint(left + width - shoveRight, top + height * height1),
                        new SKPoint(left + width - shoveRight, top + height * height2),
                        new SKPoint(left + shoveLeft, top + height), windowPaint, background
                    ));
                }
                else
                {
                    shapes.Add(new Path(
                        new SKPoint(left + shoveLeft, top + height * height1),
                        new SKPoint(left + width - shoveRight, top),
                        new SKPoint(left + width - shoveRight, top + height ),
                        new SKPoint(left + shoveLeft, top + height * height2), windowPaint, background
                    ));
                }
                closed = !closed;
            }
            else
            {
                shapes.Add(new Path(
                    new SKPoint(left, top),
                    new SKPoint(left + width, top),
                    new SKPoint(left + width, top + height),
                    new SKPoint(left, top + height), windowPaint, background
                ));
            }

        }

        private static void CreateDoor2dDimension(ShapeCollection shapes, 
            float left, float top, float width, float height, float spine,
            Window window)
        {
            SKPaint windowPaint = new SKPaint { Color = SKColors.DarkGray, StrokeWidth = spine, Style = SKPaintStyle.Stroke };
            shapes.Add(new Rectangle(left, top, width, height, windowPaint, spine));

            left += spine;
            top += spine;
            width -= spine * 2;
            height -= spine * 2;

            SKPaint background = new SKPaint { Color = SKColors.Blue };
            background.Shader = SKShader.CreateLinearGradient(
                new SKPoint(left, top),
                new SKPoint(left + width, top + height),
                new SKColor[] { SKColors.White, SKColors.LightBlue },
                new float[] { 0, 1 },
                SKShaderTileMode.Repeat);

            shapes.Add(new Rectangle(left, top, width, height, background));

            const float strokeWidth = 4;
            float[] dashArray = {10, 2 * strokeWidth};
            SKPathEffect dashEffect = SKPathEffect.CreateDash(dashArray, 1);
            SKPaint swing = new SKPaint()
            {
                Color = SKColors.Black,
                StrokeWidth = strokeWidth,
                Style = SKPaintStyle.Stroke,
                PathEffect = dashEffect
            };

            if (window.SwingHorizontal == SwingHorizontal.Left)
            {
                shapes.Add(new Line(left, top, left + width, top + (height / 2), swing));
                shapes.Add(new Line(left, top + height, left + width, top + (height / 2), swing));
            }
            else if (window.SwingHorizontal == SwingHorizontal.Right)
            {
                shapes.Add(new Line(left, top + (height / 2), left + width, top, swing));
                shapes.Add(new Line(left, top + (height / 2), left + width, top + height, swing));
            }

            if (window.SwingVertical == SwingVertical.Up)
            {
                shapes.Add(new Line(left, top + height, left + (width / 2), top, swing));
                shapes.Add(new Line(left + width, top + height, left + (width / 2), top, swing));
            }
            else if (window.SwingVertical == SwingVertical.Down)
            {
                shapes.Add(new Line(left, top, left + (width / 2), top + height, swing));
                shapes.Add(new Line(left + width, top, left + (width / 2), top + height, swing));
            }

            SKPaint arrow = new SKPaint
            {
                Color = SKColors.Black, 
                StrokeWidth = 3
            };

            if (window.SlideHorizontal == SlideHorizontal.Left)
            {
                shapes.Add(new Arrow(left + width / 4, top + height / 2, width / 2, ArrowDirection.Left, arrow));
            }
            else if (window.SlideHorizontal == SlideHorizontal.Right)
            {
                shapes.Add(new Arrow(left + width / 4, top + height / 2, width / 2, ArrowDirection.Right, arrow));
            }

            if (window.SlideVertical == SlideVertical.Up)
            {
                shapes.Add(new Arrow(left + width / 2, top + height / 4, height / 2, ArrowDirection.Up, arrow));
            }
            else if (window.SlideVertical == SlideVertical.Down)
            {
                shapes.Add(new Arrow(left + width / 2, top + height / 4, height / 2, ArrowDirection.Down, arrow));
            }

            if (window.FoldHorizontal == FoldHorizontal.Left)
            {
                shapes.Add(new Arrow(left + width / 4, top + height / 2, width / 2, ArrowDirection.Left, arrow));
            }
            else if (window.FoldHorizontal == FoldHorizontal.Right)
            {
                shapes.Add(new Arrow(left + width / 4, top + height / 2, width / 2, ArrowDirection.Right, arrow));
            }

        }

        public DrawingModel CreateDrawing(Frame? frame)
        {
            DrawingModel drawing = new DrawingModel();
            if (frame == null)
            {
                return drawing;
            }

            ShapeCollection shapes = drawing.Shapes;

            CreateFrame(shapes, frame);

            return drawing;
        }

    }
}
