using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parkour2D360.Collisions
{
    public static class CollisionHelper
    {
        public const int COLLIDING_BUFFER = 1;
        public static bool ItemsCollide(BoundingRectangle rectangleA, BoundingRectangle rectangleB)
        {
            return !(
                rectangleA.Right < rectangleB.Left || // Right of A colliding
                rectangleA.Left > rectangleB.Right || // Left of A colliding
                rectangleA.Top > rectangleB.Bottom || // Top of A colliding
                rectangleA.Bottom < rectangleB.Top // Bottom of A colliding
                );
        }

        public static Vector2 MoveEntityAOutOfEntityB(BoundingRectangle A, BoundingRectangle B)
        {
            float leftOverlap = A.Right - B.Left;
            float rightOverlap = B.Right - A.Left;
            float topOverlap = A.Bottom - B.Top;
            float bottomOverlap = B.Bottom - A.Top;

            float min = float.MaxValue;
            Vector2 moveFromColliding = Vector2.Zero;

            if (leftOverlap > 0 && leftOverlap < min)
            {
                min = leftOverlap;
                moveFromColliding = new Vector2(-leftOverlap + -COLLIDING_BUFFER, 0);
            }
            if (rightOverlap > 0 && rightOverlap < min)
            {
                min = rightOverlap;
                moveFromColliding = new Vector2(rightOverlap + COLLIDING_BUFFER, 0);
            }
            if (topOverlap > 0 && topOverlap < min)
            {
                min = topOverlap;
                moveFromColliding = new Vector2(0, -topOverlap + -COLLIDING_BUFFER);
            }
            if (bottomOverlap > 0 && bottomOverlap < min)
            {
                min = bottomOverlap;
                moveFromColliding = new Vector2(0, bottomOverlap + COLLIDING_BUFFER);
            }

            return moveFromColliding;
        }
    }
}
