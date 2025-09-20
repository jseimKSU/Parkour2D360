using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Parkour2D360.Collisions;
using Parkour2D360.Sprites;
using System.Collections.Generic;

namespace Parkour2D360.Screens
{
    public class TitleScreen : IGameScreen
    {
        private int _titleScreenId = 0;
        private Font2DSprite _2DText;
        private StickFigureSprite _stickFigureSprite;
        private GrassSprite _grassSprite;
        private List<BoundingRectangle> _itemsWithHitboxes = [];

        private SpriteFont _360Font;
        private SpriteFont _parkourFont;

        private GamePadState _gamePadState;
        private KeyboardState _keyboardState;
        private ContentManager ContentManager;

        private bool _currentInputIsKeyboard;

        public int GetId()
        {
            return _titleScreenId;
        }

        public void Initialize()
        {
            _2DText = new Font2DSprite();
            _stickFigureSprite = new StickFigureSprite();
            _stickFigureSprite.Initalize();
            _grassSprite = new(Constants.SCREEN_WIDTH, Constants.SCREEN_HEIGHT);

            _itemsWithHitboxes.Add(_2DText.Hitbox);
        }

        public void LoadContent(ContentManager content)
        {
            ContentManager = content;
            _2DText.LoadContent(content);
            _stickFigureSprite.LoadContent(content);
            _grassSprite.LoadContent(content);

            _360Font = content.Load<SpriteFont>("Font3D");
            _parkourFont = content.Load<SpriteFont>("Orbitron100");

        }

        public void Update(GameTime gameTime)
        {
            _gamePadState = GamePad.GetState(0);
            _keyboardState = Keyboard.GetState();

            CheckForCurrentInputType();

            _stickFigureSprite.Update(gameTime, _itemsWithHitboxes);

        }

        #region Update Helper Methods
        private void CheckForCurrentInputType()
        {
            if (_keyboardState.GetPressedKeyCount() > 0) _currentInputIsKeyboard = true;
            else if (
                _gamePadState.IsButtonDown(Buttons.A) ||
                _gamePadState.IsButtonDown(Buttons.B) ||
                _gamePadState.IsButtonDown(Buttons.X) ||
                _gamePadState.IsButtonDown(Buttons.Y) ||
                _gamePadState.IsButtonDown(Buttons.Start) ||
                _gamePadState.IsButtonDown(Buttons.DPadUp) ||
                _gamePadState.IsButtonDown(Buttons.DPadDown) ||
                _gamePadState.IsButtonDown(Buttons.DPadLeft) ||
                _gamePadState.IsButtonDown(Buttons.DPadRight) ||
                _gamePadState.IsButtonDown(Buttons.LeftTrigger) ||
                _gamePadState.IsButtonDown(Buttons.RightTrigger) ||
                _gamePadState.IsButtonDown(Buttons.LeftStick) ||
                _gamePadState.IsButtonDown(Buttons.RightStick) ||
                _gamePadState.IsButtonDown(Buttons.LeftShoulder) ||
                _gamePadState.IsButtonDown(Buttons.RightShoulder) ||
                _gamePadState.ThumbSticks.Left != Vector2.Zero ||
                _gamePadState.ThumbSticks.Right != Vector2.Zero
                ) _currentInputIsKeyboard = false;
        }

        #endregion

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            DrawGameScreenExitInstructions.DrawExitInstructions(ContentManager, spriteBatch, _currentInputIsKeyboard);
            _2DText.Draw(spriteBatch);
            _stickFigureSprite.Draw(gameTime, spriteBatch);
            _grassSprite.Draw(spriteBatch);
            spriteBatch.DrawString(_360Font, "360", new Vector2(540, 235), Color.Black);
            spriteBatch.DrawString(_parkourFont, "PARKOUR", new Vector2(840, 215), Color.Black);

        }
    }
}
