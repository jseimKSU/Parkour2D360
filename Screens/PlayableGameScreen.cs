using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Parkour2D360.Collisions;
using Parkour2D360.SettingsFolder;
using Parkour2D360.Sprites;
using Parkour2D360.StateManagment;

namespace Parkour2D360.Screens
{
    public class PlayableGameScreen : GameScreen
    {
        protected StickFigureSprite _stickFigureSprite;

        protected List<BoundingRectangle> _nonPlatformHitboxes = [];

        protected List<RotatableGameScreenSide> _gamescreenSides = [];
        protected int _currentGameScreenSide = 0;

        protected List<BoundingRectangle> _allHitboxes =>
            [
                .. _nonPlatformHitboxes,
                .. _gamescreenSides[_currentGameScreenSide]
                    .Platforms.Where(plat => plat.IsCollidable == true)
                    .Select(plat => plat.Location)
                    .ToList(),
            ];

        protected ContentManager ContentManager;
        protected SpriteBatch _spriteBatch;
        protected InputState _inputState;

        protected InputAction _rotateLeft;
        protected InputAction _rotateRight;
        private InputAction _pauseGameAwesomeVersion;
        private InputAction _pauseGameSimpleVersion;

        protected Texture2D _platformTexture;

        protected bool _currentInputIsKeyboard;

        protected void Initialize()
        {
            _stickFigureSprite = new StickFigureSprite();
            _inputState = new InputState();

            TransitionOnTime = TimeSpan.FromSeconds(1);
            TransitionOffTime = TimeSpan.FromSeconds(1);

            _pauseGameAwesomeVersion = new InputAction(
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
                true
            );
            _pauseGameSimpleVersion = new InputAction([Buttons.Back], [Keys.Escape], true);
        }

        public override void Activate()
        {
            base.Activate();
            _spriteBatch = ScreenManager.SpriteBatch;

            if (ContentManager == null)
            {
                ContentManager = new ContentManager(ScreenManager.Game.Services, "Content");
            }

            _stickFigureSprite.Initalize(ScreenManager.Settings);

            bool movementOnAWSD = (
                ScreenManager.Settings.KeyboardOptions == KeyboardOptions.MovementOnAWSD
            );

            _rotateLeft = new InputAction(
                [Buttons.DPadLeft],
                [(movementOnAWSD) ? Keys.Left : Keys.A],
                true
            );
            _rotateRight = new InputAction(
                [Buttons.DPadRight],
                [(movementOnAWSD) ? Keys.Right : Keys.D],
                true
            );

            _stickFigureSprite.LoadContent(ContentManager);

            _platformTexture = new Texture2D(ScreenManager.GraphicsDevice, 1, 1);
            _platformTexture.SetData(new[] { Color.White });
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

            _inputState.Update();
            _currentInputIsKeyboard = _inputState.CurrentInputIsKeyboard[0];

            _stickFigureSprite.Update(gameTime, _allHitboxes);

            if (_rotateLeft.Occurred(_inputState, PlayerIndex.One, out PlayerIndex player))
            {
                if (_currentGameScreenSide > 0)
                    _currentGameScreenSide--;
                else if (_currentGameScreenSide == 0)
                    _currentGameScreenSide = _gamescreenSides.Count - 1;
            }
            if (_rotateRight.Occurred(_inputState, PlayerIndex.One, out player))
            {
                if (_currentGameScreenSide < _gamescreenSides.Count - 1)
                    _currentGameScreenSide++;
                else if (_currentGameScreenSide == _gamescreenSides.Count - 1)
                    _currentGameScreenSide = 0;
            }
            if (_currentGameScreenSide < 0 || _currentGameScreenSide >= _gamescreenSides.Count)
                throw new System.IndexOutOfRangeException();
            if (_stickFigureSprite.Hitbox.Y > Constants.SCREEN_HEIGHT)
            {
                LoadingScreen.Load(ScreenManager, true, null, new TitleScreen()); // switch to fail screen
            }
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            base.HandleInput(gameTime, input);

            if (
                _pauseGameAwesomeVersion.AllInputsOccured(
                    _inputState,
                    ControllingPlayer,
                    out PlayerIndex player
                )
                || _pauseGameSimpleVersion.AllInputsOccured(
                    _inputState,
                    ControllingPlayer,
                    out player
                )
            )
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            _spriteBatch.Begin();
            DrawGameScreenExitInstructions.DrawExitInstructions(
                ContentManager,
                _spriteBatch,
                _currentInputIsKeyboard
            );
            _stickFigureSprite.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();
        }

        protected void DrawPlatform(BoundingRectangle platform, Color color)
        {
            _spriteBatch.Draw(
                _platformTexture,
                new Rectangle(
                    (int)platform.X,
                    (int)platform.Y,
                    (int)platform.Width,
                    (int)platform.Height
                ),
                null,
                color,
                platform.Angle,
                new Vector2(0, 0),
                SpriteEffects.None,
                0
            );
        }
    }
}
