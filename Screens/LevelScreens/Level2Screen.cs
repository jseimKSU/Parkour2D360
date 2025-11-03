using Microsoft.Xna.Framework;
using Parkour2D360.Collisions;

namespace Parkour2D360.Screens.LevelScreens
{
    public class Level2Screen : PlayableGameScreen
    {
        private Tilemap level;

        public Level2Screen()
        {
            Initialize();
        }

        public override void Activate()
        {
            base.Activate();
            level = ContentManager.Load<Tilemap>("Level2");
            RotatableGameScreenSide _first = new()
            {
                Platforms =
                [
                    (
                        new BoundingRectangle(
                            0,
                            Constants.SCREEN_HEIGHT - 100,
                            Constants.SCREEN_WIDTH,
                            10
                        ),
                        Color.White
                    ),
                ],
            };
            _gamescreenSides.Add(_first);
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
            base.Draw(gameTime);
            _spriteBatch.Begin();
            level.Draw(gameTime, _spriteBatch);
            DrawLevelPlatforms();
            _spriteBatch.End();
        }

        private void DrawLevelPlatforms()
        {
            foreach (
                (BoundingRectangle, Color) platform in _gamescreenSides[
                    _currentGameScreenSide
                ].Platforms
            )
            {
                _spriteBatch.Draw(
                    _platformTexture,
                    new Rectangle(
                        (int)platform.Item1.X,
                        (int)platform.Item1.Y,
                        (int)platform.Item1.Width,
                        (int)platform.Item1.Height
                    ),
                    platform.Item2
                );
            }
        }
    }
}
