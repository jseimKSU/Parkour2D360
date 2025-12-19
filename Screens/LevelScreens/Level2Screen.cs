using Microsoft.Xna.Framework;
using Parkour2D360.Collisions;

namespace Parkour2D360.Screens.LevelScreens
{
    public class Level2Screen : LevelScreen
    {
        public Level2Screen()
        {
            Initialize();
            _levelName = "Level 2";
            _levelNumber = 2;
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
                new Vector2(685 + 140, BASE_PLATFORM_Y - 100),
                1f,
                true
            );
            CollectableTriangle collectable1_3 = new CollectableTriangle(
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
                new Vector2(Constants.SCREEN_WIDTH - 685 - 140 + 70, BASE_PLATFORM_Y - 100),
                1f,
                true
            );
            CollectableTriangle collectable3_3 = new CollectableTriangle(
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

            collectable1_1.relatedCollectables = [collectable3_3, collectable4_1];
            collectable1_2.relatedCollectables = [];
            collectable1_3.relatedCollectables = [collectable2_1];
            collectable2_1.relatedCollectables = [collectable1_3];
            collectable2_2.relatedCollectables = [collectable3_1];
            collectable3_1.relatedCollectables = [collectable2_2];
            collectable3_2.relatedCollectables = [];
            collectable3_3.relatedCollectables = [collectable1_1, collectable4_1];
            collectable4_1.relatedCollectables = [collectable1_1, collectable3_3];
            #endregion

            RotatableGameScreenSide _first = new()
            {
                CollidablePlatforms =
                [
                    BASE_PLATFORM,
                    new Platform(
                        new BoundingRectangle(650, BASE_PLATFORM_Y - 140, 35, 140),
                        Constants.NON_JUMPABLE_COLLIDABLE_COLOR,
                        true
                    ),
                    new Platform(
                        new BoundingRectangle(965, BASE_PLATFORM_Y - 140, 35, 140),
                        Constants.NON_JUMPABLE_COLLIDABLE_COLOR,
                        true
                    ),
                ],
                NonCollidablePlatforms =
                [
                    new Platform(
                        new BoundingRectangle(210, BASE_PLATFORM_Y - 140, 35, 140),
                        Constants.NON_JUMPABLE_NON_COLLIDABLE_COLOR,
                        false
                    ),
                    new Platform(
                        new BoundingRectangle(685, BASE_PLATFORM_Y - 140, 280, 140),
                        Constants.NON_JUMPABLE_NON_COLLIDABLE_COLOR,
                        false
                    ),
                    new Platform(
                        new BoundingRectangle(
                            Constants.SCREEN_WIDTH - 140,
                            BASE_PLATFORM_Y - 140,
                            140,
                            140
                        ),
                        Constants.NON_JUMPABLE_NON_COLLIDABLE_COLOR,
                        false
                    ),
                ],
                Collectables = [collectable1_1, collectable1_2, collectable1_3],
            };
            RotatableGameScreenSide _second = new()
            {
                CollidablePlatforms =
                [
                    BASE_PLATFORM,
                    new Platform(
                        new BoundingRectangle(
                            Constants.SCREEN_WIDTH - 350,
                            BASE_PLATFORM_Y - 140,
                            70,
                            140
                        ),
                        Constants.NON_JUMPABLE_COLLIDABLE_COLOR,
                        true
                    ),
                ],
                NonCollidablePlatforms =
                [
                    new Platform(
                        new BoundingRectangle(0, BASE_PLATFORM_Y - 140, 140, 140),
                        Constants.NON_JUMPABLE_NON_COLLIDABLE_COLOR,
                        false
                    ),
                    new Platform(
                        new BoundingRectangle(
                            Constants.SCREEN_WIDTH - 280,
                            BASE_PLATFORM_Y - 140,
                            280,
                            140
                        ),
                        Constants.NON_JUMPABLE_NON_COLLIDABLE_COLOR,
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
                            Constants.SCREEN_WIDTH - 685 - 280,
                            BASE_PLATFORM_Y - 140,
                            140,
                            140
                        ),
                        Constants.NON_JUMPABLE_COLLIDABLE_COLOR,
                        true
                    ),
                    new Platform(
                        new BoundingRectangle(
                            Constants.SCREEN_WIDTH - 140 - 35,
                            BASE_PLATFORM_Y - 140,
                            35,
                            140
                        ),
                        Constants.NON_JUMPABLE_COLLIDABLE_COLOR,
                        true
                    ),
                ],
                NonCollidablePlatforms =
                [
                    new Platform(
                        new BoundingRectangle(0, BASE_PLATFORM_Y - 140, 140, 140),
                        Constants.NON_JUMPABLE_NON_COLLIDABLE_COLOR,
                        false
                    ),
                    new Platform(
                        new BoundingRectangle(
                            Constants.SCREEN_WIDTH - 685 - 280 - 35,
                            BASE_PLATFORM_Y - 140,
                            35,
                            140
                        ),
                        Constants.NON_JUMPABLE_NON_COLLIDABLE_COLOR,
                        false
                    ),
                    new Platform(
                        new BoundingRectangle(
                            Constants.SCREEN_WIDTH - 685 - 140,
                            BASE_PLATFORM_Y - 140,
                            140,
                            140
                        ),
                        Constants.NON_JUMPABLE_NON_COLLIDABLE_COLOR,
                        false
                    ),
                    new Platform(
                        new BoundingRectangle(
                            Constants.SCREEN_WIDTH - 685,
                            BASE_PLATFORM_Y - 140,
                            35,
                            140
                        ),
                        Constants.NON_JUMPABLE_NON_COLLIDABLE_COLOR,
                        false
                    ),
                ],
                Collectables = [collectable3_1, collectable3_2, collectable3_3],
            };
            RotatableGameScreenSide _fourth = new()
            {
                CollidablePlatforms =
                [
                    new Platform(
                        new BoundingRectangle(0, BASE_PLATFORM_Y, 140, 10),
                        Color.Black,
                        true
                    ),
                    new Platform(
                        new BoundingRectangle(
                            Constants.SCREEN_WIDTH - 140,
                            BASE_PLATFORM_Y,
                            140,
                            10
                        ),
                        Color.Black,
                        true
                    ),
                ],
                NonCollidablePlatforms =
                [
                    new Platform(
                        new BoundingRectangle(0, BASE_PLATFORM_Y - 140, 350, 140),
                        Constants.NON_JUMPABLE_NON_COLLIDABLE_COLOR,
                        false
                    ),
                    new Platform(
                        new BoundingRectangle(
                            Constants.SCREEN_WIDTH - 140,
                            BASE_PLATFORM_Y - 140,
                            140,
                            140
                        ),
                        Constants.NON_JUMPABLE_NON_COLLIDABLE_COLOR,
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

            UpdateLoadNextLevel(new Level3Screen());
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
