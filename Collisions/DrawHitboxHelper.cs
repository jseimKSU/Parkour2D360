using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Parkour2D360.Collisions
{
    public static class DrawHitboxHelper
    {
        public static void DrawHitbox(
            SpriteBatch spriteBatch,
            BoundingRectangle hitbox,
            Texture2D texture
        )
        {
            int x = (int)hitbox.X;
            int y = (int)hitbox.Y;
            int width = (int)hitbox.Width;
            int height = (int)hitbox.Height;

            // Top
            spriteBatch.Draw(texture, new Rectangle(x, y, width, 2), Color.Red);
            // Left
            spriteBatch.Draw(texture, new Rectangle(x, y, 2, height), Color.Red);
            // Right
            spriteBatch.Draw(texture, new Rectangle(x + width - 2, y, 2, height), Color.Red);
            // Bottom
            spriteBatch.Draw(texture, new Rectangle(x, y + height - 2, width, 2), Color.Red);
        }
    }
}
