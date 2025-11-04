using Microsoft.Xna.Framework;
using Parkour2D360.Collisions;

namespace Parkour2D360.Screens
{
    public class Platform
    {
        public BoundingRectangle Location { get; set; }
        public Color Color { get; set; }
        public bool IsCollidable { get; set; }

        public Platform(BoundingRectangle loc, Color color, bool isCollidable)
        {
            Location = loc;
            Color = color;
            IsCollidable = isCollidable;
        }
    }
}
