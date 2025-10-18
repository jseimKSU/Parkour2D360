using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Parkour2D360.StateManagment;

namespace Parkour2D360.Screens
{
    public class LoadingScreen : GameScreen
    {
        private const string SPRITE_FILES_RELATIVE_PATH =
            "SpriteTextures/StickFigureCharacterSprites2D/Fighter sprites/Idle";
        private readonly bool _loadingIsSlow;
        private bool _otherScreensAreGone;
        private readonly GameScreen[] _screensToLoad;
        private ContentManager ContentManager;

        private Texture2D _loadingTexture;

        // Constructor is private: loading screens should be activated via the static Load method instead.
        private LoadingScreen(
            ScreenManager screenManager,
            bool loadingIsSlow,
            GameScreen[] screensToLoad
        )
        {
            _loadingIsSlow = loadingIsSlow;
            _screensToLoad = screensToLoad;

            TransitionOnTime = TimeSpan.FromSeconds(2);
        }

        public override void Activate()
        {
            base.Activate();

            if (ContentManager == null)
            {
                ContentManager = new ContentManager(ScreenManager.Game.Services, "Content");
            }

            _loadingTexture = ContentManager.Load<Texture2D>($"{SPRITE_FILES_RELATIVE_PATH}/idle1");
        }

        // Activates the loading screen.
        public static void Load(
            ScreenManager screenManager,
            bool loadingIsSlow,
            PlayerIndex? controllingPlayer,
            params GameScreen[] screensToLoad
        )
        {
            // Tell all the current screens to transition off.
            foreach (var screen in screenManager.GetScreens())
                screen.ExitScreen();

            // Create and activate the loading screen.
            var loadingScreen = new LoadingScreen(screenManager, loadingIsSlow, screensToLoad);

            screenManager.AddScreen(loadingScreen, controllingPlayer);
        }

        public override void Update(
            GameTime gameTime,
            bool otherScreenHasFocus,
            bool coveredByOtherScreen
        )
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            // If all the previous screens have finished transitioning
            // off, it is time to actually perform the load.
            if (_otherScreensAreGone)
            {
                ScreenManager.RemoveScreen(this);

                foreach (var screen in _screensToLoad)
                {
                    if (screen != null)
                        ScreenManager.AddScreen(screen, ControllingPlayer);
                }

                // Once the load has finished, we use ResetElapsedTime to tell
                // the  game timing mechanism that we have just finished a very
                // long frame, and that it should not try to catch up.
                ScreenManager.Game.ResetElapsedTime();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (ScreenState == ScreenState.Active && ScreenManager.GetScreens().Length == 1)
                _otherScreensAreGone = true;

            if (_loadingIsSlow)
            {
                var spriteBatch = ScreenManager.SpriteBatch;

                float rotateSpeed = 4f;
                float angle =
                    (float)(gameTime.TotalGameTime.TotalSeconds * rotateSpeed) % MathHelper.TwoPi;
                // Draw the text.
                spriteBatch.Begin();
                spriteBatch.Draw(
                    _loadingTexture,
                    new Vector2(Constants.SCREEN_WIDTH / 2, Constants.SCREEN_HEIGHT / 2),
                    null,
                    Color.Black,
                    angle,
                    new Vector2(_loadingTexture.Width / 2, _loadingTexture.Height / 2),
                    1f,
                    SpriteEffects.None,
                    0
                );
                spriteBatch.End();
            }
        }
    }
}
