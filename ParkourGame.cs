using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Parkour2D360.Screens;
using Parkour2D360.StateManagment;

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
                [
                    Buttons.LeftShoulder,
                    Buttons.RightShoulder,
                    Buttons.A,
                    Buttons.B,
                    Buttons.X,
                    Buttons.Y,
                    Buttons.LeftStick,
                ],
                [Keys.Q, Keys.W, Keys.E, Keys.R, Keys.T, Keys.Y],
                false
            );
            _exitGameSimpleVersion = new InputAction([Buttons.Back], [Keys.Escape], false);

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
                _exitGameSimpleVersion.AllInputsOccured(
                    _inputState,
                    PlayerIndex.One,
                    out PlayerIndex player
                )
                || _exitGameAwesomeVersion.AllInputsOccured(
                    _inputState,
                    PlayerIndex.One,
                    out player
                )
            )
                Exit();
        }

        #endregion

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Wheat);

            //SpriteBatch spriteBatch = new SpriteBatch(GraphicsDevice);
            //spriteBatch.Begin();
            //spriteBatch.Draw(_hitboxOutlineTexture, new Rectangle(_graphics.PreferredBackBufferWidth/2, _graphics.PreferredBackBufferHeight/2, 10,10), Color.Black);
            //spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
