using Microsoft.Xna.Framework;
using SharpDX.Direct3D9;
using System;

namespace Parkour2D360.Collisions
{
    public class BoundingRectangle
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public float Width { get; private set; }
        public float Height { get; private set; }
        /// <summary>
        /// The angle of the object in radians
        /// </summary>
        public float Angle { get; private set; }

        public float Left => X;
        public float Right => X + Width;
        public float Top => Y;
        public float Bottom => Y + Height;

        public BoundingRectangle(float x, float y, float width, float height, float angle = 0)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Angle = angle;
        }

        public BoundingRectangle(Vector2 position, float width, float height, float angle = 0)
        {
            X = position.X;
            Y = position.Y;
            Width = width;
            Height = height;
            Angle = angle;
        }
        /// <summary>
        /// Constructor meant to create a diagonal BoundingRectangle
        /// </summary>
        /// <param name="startPoint">The first point in the rectangle. Assumed to be the bottom of the diagonal</param>
        /// <param name="endPoint">The last point in the rectangle. Assumed to be the top of the diagonal</param>
        /// <param name="height">The thickness of the rectangle. Not how high it raises.</param>
        public BoundingRectangle(Vector2 startPoint, Vector2 endPoint, float height)
        {
            X = startPoint.X;
            Y = startPoint.Y;
            Width = (endPoint - startPoint).Length();
            Height = height;
            Angle = (float)Math.Atan((startPoint.Y - endPoint.Y) / (startPoint.X - endPoint.X));
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

        public Rectangle ToRectangle()
        {
            return new Rectangle((int)X, (int)Y, (int)Width, (int)Height);
        }
    }
}
