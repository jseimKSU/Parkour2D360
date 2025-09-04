using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkour2D360.Sprites
{
    public class Font2DSprite
    {
        private const string SPRITE_TEXTURE_FOLDER = "SpriteTextures";

        private Texture2D _texture;
        private Vector2 _position;
        private Rectangle _2Rectangle = new(12*1, 28, 12, 12);
        private Rectangle _DRectangle = new(12*3, 56, 12, 12);

        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>($"{SPRITE_TEXTURE_FOLDER}/sprFont");
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, new Vector2(50+250,250), _2Rectangle, Color.White, 0, new Vector2(0, 0), 10f, SpriteEffects.None, 0);
            spriteBatch.Draw(_texture, new Vector2(50 + (10*10) + 250, 250), _DRectangle, Color.White, 0, new Vector2(0, 0), 10f, SpriteEffects.None, 0);
        }
    }
}
