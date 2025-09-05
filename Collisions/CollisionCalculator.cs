using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkour2D360.Collisions
{
    public static class CollisionCalculator // may need to separate horizontal collision to vertical collision
    {
        public static bool ItemsCollide(BoundingRectangle rectangleA, BoundingRectangle rectangleB)
        {
            return !(
                rectangleA.Right < rectangleB.Left ||
                rectangleA.Left > rectangleB.Right ||
                rectangleA.Top > rectangleB.Bottom ||
                rectangleA.Bottom < rectangleB.Top
                );
        }
    }
}
