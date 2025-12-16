using Microsoft.Xna.Framework;
using Parkour2D360.Collisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkour2D360.Screens.LevelScreens
{
    public class Level3Screen : PlayableGameScreen
    {
        private const int BASE_PLATFORM_Y = Constants.SCREEN_HEIGHT - 100;
        private Platform BASE_PLATFORM = new Platform(
            new BoundingRectangle(0, BASE_PLATFORM_Y, Constants.SCREEN_WIDTH, 10),
            Color.Black,
            true
        );
        public Level3Screen()
        {
            Initialize();
            _levelName = "Level 3 - To be developed";
            _levelNumber = 3;
        }

        public override void Activate()
        {

            RotatableGameScreenSide _first = new()
            {
                CollidablePlatforms =
                [
                    BASE_PLATFORM,
                ],
                NonCollidablePlatforms = []
            };
            _gamescreenSides.Add(_first);


            base.Activate();
        }

        public override void Deactivate()
        {
            base.Deactivate();
        }

        public override void Unload()
        {
            base.Unload();
        }

        public override void Update(
            GameTime gameTime,
            bool otherScreenHasFocus,
            bool coveredByOtherScreen
        )
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            DrawLevelName();
            DrawLevelPlatforms();
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawLevelPlatforms()
        {
            foreach (Platform platform in _gamescreenSides[_currentGameScreenSide].Platforms)
            {
                _spriteBatch.Draw(
                    _platformTexture,
                    new Rectangle(
                        (int)platform.Location.X,
                        (int)platform.Location.Y,
                        (int)platform.Location.Width,
                        (int)platform.Location.Height
                    ),
                    platform.Color
                );
            }
        }
    }
}
