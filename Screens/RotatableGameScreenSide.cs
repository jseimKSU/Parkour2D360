using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Parkour2D360.Collisions;

namespace Parkour2D360.Screens
{
    public class RotatableGameScreenSide
    {
        public List<Platform> Platforms => [.. CollidablePlatforms, .. NonCollidablePlatforms];

        public List<Platform> CollidablePlatforms { get; set; } = [];

        public List<Platform> NonCollidablePlatforms { get; set; } = [];

        public List<CollectableTriangle> Collectables { get; set; } = [];
    }
}
