namespace VouwwandImages.Models.Products
{
    public class WindowTypes
    {
        static WindowTypes()
        {
            Draaiingen = new WindowCollection();

            Draaiingen.Add(new Window(500, 500, 
                SwingHorizontal.None, SwingVertical.None, 
                "None", "Geen"));

            Draaiingen.Add(new Window(500, 500,
                SwingHorizontal.Left, SwingVertical.None,
                "Left", "Links"));

            Draaiingen.Add(new Window(500, 500,
                SwingHorizontal.Right, SwingVertical.None,
                "Right", "Rechts"));

            Draaiingen.Add(new Window(500, 500,
                SwingHorizontal.None, SwingVertical.Up,
                "Up", "Boven"));

            Draaiingen.Add(new Window(500, 500,
                SwingHorizontal.None, SwingVertical.Down,
                "Down", "Beneden"));

            Draaiingen.Add(new Window(500, 500,
                SwingHorizontal.Left, SwingVertical.Up,
                "TiltTurnLeft", "Draaikiep links"));

            Draaiingen.Add(new Window(500, 500,
                SwingHorizontal.Right, SwingVertical.Up,
                "TiltTurnRight", "Draaikiep rechts"));

        }

        public static WindowCollection Draaiingen { get; private set; }
    }
}
