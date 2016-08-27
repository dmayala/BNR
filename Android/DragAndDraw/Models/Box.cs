using Android.Graphics;

namespace DragAndDraw.Models
{
    public class Box
    {
        public PointF Origin { get; private set; }
        public PointF Current { get; set; }

        public Box(PointF origin)
        {
            Origin = origin;
            Current = origin;
        }

    }
}