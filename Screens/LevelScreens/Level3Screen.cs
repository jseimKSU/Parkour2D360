using Microsoft.Xna.Framework;

namespace Parkour2D360.Screens.LevelScreens
{
    public class Level3Screen : LevelScreen
    {
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
                CollidablePlatforms = [BASE_PLATFORM],
                NonCollidablePlatforms = [],
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

            //UpdateLoadNextLevel(new Level4Screen());
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
