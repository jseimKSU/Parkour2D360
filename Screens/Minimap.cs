using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Parkour2D360.Screens
{
    public class Minimap
    {
        protected Rectangle _position = new Rectangle(10, 10, 192, 192);

        protected int _currentSide = 0;

        protected Texture2D _platformTexture;

        public Minimap(Texture2D platformTexture)
        {
            _platformTexture = platformTexture;
        }

        public void Update() { }

        public void Draw(SpriteBatch spriteBatch)
        {
            float angle = MathHelper.ToRadians(90f * _currentSide);
            Vector2 origin = new Vector2(
                _position.X + _position.Width * 0.5f,
                _position.Y + _position.Height * 0.5f
            );

            Matrix transform =
                Matrix.CreateTranslation(-origin.X, -origin.Y, 0f)
                * Matrix.CreateRotationZ(angle)
                * Matrix.CreateTranslation(origin.X, origin.Y, 0f);

            spriteBatch.Begin(transformMatrix: transform); // need animation for it spinning

            spriteBatch.Draw(_platformTexture, _position, Color.Black);
            spriteBatch.Draw(_platformTexture, new Rectangle(15, 15, 10, 10), Color.White);

            spriteBatch.End();
        }

        /// <summary>
        /// Will update the proper variables so next time it is drawn it is rotated
        /// </summary>
        /// <param name="direction">1 for right and -1 for left</param>
        public void RotateMinimap(int direction)
        {
            if (_currentSide == 0 && direction < 0)
            {
                _currentSide = 3;
            }
            else if (_currentSide == 3 && direction > 0)
            {
                _currentSide = 0;
            }
            else
                _currentSide += direction;
        }
    }
}
