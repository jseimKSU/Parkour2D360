using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Parkour2D360.Screens
{
    public interface IGameScreen
    {
        public int GetId();
        public void Initialize();
        public void LoadContent(ContentManager content);
        public void Update(GameTime gameTime);
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}