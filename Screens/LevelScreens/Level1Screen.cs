using Microsoft.Xna.Framework;
using Parkour2D360.Collisions;
using Parkour2D360.StateManagment;

namespace Parkour2D360.Screens.LevelScreens
{
    public class Level1Screen : LevelScreen
    {
        private float _currentCollectableTipMessageTime = 0;
        private const float MAX_COLLECTABLE_TIP_MESSAGE_TIME = 4;

        private float _currentRotateScreenTipMessageTime = 0;
        private const float MAX_ROTATE_SCREEN_TIP_MESSAGE_TIME = 5;
        private bool _hasRotatedScreen = false;
        private bool _ranIntoFirstBlock = false;

        public Level1Screen()
        {
            Initialize();
            _levelName = "Level 1";
            _levelNumber = 1;
        }

        public override void Activate()
        {
            #region collectables
            CollectableTriangle collectable1_1 = new CollectableTriangle(
                ScreenManager.Game,
                new Vector2(50, BASE_PLATFORM_Y - 100),
                1f,
                false
            );
            CollectableTriangle collectable1_2 = new CollectableTriangle(
                ScreenManager.Game,
                new Vector2(Constants.SCREEN_WIDTH - 50, BASE_PLATFORM_Y - 100),
                1f,
                true
            );
            CollectableTriangle collectable2_1 = new CollectableTriangle(
                ScreenManager.Game,
                new Vector2(50, BASE_PLATFORM_Y - 100),
                1f,
                true
            );
            CollectableTriangle collectable2_2 = new CollectableTriangle(
                ScreenManager.Game,
                new Vector2(Constants.SCREEN_WIDTH - 50, BASE_PLATFORM_Y - 100),
                1f,
                true
            );
            CollectableTriangle collectable3_1 = new CollectableTriangle(
                ScreenManager.Game,
                new Vector2(50, BASE_PLATFORM_Y - 100),
                1f,
                true
            );
            CollectableTriangle collectable3_2 = new CollectableTriangle(
                ScreenManager.Game,
                new Vector2(Constants.SCREEN_WIDTH - 50, BASE_PLATFORM_Y - 100),
                1f,
                true
            );
            CollectableTriangle collectable4_1 = new CollectableTriangle(
                ScreenManager.Game,
                new Vector2(50, BASE_PLATFORM_Y - 100),
                1f,
                true
            );

            collectable1_1.relatedCollectables = [collectable3_2, collectable4_1];
            collectable1_2.relatedCollectables = [collectable2_1];
            collectable2_1.relatedCollectables = [collectable1_2];
            collectable2_2.relatedCollectables = [collectable3_1];
            collectable3_1.relatedCollectables = [collectable2_2];
            collectable3_2.relatedCollectables = [collectable1_1, collectable4_1];
            collectable4_1.relatedCollectables = [collectable1_1, collectable3_2];
            #endregion


            RotatableGameScreenSide _first = new()
            {
                CollidablePlatforms =
                [
                    BASE_PLATFORM,
                    new Platform(
                        new BoundingRectangle(300, BASE_PLATFORM_Y - 140, 70, 140),
                        Constants.COLLIDABLE_COLOR,
                        true
                    ),
                ],
                NonCollidablePlatforms =
                [
                    new Platform(
                        new BoundingRectangle(
                            Constants.SCREEN_WIDTH - 210,
                            BASE_PLATFORM_Y - 140,
                            210,
                            140
                        ),
                        Constants.NON_COLLIDABLE_COLOR,
                        false
                    ),
                    new Platform(
                        new BoundingRectangle(
                            (Constants.SCREEN_WIDTH / 2) - 140,
                            BASE_PLATFORM_Y - 140,
                            140,
                            140
                        ),
                        Constants.NON_COLLIDABLE_COLOR,
                        false
                    ),
                ],
                Collectables = [collectable1_1, collectable1_2],
            };
            RotatableGameScreenSide _second = new()
            {
                CollidablePlatforms =
                [
                    BASE_PLATFORM,
                    new Platform(
                        new BoundingRectangle(1000, BASE_PLATFORM_Y - 140, 210, 140),
                        Constants.COLLIDABLE_COLOR,
                        true
                    ),
                ],
                NonCollidablePlatforms =
                [
                    new Platform(
                        new BoundingRectangle(0, BASE_PLATFORM_Y - 140, 140, 140),
                        Constants.NON_COLLIDABLE_COLOR,
                        false
                    ),
                    new Platform(
                        new BoundingRectangle(
                            Constants.SCREEN_WIDTH - 140,
                            BASE_PLATFORM_Y - 140,
                            140,
                            140
                        ),
                        Constants.NON_COLLIDABLE_COLOR,
                        false
                    ),
                ],
                Collectables = [collectable2_1, collectable2_2],
            };
            RotatableGameScreenSide _third = new()
            {
                CollidablePlatforms =
                [
                    BASE_PLATFORM,
                    new Platform(
                        new BoundingRectangle(
                            Constants.SCREEN_WIDTH / 2,
                            BASE_PLATFORM_Y - 140,
                            140,
                            140
                        ),
                        Constants.COLLIDABLE_COLOR,
                        true
                    ),
                ],
                NonCollidablePlatforms =
                [
                    new Platform(
                        new BoundingRectangle(0, BASE_PLATFORM_Y - 140, 140, 140),
                        Constants.NON_COLLIDABLE_COLOR,
                        false
                    ),
                    new Platform(
                        new BoundingRectangle(
                            Constants.SCREEN_WIDTH - 370,
                            BASE_PLATFORM_Y - 140,
                            70,
                            140
                        ),
                        Constants.NON_COLLIDABLE_COLOR,
                        false
                    ),
                ],
                Collectables = [collectable3_1, collectable3_2],
            };
            RotatableGameScreenSide _fourth = new()
            {
                CollidablePlatforms =
                [
                    new Platform(
                        new BoundingRectangle(0, BASE_PLATFORM_Y, 900, 10),
                        Color.Black,
                        true
                    ),
                    new Platform(
                        new BoundingRectangle(
                            1350,
                            BASE_PLATFORM_Y,
                            Constants.SCREEN_WIDTH - 1350,
                            10
                        ),
                        Color.Black,
                        true
                    ),
                ],
                NonCollidablePlatforms =
                [
                    new Platform(
                        new BoundingRectangle(
                            Constants.SCREEN_WIDTH - 70,
                            BASE_PLATFORM_Y - 140,
                            70,
                            140
                        ),
                        Constants.NON_COLLIDABLE_COLOR,
                        false
                    ),
                    new Platform(
                        new BoundingRectangle(0, BASE_PLATFORM_Y - 140, 140, 140),
                        Constants.NON_COLLIDABLE_COLOR,
                        false
                    ),
                ],
                Collectables = [collectable4_1],
            };
            _gamescreenSides.Add(_first);
            _gamescreenSides.Add(_second);
            _gamescreenSides.Add(_third);
            _gamescreenSides.Add(_fourth);

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

            if (!_hasRotatedScreen && _currentGameScreenSide != 0)
            {
                _hasRotatedScreen = true;
            }
            if (_stickFigureSprite.Hitbox.Right >= 295)
            {
                _ranIntoFirstBlock = true;
            }

            UpdateCollectableTipMessage(gameTime);
            UpdateRotateScreenTipMessage(gameTime);

            UpdateLoadNextLevel(new Level2Screen());
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            base.HandleInput(gameTime, input);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            _spriteBatch.Begin();
            DrawCollectableTipMessage();
            DrawRotateScreenTipMessage();
            _spriteBatch.End();
        }

