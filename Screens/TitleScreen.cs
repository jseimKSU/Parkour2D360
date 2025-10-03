using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Parkour2D360.Collisions;
using Parkour2D360.Sprites;
using Parkour2D360.StateManagment;
using System;
using System.Collections.Generic;

namespace Parkour2D360.Screens
{
    public class TitleScreen : GameScreen
    {
        private Font2DSprite _2DText;
        private BoundingRectangle _titleTextHitbox;
        private StickFigureSprite _stickFigureSprite;
        private GrassSprite _grassSprite;
        private List<BoundingRectangle> _itemsWithHitboxes = [];

        private SpriteFont _360Font;
        private SpriteFont _parkourFont;
        private Song _backgroundMusic;

        private InputAction _level1;
        private InputState _inputState;

        private ContentManager ContentManager;

        private bool _currentInputIsKeyboard;

        public TitleScreen()
        {
            _2DText = new Font2DSprite();
            _stickFigureSprite = new StickFigureSprite();
            _stickFigureSprite.Initalize();
            _grassSprite = new();
            _titleTextHitbox = new BoundingRectangle(x:300, y:250, width:1288, height:120);
            _inputState = new InputState();

            _itemsWithHitboxes.Add(_grassSprite.Hitbox);
            _itemsWithHitboxes.Add(_titleTextHitbox);

            _level1 = new InputAction([Buttons.DPadUp], [Keys.D1], false);
        }

        public override void Activate()
        {
            base.Activate();

            if (ContentManager == null)
            {
                ContentManager = new ContentManager(ScreenManager.Game.Services, "Content");
            }
            _2DText.LoadContent(ContentManager);
            _stickFigureSprite.LoadContent(ContentManager);
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

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            _inputState.Update();

            _currentInputIsKeyboard = _inputState.CurrentInputIsKeyboard[0];

            _stickFigureSprite.Update(gameTime, _itemsWithHitboxes);

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
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();
            DrawGameScreenExitInstructions.DrawExitInstructions(ContentManager, spriteBatch, _currentInputIsKeyboard);
            _2DText.Draw(spriteBatch);
            _stickFigureSprite.Draw(gameTime, spriteBatch);
            _grassSprite.Draw(spriteBatch);
            spriteBatch.DrawString(_360Font, "360", new Vector2(540, 235), Color.Black);
            spriteBatch.DrawString(_parkourFont, "PARKOUR", new Vector2(840, 215), Color.Black);
            spriteBatch.End();


        }
    }
}
