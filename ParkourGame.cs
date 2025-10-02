using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Parkour2D360.Collisions;
using Parkour2D360.Screens;
using Parkour2D360.Sprites;
using Parkour2D360.StateManagment;
using System.Collections.Generic;

namespace Parkour2D360
{
    public class ParkourGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private readonly ScreenManager _screenManager;

        private Texture2D _hitboxOutlineTexture;

        private InputAction _exitGameAwesomeVersion;
        private InputAction _exitGameSimpleVersion;

        private InputState _inputState;

        public ParkourGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            ScreenFactory screenFactory = new ScreenFactory();
            Services.AddService(typeof(IScreenFactory), screenFactory);

            _screenManager = new ScreenManager(this);
            Components.Add(_screenManager);

            _exitGameAwesomeVersion = new InputAction(
                [Buttons.LeftShoulder, Buttons.RightShoulder, Buttons.A, Buttons.B, Buttons.X, Buttons.Y, Buttons.LeftStick], 
                [Keys.Q, Keys.W, Keys.E, Keys.R, Keys.T, Keys.Y], 
                false);
            _exitGameSimpleVersion = new InputAction(
                [Buttons.Back], 
                [Keys.Escape], 
                false);

            _inputState = new InputState();

            AddAllGameScreens();
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = Constants.SCREEN_WIDTH;
            _graphics.PreferredBackBufferHeight = Constants.SCREEN_HEIGHT;
            //_graphics.IsFullScreen = true;
            _graphics.ApplyChanges();
            
            base.Initialize();
        }

        private void AddAllGameScreens()
        {
            _screenManager.AddScreen(new TitleScreen(), null);
        }

        protected override void LoadContent()
        {
            _hitboxOutlineTexture = new Texture2D(GraphicsDevice, 1, 1);
            _hitboxOutlineTexture.SetData(new[] { Color.White });
        }

        protected override void Update(GameTime gameTime)
        {
            _inputState.Update();

            CheckForExit();

            base.Update(gameTime);
        }
        #region Update Helper Methods

        private void CheckForExit()
        {

            if (
                _exitGameSimpleVersion.AllInputsOccured(_inputState, PlayerIndex.One, out PlayerIndex player) ||
                _exitGameAwesomeVersion.AllInputsOccured(_inputState, PlayerIndex.One, out player)
                )
                Exit();
        }



        private bool EscapeOrBackPressed()
        {
            return (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape));
        }

        private bool LongComboWasPressed()
        {
            GamePadState _gamePadState = GamePad.GetState(PlayerIndex.One);
            KeyboardState _keyboardState = Keyboard.GetState();
            return (
                _gamePadState.Buttons.LeftShoulder == ButtonState.Pressed &&
                _gamePadState.Buttons.RightShoulder == ButtonState.Pressed &&
                _gamePadState.Buttons.A == ButtonState.Pressed &&
                _gamePadState.Buttons.B == ButtonState.Pressed &&
                _gamePadState.Buttons.X == ButtonState.Pressed &&
                _gamePadState.Buttons.Y == ButtonState.Pressed &&
                _gamePadState.Buttons.LeftStick == ButtonState.Pressed
                ) || (
                _keyboardState.IsKeyDown(Keys.Q) &&
                _keyboardState.IsKeyDown(Keys.W) &&
                _keyboardState.IsKeyDown(Keys.E) &&
                _keyboardState.IsKeyDown(Keys.R) &&
                _keyboardState.IsKeyDown(Keys.T) &&
                _keyboardState.IsKeyDown(Keys.Y)
                );
        }

        #endregion

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Wheat);

            base.Draw(gameTime);
        }
    }
}
