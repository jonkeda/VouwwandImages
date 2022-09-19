using System.Linq;

namespace VouwwandImages.Models.Products
{
    public class WindowTypes
    {

        private static Frames CreateSwing(Dimensions dimension)
        {
            float draaiingSize = 200;
            return new Frames(dimension, new []
            {
                new Window(draaiingSize, draaiingSize,
                    SwingHorizontal.None, SwingVertical.None,
                    "None", "Geen"),
                new Window(draaiingSize, draaiingSize,
                    SwingHorizontal.Left, SwingVertical.None,
                    "Left", "Links"),
                new Window(draaiingSize, draaiingSize,
                    SwingHorizontal.Right, SwingVertical.None,
                    "Right", "Rechts"),
                new Window(draaiingSize, draaiingSize,
                    SwingHorizontal.None, SwingVertical.Up,
                    "Up", "Boven"),
                new Window(draaiingSize, draaiingSize,
                    SwingHorizontal.None, SwingVertical.Down,
                    "Down", "Beneden"),
                new Window(draaiingSize, draaiingSize,
                    SwingHorizontal.Left, SwingVertical.Up,
                    "TiltTurnLeft", "Draaikiep links"),
                new Window(draaiingSize, draaiingSize,
                    SwingHorizontal.Right, SwingVertical.Up,
                    "TiltTurnRight", "Draaikiep rechts")
            })
            {
                Uid = $"Swing {dimension}",
                Name = "Draaingen",
            };
        }

        private static Frames CreateFoldingDoorNumberOfSashes(Dimensions dimension)
        {
            float vouwwandWidth = 100;
            float vouwwandHeight = 200;

            var foldingDoorNumberOfSashes = new Frames
            {
                Uid = "FoldingDoors",
                Name = $"Vouwwanden aantallen {dimension}",
            };

            for (int i = 2; i <= 6; i++)
            {
                var frame = new Frame($"{i}", $"{i} delige vouwwand") { Dimension = dimension };
                foldingDoorNumberOfSashes.FrameItems.Add(frame);
                for (int j = 0; j < i; j++)
                {
                    var sash = new Sash();
                    frame.Sashes.Add(sash);
                    sash.WindowCollection.Add(new Window(vouwwandWidth, vouwwandHeight));
                }
            }

            return foldingDoorNumberOfSashes;
        }

        private static Frames CreateFoldingDoorDistribution(Dimensions dimension, SwingDirection direction, string extrachar)
        {
            float vouwwandWidth = 100;
            float vouwwandHeight = 200;

            var foldingDoorDistribution = new Frames
            {
                Uid = "FoldingDoorsDistribution",
                Name = $"Vouwwanden verdeling {dimension}",
            };

            for (int i = 2; i <= 6; i++)
            {
                for (int k = 0; k <= i; k++)
                {
                    var frame = new Frame($"{i}{k}{i - k}{extrachar}", $"{i} {k} {i - k} delige vouwwand")
                    {
                        Dimension = dimension
                    };
                    foldingDoorDistribution.FrameItems.Add(frame);
                    for (int j = 0; j < k; j++)
                    {
                        var sash = new Sash();
                        frame.Sashes.Add(sash);
                        sash.WindowCollection.Add(new Window(vouwwandWidth, vouwwandHeight, FoldHorizontal.Left) { SwingDirection = direction }) ;
                    }

                    if (frame.Sashes.Count > 0)
                    {
                        frame.Sashes.Last().WindowCollection[0].FirstFold = true;
                    }
                    bool first = true;
                    for (int j = k; j < i; j++)
                    {
                        var sash = new Sash();
                        frame.Sashes.Add(sash);
                        sash.WindowCollection.Add(new Window(vouwwandWidth, vouwwandHeight, FoldHorizontal.Right)
                        {
                            SwingDirection = direction,
                            FirstFold = first
                        });
                        first = false;
                    }

                    if (i % 2 == 0)
                    {
                        if (k % 2 == 1)
                        {
                            SetSwingHorizontal(frame, k - 1, SwingHorizontal.Left);
                            SetSwingHorizontal(frame, k, SwingHorizontal.Right);
                        }
                        if (k == 1)
                        {
                            ClearFoldHorizontal(frame, 0);
                        }
                        if (i - k == 1)
                        {
                            ClearFoldHorizontal(frame, i - 1);
                        }
                    }
                    else
                    {
                        if (k % 2 == 0)
                        {
                            SetSwingHorizontal(frame, k, SwingHorizontal.Right);
                        }
                        else
                        {
                            SetSwingHorizontal(frame, k - 1, SwingHorizontal.Left);
                        }
                    }
                }
            }

            return foldingDoorDistribution;
        }


