using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Parkour2D360.Collisions;
using Parkour2D360.Screens;
using Parkour2D360.Sprites;
using System.Collections.Generic;

namespace Parkour2D360
{
    public class ParkourGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _hitboxOutlineTexture;

        private GamePadState _gamePadState;
        private KeyboardState _keyboardState;

        private List<IGameScreen> _gameScreens = [];
        private IGameScreen _currentGameScreen; // make as index?

        public ParkourGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = Constants.SCREEN_WIDTH;
            _graphics.PreferredBackBufferHeight = Constants.SCREEN_HEIGHT;
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();

            AddAllGameScreens();

            foreach (IGameScreen screen in _gameScreens)
            {
                screen.Initialize();
                if (screen.GetId() == 0)
                {
                    _currentGameScreen = screen;
                }
            }
            
            base.Initialize();
        }

        private void AddAllGameScreens()
        {
            _gameScreens.Add(new TitleScreen());
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // Load all content in all gamescreens?

            foreach(IGameScreen screen in _gameScreens)
            {
                screen.LoadContent(Content);
            }


            _hitboxOutlineTexture = new Texture2D(GraphicsDevice, 1, 1);
            _hitboxOutlineTexture.SetData(new[] { Color.White });

        }

        protected override void Update(GameTime gameTime)
        {
            _gamePadState = GamePad.GetState(0);
            _keyboardState = Keyboard.GetState();

            CheckForExit();

            // Check for change in gameScreen

            _currentGameScreen.Update(gameTime);

            base.Update(gameTime);
        }
        #region Update Helper Methods

        private void CheckForExit()
        {

            if (
                EscapeOrBackPressed() ||
                LongComboWasPressed()
                )
                Exit();
        }



        private bool EscapeOrBackPressed()
        {
            return (_gamePadState.Buttons.Back == ButtonState.Pressed || _keyboardState.IsKeyDown(Keys.Escape));
        }

        private bool LongComboWasPressed()
        {
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

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            _currentGameScreen.Draw(_spriteBatch, gameTime);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
