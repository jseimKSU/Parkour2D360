using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Parkour2D360.Collisions;

namespace Parkour2D360.Screens
{
    public class RotatableGameScreenSide
    {
        public List<(BoundingRectangle, Color)> Platforms { get; set; }
    }
}
