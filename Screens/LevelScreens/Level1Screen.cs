using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Parkour2D360.Collisions;
using Parkour2D360.Sprites;
using Parkour2D360.StateManagment;

namespace Parkour2D360.Screens.LevelScreens
{
    public class Level1Screen : PlayableGameScreen
    {
        private const int BASE_PLATFORM_Y = Constants.SCREEN_HEIGHT - 100;
        private Platform BASE_PLATFORM = new Platform(
            new BoundingRectangle(0, Constants.SCREEN_HEIGHT - 100, Constants.SCREEN_WIDTH, 10),
            Color.Black,
            true
        );
        private Color COLLIDABLE_COLOR = Color.DarkRed;
        private Color NON_COLLIDABLE_COLOR = Color.PaleVioletRed;

        List<(Vector2, int, bool)> _collectables;

        public Level1Screen()
        {
            Initialize();
        }

        public override void Activate()
        {
            RotatableGameScreenSide _first = new()
            {
                Platforms =
                [
                    BASE_PLATFORM,
                    new Platform(
                        new BoundingRectangle(300, BASE_PLATFORM_Y - 140, 70, 140),
                        COLLIDABLE_COLOR,
                        true
                    ),
                    new Platform(
                        new BoundingRectangle(
                            Constants.SCREEN_WIDTH - 210,
                            BASE_PLATFORM_Y - 140,
                            210,
                            140
                        ),
                        NON_COLLIDABLE_COLOR,
                        false
                    ),
                ],
            };
            RotatableGameScreenSide _second = new()
            {
                Platforms =
                [
                    BASE_PLATFORM,
                    new Platform(
                        new BoundingRectangle(0, BASE_PLATFORM_Y - 140, 140, 140),
                        NON_COLLIDABLE_COLOR,
                        false
                    ),
                    new Platform(
                        new BoundingRectangle(1000, BASE_PLATFORM_Y - 140, 210, 140),
                        COLLIDABLE_COLOR,
                        true
                    ),
                ],
            };
            RotatableGameScreenSide _third = new()
            {
                Platforms =
                [
                    BASE_PLATFORM,
                    new Platform(
                        new BoundingRectangle(0, BASE_PLATFORM_Y - 140, 140, 140),
                        NON_COLLIDABLE_COLOR,
                        false
                    ),
                    new Platform(
                        new BoundingRectangle(
                            Constants.SCREEN_WIDTH - 370,
                            BASE_PLATFORM_Y - 140,
                            70,
                            140
                        ),
                        NON_COLLIDABLE_COLOR,
                        false
                    ),
                ],
            };
            RotatableGameScreenSide _fourth = new()
            {
                Platforms =
                [
                    new Platform(
                        new BoundingRectangle(0, Constants.SCREEN_HEIGHT - 100, 900, 10),
                        Color.Black,
                        true
                    ),
                    new Platform(
                        new BoundingRectangle(
                            1350,
                            Constants.SCREEN_HEIGHT - 100,
                            Constants.SCREEN_WIDTH - 1350,
                            10
                        ),
                        Color.Black,
                        true
                    ),
                    new Platform(
                        new BoundingRectangle(
                            Constants.SCREEN_WIDTH - 70,
                            BASE_PLATFORM_Y - 140,
                            70,
                            140
                        ),
                        NON_COLLIDABLE_COLOR,
                        false
                    ),
                ],
            };
            _gamescreenSides.Add(_first);
            _gamescreenSides.Add(_second);
            _gamescreenSides.Add(_third);
            _gamescreenSides.Add(_fourth);

            (Vector2, int, bool) collectableItem1_1 = (
                new Vector2(50, BASE_PLATFORM_Y - 100),
                1,
                false
            );
            (Vector2, int, bool) collectableItem1_2 = (
                new Vector2(Constants.SCREEN_WIDTH - 50, BASE_PLATFORM_Y - 100),
                1,
                true
            );
            (Vector2, int, bool) collectableItem2_1 = (
                new Vector2(50, BASE_PLATFORM_Y - 100),
                2,
                true
            );
            (Vector2, int, bool) collectableItem2_2 = (
                new Vector2(Constants.SCREEN_WIDTH - 50, BASE_PLATFORM_Y - 100),
                2,
                true
            );
            (Vector2, int, bool) collectableItem3_1 = (
                new Vector2(50, BASE_PLATFORM_Y - 100),
                3,
                true
            );
            (Vector2, int, bool) collectableItem3_2 = (
                new Vector2(Constants.SCREEN_WIDTH - 50, BASE_PLATFORM_Y - 100),
                3,
                true
            );
            (Vector2, int, bool) collectableItem4_1 = (
                new Vector2(50, BASE_PLATFORM_Y - 100),
                4,
                true
            );

            _collectables = new List<(Vector2, int, bool)>
            {
                collectableItem1_1,
                collectableItem1_2,
                collectableItem2_1,
                collectableItem2_2,
                collectableItem3_1,
                collectableItem3_2,
                collectableItem4_1,
            };

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

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            base.HandleInput(gameTime, input);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            DrawLevelPlatforms();
            DrawCollectables();
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawCollectables()
        {
            foreach ((Vector2, int, bool) collectable in _collectables)
            {
                if (collectable.Item2 - 1 == _currentGameScreenSide)
                {
                    _spriteBatch.Draw(
                        _tempTriangleTexture,
                        collectable.Item1,
                        (collectable.Item3) ? Color.DeepSkyBlue : Color.LightSteelBlue
                    );
                }
            }
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
