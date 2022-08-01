using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VouwwandImages.Services
{
    public enum FoldDirection
    {
        In,
        Out
    }

    public enum SlideDirection
    {
        Left,
        Right,
        Both
    }

    public class DoorCollection : Collection<Door>
    {

    }

    public class Door
    {
        public int Top { get; set; }
        public int Left { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
    }

    public class ImageCreator
    {
        public void CreateImages()
        {
            CreateImage(2, FoldDirection.In, SlideDirection.Left, 2, 0);
        }

        public void CreateImage(int doors, FoldDirection foldDirection, SlideDirection slideDirection, int doorsLeft, int doorsRight)
        {

        }

        public void CreateDoors(int doors)
        {

        }

        public void CreateFolds(int doors)
        {

        }

        public void CreateArrow(int doors)
        {

        }


    }
}
