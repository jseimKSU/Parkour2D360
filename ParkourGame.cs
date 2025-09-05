using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Parkour2D360.Sprites;
using System.Runtime.InteropServices.Marshalling;

namespace Parkour2D360
{
    public class ParkourGame : Game
    {
        private const string EXIT_COMBO_STRING_GAMEPAD = "Left Bumper + Right Bumper + A + B + X + Y + Left Stick";
        private const string EXIT_COMBO_STRING_KEYBOARD = "Q + W + E + R + T + Y";
        private const string EXIT_SIMPLE_GAMEPAD = "Back";
        private const string EXIT_SIMPLE_KEYBOARD = "Esc";

        private bool _currentInputIsKeyboard;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private GamePadState _gamePadState;
        private KeyboardState _keyboardState;

        private Font2DSprite _2DText;
        private StickFigureSprite _stickFigureSprite;
        private GrassSprite _grassSprite;
        private SpriteFont _360Font;
        private SpriteFont _parkourFont;
        private SpriteFont _exitInstructionsFont;

        public ParkourGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.ApplyChanges();

            // TODO: Add your initialization logic here
            // create sprites

            _2DText = new Font2DSprite();
            _stickFigureSprite = new StickFigureSprite();
            _grassSprite = new(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here

            _2DText.LoadContent(Content);
            _stickFigureSprite.LoadContent(Content);
            _grassSprite.LoadContent(Content);

            _360Font = Content.Load<SpriteFont>("Font3D");
            _parkourFont = Content.Load<SpriteFont>("Orbitron100");
            _exitInstructionsFont = Content.Load<SpriteFont>("OrbitronSmall");

        }

        protected override void Update(GameTime gameTime)
        {
            _gamePadState = GamePad.GetState(0);
            _keyboardState = Keyboard.GetState();
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

            if (
                EscapeOrBackPressed(_gamePadState, _keyboardState) ||
                LongComboWasPressed(_gamePadState, _keyboardState)
                )
                Exit();
            _stickFigureSprite.Update(gameTime);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        #region Update Helper Methods

        private bool EscapeOrBackPressed(GamePadState gamePadState, KeyboardState keyboardState)
        {
            return (gamePadState.Buttons.Back == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Escape));
        }

        private bool LongComboWasPressed(GamePadState gamePadState, KeyboardState keyboardState)
        {
            return (
                gamePadState.Buttons.LeftShoulder == ButtonState.Pressed &&
                gamePadState.Buttons.RightShoulder == ButtonState.Pressed &&
                gamePadState.Buttons.A == ButtonState.Pressed &&
                gamePadState.Buttons.B == ButtonState.Pressed &&
                gamePadState.Buttons.X == ButtonState.Pressed &&
                gamePadState.Buttons.Y == ButtonState.Pressed &&
                gamePadState.Buttons.LeftStick == ButtonState.Pressed
                ) || (
                keyboardState.IsKeyDown(Keys.Q) &&
                keyboardState.IsKeyDown(Keys.W) &&
                keyboardState.IsKeyDown(Keys.E) &&
                keyboardState.IsKeyDown(Keys.R) &&
                keyboardState.IsKeyDown(Keys.T) &&
                keyboardState.IsKeyDown(Keys.Y)
                );
        }

        #endregion

        protected override void Draw(GameTime gameTime)
        {
            ((string exitCombo,string simpleExit), bool isKeyboard) exitInstructions = (_currentInputIsKeyboard) ?
                ((EXIT_COMBO_STRING_KEYBOARD, EXIT_SIMPLE_KEYBOARD), true) :
            ((EXIT_COMBO_STRING_GAMEPAD, EXIT_SIMPLE_GAMEPAD), false);

            GraphicsDevice.Clear(Color.Wheat);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            _2DText.Draw(_spriteBatch);
            _stickFigureSprite.Draw(gameTime, _spriteBatch);
            _grassSprite.Draw(_spriteBatch);
            _spriteBatch.DrawString(_360Font, "360", new Vector2(540, 235), Color.Black);
            _spriteBatch.DrawString(_parkourFont, "PARKOUR", new Vector2(840, 215), Color.Black);
            _spriteBatch.DrawString(_exitInstructionsFont, $"Do {exitInstructions.Item1.exitCombo} to Exit or {exitInstructions.Item1.simpleExit}", (exitInstructions.isKeyboard) ? new Vector2(1635,20) : new Vector2(1380, 20), Color.Black);
            

            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
