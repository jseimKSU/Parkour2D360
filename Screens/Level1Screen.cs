using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Parkour2D360.Collisions;
using Parkour2D360.Sprites;
using Parkour2D360.StateManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkour2D360.Screens
{
    public class Level1Screen : GameScreen
    {
        private StickFigureSprite _stickFigureSprite;
        private List<BoundingRectangle> _itemsWithHitboxes = [];

        private ContentManager _content;

        private bool _currentInputIsKeyboard;

        public Level1Screen()
        {
            _stickFigureSprite = new StickFigureSprite();
            _stickFigureSprite.Initalize();
        }

        public override void Activate()
        {
            if (_content == null)
            {
                _content = new ContentManager(ScreenManager.Game.Services, "Content");
            }
            _stickFigureSprite.LoadContent(_content);
        }

        public override void Deactivate()
        {
            base.Deactivate();
        }

        public override void Unload()
        {
            base.Unload();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            CheckForCurrentInputType();

            _stickFigureSprite.Update(gameTime, _itemsWithHitboxes);
        }
        private void CheckForCurrentInputType()
        {
            var _keyboardState = Keyboard.GetState();
            var _gamePadState = GamePad.GetState(PlayerIndex.One);

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

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            base.HandleInput(gameTime, input);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();
            DrawGameScreenExitInstructions.DrawExitInstructions(_content, spriteBatch, _currentInputIsKeyboard);
            _stickFigureSprite.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }
    }
}
