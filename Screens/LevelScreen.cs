using Microsoft.Xna.Framework;
using Parkour2D360.Collisions;
using Parkour2D360.Screens.LevelScreens;
using Parkour2D360.StateManagment;

namespace Parkour2D360.Screens
{
    public class LevelScreen : PlayableGameScreen
    {
        protected const int BASE_PLATFORM_Y = Constants.SCREEN_HEIGHT - 100;
        protected Platform BASE_PLATFORM = new Platform(
            new BoundingRectangle(0, Constants.SCREEN_HEIGHT - 100, Constants.SCREEN_WIDTH, 10),
            Color.Black,
            true
        );

        protected bool levelCompleted
        {
            get
            {
                foreach (RotatableGameScreenSide side in _gamescreenSides)
                {
                    foreach (CollectableTriangle collectable in side.Collectables)
                    {
                        if (!collectable.isCollected)
                            return false;
                    }
                }
                return true;
            }
        }

        public LevelScreen()
        {
            Initialize();
        }

        public override void Activate()
        {
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

            UpdateCollectables(gameTime);

            foreach (
                CollectableTriangle collectable in _gamescreenSides[
                    _currentGameScreenSide
                ].Collectables
            )
            {
                if (
                    collectable.isCollideable
                    && CollisionHelper.ItemsCollide(_stickFigureSprite.Hitbox, collectable.Hitbox)
                )
                {
                    collectable.Collect();
                }
            }
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            base.HandleInput(gameTime, input);
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 offset = GetTransitionOffset();

            if (_isSideSliding)
            {
                Vector2 offsetFrom =
                    new Vector2(-_slideDirection * _slideProgress * Constants.SCREEN_WIDTH, 0f)
                    + offset;
                Vector2 offsetTo =
                    new Vector2(
                        _slideDirection * (1f - _slideProgress) * Constants.SCREEN_WIDTH,
                        0f
                    ) + offset;

                _spriteBatch.Begin();

                DrawLevelName();

                DrawLevelPlatforms(_slideFromIndex, offsetFrom);
                DrawLevelPlatforms(_slideToIndex, offsetTo);
                _spriteBatch.End();

                DrawCollectables();
            }
            else
            {
                _spriteBatch.Begin();

                DrawLevelName();

                DrawLevelPlatforms(_currentGameScreenSide, Vector2.Zero);
                _spriteBatch.End();

                DrawCollectables();
            }

            base.Draw(gameTime);
        }

        protected void DrawLevelPlatforms(int sideIndex, Vector2 offset)
        {
            DrawCollidableLevelPlatforms(sideIndex, offset);
            DrawNonCollidableLevelPlatforms(sideIndex, offset);
        }

        protected void DrawCollidableLevelPlatforms(int sideIndex, Vector2 offset)
        {
            if (_gamescreenSides?[sideIndex]?.CollidablePlatforms == null)
                return;

            foreach (Platform platform in _gamescreenSides[sideIndex].CollidablePlatforms)
            {
                _spriteBatch.Draw(
                    _platformTexture,
                    new Rectangle(
                        (int)(platform.Location.X + offset.X),
                        (int)(platform.Location.Y + offset.Y),
                        (int)platform.Location.Width,
                        (int)platform.Location.Height
                    ),
                    platform.Color
                );
            }
        }

        protected void DrawNonCollidableLevelPlatforms(int sideIndex, Vector2 offset)
        {
            if (_gamescreenSides?[sideIndex]?.NonCollidablePlatforms == null)
                return;
            Vector2 usedOffset = offset;
            if (_isSideSliding)
            {
                usedOffset = new Vector2(-offset.X, offset.Y);
            }
            foreach (Platform platform in _gamescreenSides[sideIndex].NonCollidablePlatforms)
            {
                _spriteBatch.Draw(
                    _platformTexture,
                    new Rectangle(
                        (int)(platform.Location.X + usedOffset.X),
                        (int)(platform.Location.Y + usedOffset.Y),
                        (int)platform.Location.Width,
                        (int)platform.Location.Height
                    ),
                    platform.Color
                );
            }
        }

        protected void UpdateLoadNextLevel(LevelScreen nextLevel)
        {
            if (levelCompleted)
            {
                LoadingScreen.Load(ScreenManager, true, ControllingPlayer, nextLevel);
            }
        }
    }
}
