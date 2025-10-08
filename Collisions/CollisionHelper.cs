using Microsoft.Xna.Framework;
using System;

namespace Parkour2D360.Collisions
{
    public static class CollisionHelper
    {
        public const int COLLIDING_BUFFER = 5;
        public static bool ItemsCollide(BoundingRectangle a, BoundingRectangle b)
        {
            return !(
                a.Right < b.Left || // Right of A colliding
                a.Left > b.Right || // Left of A colliding
                a.Top > b.Bottom ||// Top of A colliding
                a.Bottom < b.Top // Bottom of A colliding
                );
            
        }

        public static Vector2 MoveEntityAOutOfEntityB(BoundingRectangle a, BoundingRectangle b)
        {
            float leftOverlap = a.Right - b.Left;
            float rightOverlap = b.Right - a.Left;
            float topOverlap = a.Bottom - b.Top;
            float bottomOverlap = b.Bottom - a.Top;

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
            if (
                CharacterIsTooHigh(character.Bottom, entity.Top) || 
                CharacterIsTooFarToTheRight(character.Left, entity.Right) || 
                CharacterIsTooFarToTheLeft(character.Right, entity.Left)
                ) return false;

            return true;
        }
        private static bool CharacterIsTooHigh(float bottomOfCharacter, float topOfEntity)
        {
            return ((topOfEntity > bottomOfCharacter) ? (topOfEntity - bottomOfCharacter) : COLLIDING_BUFFER + 1) > COLLIDING_BUFFER;
        }

        private static bool CharacterIsTooFarToTheLeft(float rightSideOfCharacter, float leftSideOfEntity)
        {
            return (leftSideOfEntity - rightSideOfCharacter) > COLLIDING_BUFFER;
        }

        private static bool CharacterIsTooFarToTheRight(float leftSideOfCharacter, float rightSideOfEntity)
        {
            return (leftSideOfCharacter - rightSideOfEntity) > COLLIDING_BUFFER;
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
