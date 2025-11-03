using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Parkour2D360
{
    public class Tilemap
    {
        public int MapWidth { get; init; }

        public int MapHeight { get; init; }

        public int TileWidth { get; init; }

        public int TileHeight { get; init; }

        public Texture2D TilesetTexture { get; init; }

        public Rectangle[] Tiles { get; init; }

        public int[] TileIndices { get; init; }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int y = 0; y < MapHeight; y++)
            {
                for (int x = 0; x < MapWidth; x++)
                {
                    int temp = (y * MapWidth) + x;
                    int index = TileIndices[temp];

                    if (index == -1)
                        continue;

                    spriteBatch.Draw(
                        TilesetTexture,
                        new Rectangle(x * TileWidth, y * TileHeight, TileWidth, TileHeight),
                        Tiles[index],
                        Color.White
                    );
                }
            }
        }
    }
}
