using Microsoft.Xna.Framework;
using Parkour2D360.Collisions;

namespace Parkour2D360.Screens.LevelScreens
{
    public class Level3Screen : LevelScreen
    {
        public Level3Screen()
        {
            Initialize();
            _levelName = "Level 3";
            _levelNumber = 3;
        }

        public override void Activate()
        {
            #region collectables
            CollectableTriangle collectable1_1 = new CollectableTriangle(
                ScreenManager.Game,
                new Vector2(35, BASE_PLATFORM_Y - 210),
                1f,
                false
            );
            CollectableTriangle collectable1_2 = new CollectableTriangle(
                ScreenManager.Game,
                new Vector2(Constants.SCREEN_WIDTH - 70 - 35, BASE_PLATFORM_Y - 50),
                1f,
                true
            );
            CollectableTriangle collectable1_3 = new CollectableTriangle(
                ScreenManager.Game,
                new Vector2(Constants.SCREEN_WIDTH - 35, BASE_PLATFORM_Y - 280 - 50),
                1f,
                false
            );
            CollectableTriangle collectable2_1 = new CollectableTriangle(
                ScreenManager.Game,
                new Vector2(210 + 35, BASE_PLATFORM_Y - 140 - 50),
                1f,
                false
            );
            CollectableTriangle collectable2_2 = new CollectableTriangle(
                ScreenManager.Game,
                new Vector2(350 + 35, BASE_PLATFORM_Y - 70 - 50),
                1f,
                false
            );
            CollectableTriangle collectable2_3 = new CollectableTriangle(
                ScreenManager.Game,
                new Vector2(Constants.SCREEN_WIDTH / 2, BASE_PLATFORM_Y - 50),
                1f,
                true
            );
            CollectableTriangle collectable2_4 = new CollectableTriangle(
                ScreenManager.Game,
                new Vector2(Constants.SCREEN_WIDTH - 35, BASE_PLATFORM_Y - 280 - 50),
                1f,
                true
            );
            CollectableTriangle collectable3_1 = new CollectableTriangle(
                ScreenManager.Game,
                new Vector2(35, BASE_PLATFORM_Y - 50),
                1f,
                false
            );
            CollectableTriangle collectable3_2 = new CollectableTriangle(
                ScreenManager.Game,
                new Vector2(35, BASE_PLATFORM_Y - 280 - 50),
                1f,
                true
            );
            CollectableTriangle collectable3_3 = new CollectableTriangle(
                ScreenManager.Game,
                new Vector2(Constants.SCREEN_WIDTH - 35, BASE_PLATFORM_Y - 70 - 50),
                1f,
                false
            );
            CollectableTriangle collectable4_1 = new CollectableTriangle(
                ScreenManager.Game,
                new Vector2(35, BASE_PLATFORM_Y - 280 - 50),
                1f,
                true
            );
            CollectableTriangle collectable4_2 = new CollectableTriangle(
                ScreenManager.Game,
                new Vector2(Constants.SCREEN_WIDTH / 2, BASE_PLATFORM_Y - 50),
                1f,
                false
            );
            CollectableTriangle collectable4_3 = new CollectableTriangle(
                ScreenManager.Game,
                new Vector2(Constants.SCREEN_WIDTH - 350 - 35, BASE_PLATFORM_Y - 70 - 50),
                1f,
                true
            );
            CollectableTriangle collectable4_4 = new CollectableTriangle(
                ScreenManager.Game,
                new Vector2(Constants.SCREEN_WIDTH - 280, BASE_PLATFORM_Y - 140 - 50),
                1f,
                true
            );
            CollectableTriangle collectable4_5 = new CollectableTriangle(
                ScreenManager.Game,
                new Vector2(Constants.SCREEN_WIDTH - 35, BASE_PLATFORM_Y - 50),
                1f,
                false
            );

            collectable1_1.relatedCollectables = [collectable4_4, collectable2_1];
            collectable1_2.relatedCollectables = [collectable3_1, collectable4_5];
            collectable1_3.relatedCollectables = [collectable2_4, collectable3_2, collectable4_1];
            collectable2_1.relatedCollectables = [collectable1_1, collectable4_4];
            collectable2_2.relatedCollectables = [collectable3_3, collectable4_3];
            collectable2_3.relatedCollectables = [collectable4_2];
            collectable2_4.relatedCollectables = [collectable1_3, collectable3_2, collectable4_1];
            collectable3_1.relatedCollectables = [collectable1_2, collectable4_5];
            collectable3_2.relatedCollectables = [collectable1_3, collectable2_4, collectable4_1];
            collectable3_3.relatedCollectables = [collectable2_2, collectable4_3];
            collectable4_1.relatedCollectables = [collectable1_3, collectable2_4, collectable3_2];
            collectable4_2.relatedCollectables = [collectable2_3];
            collectable4_3.relatedCollectables = [collectable2_2, collectable3_3];
            collectable4_4.relatedCollectables = [collectable1_1, collectable2_1];
            collectable4_5.relatedCollectables = [collectable1_2, collectable3_1];
            #endregion


            RotatableGameScreenSide _first = new()
            {
                CollidablePlatforms =
                [
                    new Platform(
                        new BoundingRectangle(0, BASE_PLATFORM_Y, 490, 10),
                        Color.Black,
                        true
                    ),
                    new Platform(
                        new BoundingRectangle(
                            Constants.SCREEN_WIDTH - 490,
                            BASE_PLATFORM_Y,
                            490,
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
                        Constants.NON_JUMPABLE_COLLIDABLE_COLOR,
                        true
                    ),
                ],
                NonCollidablePlatforms =
                [
                    new Platform(
                        new BoundingRectangle(0, BASE_PLATFORM_Y - 210, 70, 140),
                        Constants.JUMPABLE_NON_COLLIDABLE_COLOR,
                        false
                    ),
                    new Platform(
                        new BoundingRectangle(70, BASE_PLATFORM_Y - 70, 140, 70),
                        Constants.JUMPABLE_NON_COLLIDABLE_COLOR,
                        false
                    ),
                    new Platform(
                        new BoundingRectangle(350, BASE_PLATFORM_Y - 70, 140, 70),
                        Constants.JUMPABLE_NON_COLLIDABLE_COLOR,
                        false
                    ),
                    new Platform(
                        new BoundingRectangle(
                            490,
                            BASE_PLATFORM_Y - 280,
                            Constants.SCREEN_WIDTH - 490,
                            10
                        ),
                        Color.Gray,
                        false
                    ),
                    new Platform(
                        new BoundingRectangle(490 - 10, BASE_PLATFORM_Y - 280, 10, 280),
                        Color.Gray,
                        false
                    ),
                ],
                Collectables = [collectable1_1, collectable1_2, collectable1_3],
            };
            RotatableGameScreenSide _second = new()
            {
                CollidablePlatforms =
                [
                    new Platform(
                        new BoundingRectangle(0, BASE_PLATFORM_Y, 70, 10),
                        Color.Black,
                        true
                    ),
                    new Platform(
                        new BoundingRectangle(0, BASE_PLATFORM_Y - 140, 70, 140),
                        Constants.NON_JUMPABLE_COLLIDABLE_COLOR,
                        true
                    ),
                    new Platform(
                        new BoundingRectangle(
                            Constants.SCREEN_WIDTH / 2 - (350 / 2),
                            BASE_PLATFORM_Y,
                            350,
                            10
                        ),
                        Color.Black,
                        true
                    ),
                    new Platform(
                        new BoundingRectangle(Constants.SCREEN_WIDTH - 70, BASE_PLATFORM_Y, 70, 10),
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
                        Constants.NON_JUMPABLE_COLLIDABLE_COLOR,
                        true
                    ),
                    new Platform(
                        new BoundingRectangle(
                            Constants.SCREEN_WIDTH - 140,
                            BASE_PLATFORM_Y - 280,
                            140,
                            280
                        ),
                        Color.Black,
                        true
                    ),
                ],
                NonCollidablePlatforms =
                [
                    new Platform(
                        new BoundingRectangle(210, BASE_PLATFORM_Y - 140, 140, 70),
                        Constants.JUMPABLE_NON_COLLIDABLE_COLOR,
                        false
                    ),
                    new Platform(
                        new BoundingRectangle(350, BASE_PLATFORM_Y - 210, 140, 70),
                        Constants.JUMPABLE_NON_COLLIDABLE_COLOR,
                        false
                    ),
                ],
                Collectables = [collectable2_1, collectable2_2, collectable2_3, collectable2_4],
            };
            RotatableGameScreenSide _third = new()
            {
                CollidablePlatforms =
                [
                    BASE_PLATFORM,
                    new Platform(
                        new BoundingRectangle(
                            0,
                            BASE_PLATFORM_Y - 280,
                            Constants.SCREEN_WIDTH - 490,
                            10
                        ),
                        Color.Black,
                        true
                    ),
                    new Platform(
                        new BoundingRectangle(
                            Constants.SCREEN_WIDTH - 490 - 10,
                            BASE_PLATFORM_Y - 280,
                            10,
                            280
                        ),
                        Color.Black,
                        true
                    ),
                    new Platform(
                        new BoundingRectangle(
                            Constants.SCREEN_WIDTH - 210,
                            BASE_PLATFORM_Y - 70,
                            140,
                            70
                        ),
                        Constants.JUMPABLE_COLLIDABLE_COLOR,
                        true
                    ),
                    new Platform(
                        new BoundingRectangle(
                            Constants.SCREEN_WIDTH - 490,
                            BASE_PLATFORM_Y - 70,
                            140,
                            70
                        ),
                        Constants.JUMPABLE_COLLIDABLE_COLOR,
                        true
                    ),
                ],
                NonCollidablePlatforms =
                [
                    new Platform(
                        new BoundingRectangle(
                            Constants.SCREEN_WIDTH - 70,
                            BASE_PLATFORM_Y - 210,
                            70,
                            140
                        ),
                        Constants.JUMPABLE_NON_COLLIDABLE_COLOR,
                        false
                    ),
                ],
                Collectables = [collectable3_1, collectable3_2, collectable3_3],
            };
            RotatableGameScreenSide _fourth = new()
            {
                CollidablePlatforms =
                [
                    BASE_PLATFORM,
                    new Platform(
                        new BoundingRectangle(
                            Constants.SCREEN_WIDTH - 350,
                            BASE_PLATFORM_Y - 140,
                            140,
                            70
                        ),
                        Constants.JUMPABLE_COLLIDABLE_COLOR,
                        true
                    ),
                    new Platform(
                        new BoundingRectangle(
                            Constants.SCREEN_WIDTH - 490,
                            BASE_PLATFORM_Y - 210,
                            140,
                            70
                        ),
                        Constants.JUMPABLE_COLLIDABLE_COLOR,
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
                        Constants.NON_JUMPABLE_NON_COLLIDABLE_COLOR,
                        false
                    ),
                    new Platform(
                        new BoundingRectangle(0, BASE_PLATFORM_Y - 280, 140, 280),
                        Color.Gray,
                        false
                    ),
                    new Platform(
                        new BoundingRectangle(0, BASE_PLATFORM_Y - 70, 70, 70),
                        Constants.JUMPABLE_NON_COLLIDABLE_COLOR,
                        false
                    ),
                ],
                Collectables =
                [
                    collectable4_1,
                    collectable4_2,
                    collectable4_3,
                    collectable4_4,
                    collectable4_5,
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

            if (levelCompleted)
            {
                ScreenManager.AddScreen(new WinScreen(), ControllingPlayer);
                ScreenManager.RemoveScreen(this);
                _stickFigureSprite.StopSoundEffects();
            }
            //UpdateLoadNextLevel(new Level4Screen());
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
