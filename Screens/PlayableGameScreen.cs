using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Parkour2D360.Collisions;
using Parkour2D360.Sprites;
using Parkour2D360.StateManagment;
using System;
using System.Collections.Generic;

namespace Parkour2D360.Screens
{
    public class PlayableGameScreen : GameScreen
    {
        protected StickFigureSprite _stickFigureSprite;

        protected List<BoundingRectangle> _itemsWithHitboxes = [];
        protected List<BoundingRectangle> _platforms = [];

        protected ContentManager ContentManager;
        protected SpriteBatch _spriteBatch;
        protected InputState _inputState;

        protected Texture2D _platformTexture;

        protected bool _currentInputIsKeyboard;

        protected void Initialize()
        {
            _stickFigureSprite = new StickFigureSprite();
            _stickFigureSprite.Initalize();
            _inputState = new InputState();
        }

        public override void Activate()
        {
            base.Activate();
            _spriteBatch = ScreenManager.SpriteBatch;

            if (ContentManager == null)
            {
                ContentManager = new ContentManager(ScreenManager.Game.Services, "Content");
            }

            _stickFigureSprite.LoadContent(ContentManager);

            _platformTexture = new Texture2D(ScreenManager.GraphicsDevice, 1, 1);
            _platformTexture.SetData(new[] { Color.White });

            _itemsWithHitboxes.AddRange(_platforms);
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

            _inputState.Update();
            _currentInputIsKeyboard = _inputState.CurrentInputIsKeyboard[0];

            _stickFigureSprite.Update(gameTime, _itemsWithHitboxes);
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            base.HandleInput(gameTime, input);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            _spriteBatch.Begin();
            DrawGameScreenExitInstructions.DrawExitInstructions(ContentManager, _spriteBatch, _currentInputIsKeyboard);
            _stickFigureSprite.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();
        }

        /// <summary>
        /// startPoint is assumed to be the lower left point on the diagonal
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <param name="color"></param>
        protected void DrawDiagonalPlatform(BoundingRectangle platform, Color color)
        {
            _spriteBatch.Draw(
                _platformTexture,
                new Rectangle((int)platform.X, (int)platform.Y, (int)platform.Width, (int)platform.Height),
                null,
                color,
                platform.Angle,
                new Vector2(0, 0),
                SpriteEffects.None,
                0);
        }
    }

}
