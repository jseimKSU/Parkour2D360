using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkour2D360.Collisions
{
    public struct BoundingRectangle
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public float Width { get; private set; }
        public float Height { get; private set; }

        public float Left => X;
        public float Right => X + Width;
        public float Top => Y;
        public float Bottom => Y + Height;

        public BoundingRectangle(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public BoundingRectangle(Vector2 position, float width, float height)
        {
            X = position.X;
            Y = position.Y;
            Width = width;
            Height = height;
        }

        public bool CollidesWith (BoundingRectangle otherRectangle)
        {
            return CollisionHelper.ItemsCollide(this, otherRectangle);
        }
            
        public void ChangePositionTo(Vector2 position)
        {
            X = position.X;
            Y = position.Y;
        }

        public void PositionMove(Vector2 position)
        {
            X += position.X;
            Y += position.Y;
        }

        public void ChangeDimentions(float width, float height)
        {
            Width = width;
            Height = height;
        }
    }
}
