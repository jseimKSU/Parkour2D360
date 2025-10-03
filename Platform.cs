using Microsoft.Xna.Framework;
using Parkour2D360.Collisions;

namespace Parkour2D360
{
    public struct Platform
    {
        public Platform(Rectangle platform)
        {
            PlatformLocation = platform;
            Hitbox = new BoundingRectangle(platform.X, platform.Y, platform.Width, platform.Height);
        }
        public Platform(int x, int y, int width, int height)
        {
            PlatformLocation = new Rectangle(x, y, width, height);
            Hitbox = new BoundingRectangle(x, y, width, height);
        }
        public Platform(Vector2 location, int width, int height)
        {
            PlatformLocation = new Rectangle((int)location.X, (int)location.Y, width, height);
            Hitbox = new BoundingRectangle(location.X, location.Y, width, height);
        }

        public Rectangle PlatformLocation { get; set;}

        public BoundingRectangle Hitbox { get; }
    }
}
