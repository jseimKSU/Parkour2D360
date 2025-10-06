using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Parkour2D360.Collisions;
using Parkour2D360.Sprites;
using Parkour2D360.StateManagment;
using System;
using System.Collections.Generic;

namespace Parkour2D360.Screens
{
    public class Level1Screen : PlayableGameScreen
    {
        public Level1Screen()
        {
            base.Initialize();
        }

        public override void Activate()
        {
            base.Activate();
            
            _platforms.Add(new BoundingRectangle(0, Constants.SCREEN_HEIGHT-100, Constants.SCREEN_WIDTH, 10));

            _platforms.Add(new BoundingRectangle(200, Constants.SCREEN_HEIGHT-100-70, 70, 70));
            _platforms.Add(new BoundingRectangle(400, Constants.SCREEN_HEIGHT-100-70, 70, 70));
            _platforms.Add(new BoundingRectangle(600, Constants.SCREEN_HEIGHT-100-70, 70, 70));
            _platforms.Add(new BoundingRectangle(800, Constants.SCREEN_HEIGHT-100-70, 70, 70));
            _platforms.Add(new BoundingRectangle(1000, Constants.SCREEN_HEIGHT-100-70, 70, 70));

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
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            base.HandleInput(gameTime, input);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            DrawLevelPlatforms();
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawLevelPlatforms()
        {
            foreach(BoundingRectangle platform in _platforms)
            {
                _spriteBatch.Draw(_platformTexture, new Rectangle((int)platform.X, (int)platform.Y, (int)platform.Width, (int)platform.Height), Color.Black);
            }
        }
    }
}
