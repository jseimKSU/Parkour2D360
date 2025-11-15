using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Parkour2D360.Collisions;
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

        private bool levelCompleted 
        {
            get
            {
                foreach (RotatableGameScreenSide side in _gamescreenSides)
                {
                    foreach(CollectableTriangle collectable in side.Collectables)
                    {
                        if (!collectable.isCollected) return false;
                    }
                }
                return true;
            }
            }

        public Level1Screen()
        {
            Initialize();
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
                Platforms =
                [
                    BASE_PLATFORM,
                    new Platform(
                        new BoundingRectangle(300, BASE_PLATFORM_Y - 140, 70, 140),
                        Constants.COLLIDABLE_COLOR,
                        true
                    ),
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
                ],
                Collectables =
                [
                    collectable1_1,
                    collectable1_2,
                ],
            };
            RotatableGameScreenSide _second = new()
            {
                Platforms =
                [
                    BASE_PLATFORM,
                    new Platform(
                        new BoundingRectangle(0, BASE_PLATFORM_Y - 140, 140, 140),
                        Constants.NON_COLLIDABLE_COLOR,
                        false
                    ),
                    new Platform(
                        new BoundingRectangle(1000, BASE_PLATFORM_Y - 140, 210, 140),
                        Constants.COLLIDABLE_COLOR,
                        true
                    ),
                ],
                Collectables =
                [
                    collectable2_1,
                    collectable2_2,
                ],
            };
            RotatableGameScreenSide _third = new()
            {
                Platforms =
                [
                    BASE_PLATFORM,
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
                Collectables =
                [
                    collectable3_1,
                    collectable3_2,
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
                        Constants.NON_COLLIDABLE_COLOR,
                        false
                    ),
                ],
                Collectables =
                [
                    collectable4_1,
                ],
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
            UpdateCollectables(gameTime);
            foreach (
                CollectableTriangle collectable in _gamescreenSides[
                    _currentGameScreenSide
                ].Collectables
            )
            {
                if (
                    collectable.isCollideable && CollisionHelper.ItemsCollide(
                        _stickFigureSprite.Hitbox,
                        collectable.Hitbox
                    )
                )
                {
                    collectable.Collect();
                }
            }
            if (levelCompleted)
            {
                LoadingScreen.Load(ScreenManager, true, ControllingPlayer, new Level2Screen());
            }
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            base.HandleInput(gameTime, input);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            DrawLevelPlatforms();
            _spriteBatch.End();
            DrawCollectables();

            base.Draw(gameTime);
        }

        private void UpdateCollectables(GameTime gameTime)
        {
            foreach (
                CollectableTriangle collectable in _gamescreenSides[
                    _currentGameScreenSide
                ].Collectables
            )
            {
                collectable.Update(gameTime);
            }
        }

        private void DrawCollectables()
        {
            ScreenManager.GraphicsDevice.RasterizerState = RasterizerState.CullNone;
            ScreenManager.GraphicsDevice.DepthStencilState = DepthStencilState.None;


            foreach (
                CollectableTriangle collectable in _gamescreenSides[
                    _currentGameScreenSide
                ].Collectables
            )
            {
                if (!collectable.isCollected) collectable.Draw();
            }

            ScreenManager.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
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
