using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Parkour2D360.Collisions;

namespace Parkour2D360.Sprites
{
    public class Font2DSprite
    {
        private const string SPRITE_TEXTURE_FOLDER = "SpriteTextures";
        private const int SPRITE_WIDTH = 12;
        private const int SPRITE_HEIGHT = 12;
        private const int SPRITE_SCALE_FACTOR = 10;

        private Texture2D _texture;
        private Vector2 _position = new Vector2(300, 250);
        private Rectangle _2Rectangle;
        private Rectangle _DRectangle;

        public BoundingRectangle Hitbox { get; set; }

        public Font2DSprite()
        {
            _2Rectangle = new(12 * 1, 28, 12, 12);
            _DRectangle = new(12 * 3, 56, 12, 12);
            Hitbox = new BoundingRectangle(
                _position,
                SPRITE_WIDTH * 2 * SPRITE_SCALE_FACTOR,
                SPRITE_HEIGHT * SPRITE_SCALE_FACTOR
            );
        }

        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>($"{SPRITE_TEXTURE_FOLDER}/sprFont");
        }

        public void Update(GameTime gameTime) { }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _texture,
                _position,
                _2Rectangle,
                Color.White,
                0,
                new Vector2(0, 0),
                10f,
                SpriteEffects.None,
                0
            );
            spriteBatch.Draw(
                _texture,
                new Vector2(_position.X + (SPRITE_SCALE_FACTOR * 10), _position.Y),
                _DRectangle,
                Color.White,
                0,
                new Vector2(0, 0),
                SPRITE_SCALE_FACTOR,
                SpriteEffects.None,
                0
            );
        }
    }
}
