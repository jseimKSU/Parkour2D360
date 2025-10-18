using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Parkour2D360.Collisions;

namespace Parkour2D360.Sprites
{
    public class LevelSelectSprite
    {
        private const string SPRITE_TEXTURE_FOLDER = "SpriteTextures";
        private const int SPRITE_WIDTH = 100;
        private const int SPRITE_HEIGHT = 24;
        private const int SPRITE_SCALE_FACTOR = 3;

        private Texture2D _mainTexture;
        private Texture2D _glowTexture;
        private Vector2 _position = new Vector2(
            (Constants.SCREEN_WIDTH / 2) - ((SPRITE_WIDTH * SPRITE_SCALE_FACTOR) / 2),
            Constants.SCREEN_HEIGHT - 470
        );

        private float _shakeTimer;

        public BoundingRectangle Hitbox { get; set; }

        public LevelSelectSprite()
        {
            Hitbox = new BoundingRectangle(
                _position,
                SPRITE_WIDTH * SPRITE_SCALE_FACTOR,
                SPRITE_HEIGHT * SPRITE_SCALE_FACTOR
            );
            _shakeTimer = 0;
        }

        public void LoadContent(ContentManager content)
        {
            _mainTexture = content.Load<Texture2D>($"{SPRITE_TEXTURE_FOLDER}/LevelSelectSprite");
            _glowTexture = content.Load<Texture2D>($"{SPRITE_TEXTURE_FOLDER}/glow");
        }

        public void Update(GameTime gameTime)
        {
            if (_shakeTimer < 3)
            {
                _shakeTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Random rand = new Random();
            float shakeIntensity = (_shakeTimer < 3) ? 3 : 0;
            float offsetX = (float)(rand.NextDouble() * 2 - 1) * shakeIntensity;
            float offsetY = (float)(rand.NextDouble() * 2 - 1) * shakeIntensity;

            Matrix shakeTransform = Matrix.CreateTranslation(offsetX, offsetY, 0);

            if (_shakeTimer < 3)
            {
                spriteBatch.Begin(transformMatrix: shakeTransform, blendState: BlendState.Additive);
                spriteBatch.Draw(
                    _glowTexture,
                    _position + new Vector2(-25, -150),
                    null,
                    Color.Blue,
                    0f,
                    Vector2.Zero,
                    (SPRITE_SCALE_FACTOR * 5f),
                    SpriteEffects.None,
                    0
                );
                spriteBatch.End();
            }

            spriteBatch.Begin(transformMatrix: shakeTransform, blendState: BlendState.AlphaBlend);

            spriteBatch.Draw(
                _mainTexture,
                _position,
                null,
                Color.White,
                0f,
                Vector2.Zero,
                SPRITE_SCALE_FACTOR,
                SpriteEffects.None,
                0
            );
            spriteBatch.End();
        }
    }
}