        private void UpdateCollectableTipMessage(GameTime gameTime)
        {
            if (_currentCollectableTipMessageTime < MAX_COLLECTABLE_TIP_MESSAGE_TIME)
            {
                _currentCollectableTipMessageTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        private void UpdateRotateScreenTipMessage(GameTime gameTime)
        {
            if (
                _ranIntoFirstBlock
                && _currentRotateScreenTipMessageTime < MAX_ROTATE_SCREEN_TIP_MESSAGE_TIME
            )
            {
                _currentRotateScreenTipMessageTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        private void DrawCollectableTipMessage()
        {
            if (_currentCollectableTipMessageTime < MAX_COLLECTABLE_TIP_MESSAGE_TIME)
                _spriteBatch.DrawString(
                    ScreenManager.Font,
                    "Collect all triangular prism sides to advance to the next level!",
                    new Vector2(250, 100),
                    Constants.COLLIDABLE_COLLECTABLE_COLOR
                );
        }

        private void DrawRotateScreenTipMessage()
        {
            if (
                !_hasRotatedScreen
                && _ranIntoFirstBlock
                && _currentRotateScreenTipMessageTime < MAX_ROTATE_SCREEN_TIP_MESSAGE_TIME
            )
            {
                _spriteBatch.DrawString(
                    ScreenManager.Font,
                    "Use Left and Right Arrow keys or Left and Right on the D-Pad to rotate the screen!",
                    new Vector2(40, 300),
                    Color.DarkGreen
                );
            }
        }
    }
}
