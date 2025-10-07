using Microsoft.Xna.Framework;
using System;

namespace Parkour2D360.Collisions
{
    public static class CollisionHelper
    {
        public const int COLLIDING_BUFFER = 3;
        public static bool ItemsCollide(BoundingRectangle a, BoundingRectangle b)
        {
            float differenceInYIf_A_IsAngled = GetYFromXPosition(new Vector2(a.X, a.Y), b.X, a.Angle);
            float differenceInYIf_B_IsAngled = GetYFromXPosition(new Vector2(b.X, b.Y), a.X, b.Angle);

            return !(
                a.Right < b.Left || // Right of A colliding
                a.Left > b.Right || // Left of A colliding
                a.Top + differenceInYIf_A_IsAngled 
                > 
                b.Bottom + differenceInYIf_B_IsAngled // Top of A colliding
                || 
                a.Bottom + differenceInYIf_B_IsAngled 
                < 
                b.Top + differenceInYIf_A_IsAngled // Bottom of A colliding
                );
            
        }

        public static Vector2 MoveEntityAOutOfEntityB(BoundingRectangle a, BoundingRectangle b)
        {
            float differenceInYIf_A_IsAngled = GetYFromXPosition(new Vector2(a.X, a.Y), b.X, a.Angle);
            float differenceInYIf_B_IsAngled = GetYFromXPosition(new Vector2(b.X, b.Y), a.X, b.Angle);

            float leftOverlap = a.Right - b.Left;
            float rightOverlap = b.Right - a.Left;
            float topOverlap = (a.Bottom + differenceInYIf_A_IsAngled) - (b.Top + differenceInYIf_B_IsAngled);
            float bottomOverlap = (b.Bottom + differenceInYIf_B_IsAngled) - (a.Top + differenceInYIf_A_IsAngled);

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

        public static bool IsCharacterStandingOnAnEntity(BoundingRectangle character, BoundingRectangle entity)
        {
            float hitboxDistance = Math.Abs((entity.Top + GetYFromXPosition(new Vector2(entity.X, entity.Y), character.X, entity.Angle)) - character.Bottom);

            if (hitboxDistance > COLLIDING_BUFFER) return false;

            return true;
        }

        /// <summary>
        /// Finds the difference between the Y point on the vertex and the Y position that correlates with a given X on a triangle
        /// </summary>
        /// <param name="platformStartingVertex">The vertex of the triangle</param>
        /// <param name="otherHitboxXPosition">The X point on the triangle</param>
        /// <param name="vertexAngle">The angle of the vertex</param>
        /// <returns>The difference between the Y point on the vertex and the Y position that correlates with a given X on a triangle, if angle = 0 returns 0</returns>
        public static float GetYFromXPosition(Vector2 platformStartingVertex, float otherHitboxXPosition, float vertexAngle)
        {
            return (float)((platformStartingVertex.X - otherHitboxXPosition) * Math.Tan(vertexAngle));
        }
    }
}
