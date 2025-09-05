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
        private int _viewWidth;
        private int _viewHeight;
        
        public BoundingRectangle Hitbox => _hitbox;

        public GrassSprite(int viewWidth, int viewHeight)
        {
            _viewWidth = viewWidth;
            _viewHeight = viewHeight;
            _position = new Vector2(0, _viewHeight - SPRITE_HEIGHT);
            _hitbox = new BoundingRectangle(_position, _viewWidth, SPRITE_HEIGHT);
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
            for(int i = 0; i < _viewWidth; i += SPRITE_WIDTH-2)
            {
                spriteBatch.Draw(_texture, new Vector2(i, _viewHeight - SPRITE_HEIGHT), null, Color.Green, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
            }
        }
    }
}
