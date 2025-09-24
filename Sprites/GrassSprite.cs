using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Parkour2D360.Collisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkour2D360.Sprites
{
    public class GrassSprite
    {
        private const string SPRITE_TEXTURE_FOLDER = "SpriteTextures/StickFigureCharacterSprites2D/Extras/";
        private const int SPRITE_WIDTH = 770;
        private const int SPRITE_HEIGHT = 167;

        private Texture2D _texture;
        private Vector2 _position;
        private BoundingRectangle _hitbox;
        
        public BoundingRectangle Hitbox => _hitbox;

        public GrassSprite()
        {
            _position = new Vector2(0, Constants.SCREEN_HEIGHT - SPRITE_HEIGHT);
            _hitbox = new BoundingRectangle(_position.X, _position.Y+20, Constants.SCREEN_WIDTH, SPRITE_HEIGHT);
        }

        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>($"{SPRITE_TEXTURE_FOLDER}ground");
        }

        public void Update(GameTime gametime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for(int i = 0; i < Constants.SCREEN_WIDTH; i += SPRITE_WIDTH-2)
            {
                spriteBatch.Draw(_texture, new Vector2(i, Constants.SCREEN_HEIGHT - SPRITE_HEIGHT), null, Color.Green, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
            }
        }
    }
}
