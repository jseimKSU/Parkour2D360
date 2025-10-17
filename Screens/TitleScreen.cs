using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Parkour2D360.Collisions;
using Parkour2D360.Sprites;
using Parkour2D360.StateManagment;

namespace Parkour2D360.Screens
{
    /**
     * Need a button for the level select screen and a buton for the options menu
     * Later esc should also bring up a menu where you may easily select either from that menu
     * Level screen should have cards that can be locked and unlocked based on progress that allow for selection of the cards to go into that level
     * Options menu for now should have a spot to adjust music and sound effect volumes more will be added later
     */
    public class TitleScreen : PlayableGameScreen
    {
        private Font2DSprite _2DText;
        private BoundingRectangle _titleTextHitbox;
        private GrassSprite _grassSprite;

        private SpriteFont _360Font;
        private SpriteFont _parkourFont;
        private Song _backgroundMusic;

        private InputAction _level1;

        public TitleScreen()
        {
            _2DText = new Font2DSprite();
            base.Initialize();
            _grassSprite = new();
            _titleTextHitbox = new BoundingRectangle(x: 300, y: 250, width: 1288, height: 120);

            _nonPlatformHitboxes.Add(_grassSprite.Hitbox);
            _nonPlatformHitboxes.Add(_titleTextHitbox);

            _level1 = new InputAction([Buttons.DPadUp], [Keys.D1], false);
        }

        public override void Activate()
        {
            RotatableGameScreenSide _first = new() { Platforms = [] };
            _gamescreenSides.Add(_first);

            base.Activate();

            _2DText.LoadContent(ContentManager);
            _grassSprite.LoadContent(ContentManager);

            _backgroundMusic = ContentManager.Load<Song>("StruttinWithSomeBBQ");
            MediaPlayer.Play(_backgroundMusic);
            MediaPlayer.Volume = .1f;

            _360Font = ContentManager.Load<SpriteFont>("Font3D");
            _parkourFont = ContentManager.Load<SpriteFont>("Orbitron100");
        }

        public override void Deactivate()
        {
            base.Deactivate();
            MediaPlayer.Stop();
        }

        public override void Unload()
        {
            base.Unload();
            MediaPlayer.Stop();
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

            if (_level1.Occurred(_inputState, PlayerIndex.One, out PlayerIndex player))
            {
                LoadingScreen.Load(ScreenManager, true, player, new Level1Screen());
            }
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _2DText.Draw(_spriteBatch);
            _grassSprite.Draw(_spriteBatch);
            foreach (
                (BoundingRectangle, Color) platform in _gamescreenSides[
                    _currentGameScreenSide
                ].Platforms
            )
            {
                DrawPlatform(platform.Item1, platform.Item2);
            }
            _spriteBatch.DrawString(_360Font, "360", new Vector2(540, 235), Color.Black);
            _spriteBatch.DrawString(_parkourFont, "PARKOUR", new Vector2(840, 215), Color.Black);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