        private static Frames CreateFoldingDirection(Dimensions dimension)
        {
            float vouwwandWidth = 100;
            float vouwwandHeight = 200;

            var frames = new Frames
            {
                Uid = "FoldingDoorDirection",
                Name = $"Vouwwanden richting {dimension}",
            };

            var frame = new Frame($"Inside", $"Naar binnen")
            {
                Dimension = dimension
            };
            frames.FrameItems.Add(frame);
            var sash = new Sash();
            frame.Sashes.Add(sash);
            sash.WindowCollection.Add(new Window(vouwwandWidth, vouwwandHeight, SwingHorizontal.Left) { SwingDirection = SwingDirection.Inside });

            sash = new Sash();
            frame.Sashes.Add(sash);
            sash.WindowCollection.Add(new Window(vouwwandWidth, vouwwandHeight, SwingHorizontal.Right) { SwingDirection = SwingDirection.Inside });


            frame = new Frame($"Outside", $"Naar buiten")
            {
                Dimension = dimension
            };
            frames.FrameItems.Add(frame);
            sash = new Sash();
            frame.Sashes.Add(sash);
            sash.WindowCollection.Add(new Window(vouwwandWidth, vouwwandHeight, SwingHorizontal.Left) { SwingDirection = SwingDirection.Outside });
            
            sash = new Sash();
            frame.Sashes.Add(sash);
            sash.WindowCollection.Add(new Window(vouwwandWidth, vouwwandHeight, SwingHorizontal.Right) { SwingDirection = SwingDirection.Outside });


            return frames;
        }

        private static void ClearFoldHorizontal(Frame frame, int k)
        {
            if (k >= 0
                && k < frame.Sashes.Count)
            {

                var sash = frame.Sashes[k];
                foreach (Window window in sash.WindowCollection)
                {
                    window.FoldHorizontal = FoldHorizontal.None;
                }
            }

        }

        private static void SetSwingHorizontal(Frame frame, int k, SwingHorizontal swingHorizontal)
        {
            if (k >= 0
                && k < frame.Sashes.Count)
            {

                var sash = frame.Sashes[k];
                foreach (Window window in sash.WindowCollection)
                {
                    window.SwingHorizontal = swingHorizontal;
                }
            }
        }

        private static Frames CreateDoors(Dimensions dimension)
        {
            float doorWidth = 100;
            float doorHeight = 200;

            return new Frames(dimension, new[]
            {
                new Window(doorWidth, doorHeight,
                    SwingHorizontal.Left, SwingVertical.None, SwingDirection.Inside,
                    "Inside_Left", "Naar binnen links"),

                new Window(doorWidth, doorHeight,
                    SwingHorizontal.Right, SwingVertical.None, SwingDirection.Inside,
                    "Inside_Right", "Naar binnen rechts"),

                new Window(doorWidth, doorHeight,
                    SwingHorizontal.Left, SwingVertical.None, SwingDirection.Outside,
                    "Outside_Left", "Naar buiten links"),

                new Window(doorWidth, doorHeight,
                    SwingHorizontal.Right, SwingVertical.None, SwingDirection.Outside,
                    "Outside_Right", "Naar buiten rechts")

            })
            {
                Uid = $"Doors{dimension}",
                Name = $"Deuren{dimension}",
            };
        }



        public static FramesCollection GetFrame()
        {
            FramesCollection frames = new FramesCollection();

            frames.Add(CreateSwing(Dimensions.D2));
            frames.Add(CreateSwing(Dimensions.D3));

            frames.Add(CreateFoldingDoorNumberOfSashes(Dimensions.D2));
            frames.Add(CreateFoldingDoorNumberOfSashes(Dimensions.D3));

            frames.Add(CreateFoldingDoorDistribution(Dimensions.D2, SwingDirection.Outside, ""));
            frames.Add(CreateFoldingDoorDistribution(Dimensions.D3, SwingDirection.Outside, ""));

            frames.Add(CreateFoldingDoorDistribution(Dimensions.D3, SwingDirection.Inside, "Z"));
            frames.Add(CreateFoldingDoorDistribution(Dimensions.D3, SwingDirection.Outside, "W"));

            frames.Add(CreateDoors(Dimensions.D2));
            frames.Add(CreateDoors(Dimensions.D3));

            frames.Add(CreateFoldingDirection(Dimensions.D3));

            return frames;
        }
    }
}
