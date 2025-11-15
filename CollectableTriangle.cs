using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Parkour2D360.Collisions;
using System.Collections.Generic;

namespace Parkour2D360
{
    public class CollectableTriangle
    {
        VertexPositionTexture[] vertices;

        private Vector3 position;
        private float scale;

        public BoundingRectangle Hitbox { get; set; }

        Texture2D texture;

        BasicEffect effect;

        Game game;

        Color color;

        public bool isCollideable;

        public bool isCollected = false;

        public List<CollectableTriangle> relatedCollectables = new List<CollectableTriangle>();

        public CollectableTriangle(Game game, Vector2 position, float scale, bool isCollideable)
        {
            this.game = game;
            this.position = new Vector3(position.X, position.Y, -1f);
            this.scale = scale;
            this.isCollideable = isCollideable;
            Hitbox = new BoundingRectangle(position.X-25, position.Y-25, 25, 25);
            color = isCollideable ? Constants.COLLIDABLE_COLLECTABLE_COLOR : Constants.NON_COLLIDABLE_COLLECTABLE_COLOR;

            texture = game.Content.Load<Texture2D>("tempTriangle");

            InitializeVertices();
            InitializeEffect();
        }

        private void InitializeVertices()
        {
            vertices = new VertexPositionTexture[3];
            vertices[0].Position = new Vector3(0, 0, 0);
            vertices[0].TextureCoordinate = new Vector2(.5f, 0f);

            vertices[1].Position = new Vector3(-25, 50, -10);
            vertices[1].TextureCoordinate = new Vector2(0f, 1f);

            vertices[2].Position = new Vector3(25, 50, -10);
            vertices[2].TextureCoordinate = new Vector2(1f,1f);
        }

        private void InitializeEffect()
        {
            effect = new BasicEffect(game.GraphicsDevice);
            effect.TextureEnabled = true;
            effect.Texture = texture;
            effect.VertexColorEnabled = false;
            effect.World = Matrix.Identity;
            effect.DiffuseColor = color.ToVector3();
            effect.Alpha = 1f;
            effect.LightingEnabled = false;
            effect.View = Matrix.CreateLookAt(
                new Vector3(0, 0, 10), // The camera position
                new Vector3(0, 0, 0), // The camera target,
                Vector3.Up // The camera up vector
            );
            Viewport vp = game.GraphicsDevice.Viewport;
            effect.Projection = Matrix.CreateOrthographicOffCenter(
                0,
                vp.Width,
                vp.Height,
                0,
                .1f,
                1000f
            );
        }

        public void Collect()
        {
            isCollected = true;
            foreach (CollectableTriangle collectable in relatedCollectables)
            {
                collectable.isCollected = true;
            }
        }

        public void Update(GameTime gameTime)
        {
            float angle = (float)gameTime.TotalGameTime.TotalSeconds;

            effect.World =
                Matrix.CreateRotationY(angle)
                * Matrix.CreateScale(scale)
                * Matrix.CreateTranslation(position);
        }

        public void Draw()
        {
            RasterizerState oldState = game.GraphicsDevice.RasterizerState;
            game.GraphicsDevice.RasterizerState = RasterizerState.CullNone;
            effect.CurrentTechnique.Passes[0].Apply();
            game.GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>(
                PrimitiveType.TriangleList,
                vertices, // The vertex data
                0, // The first vertex to use
                1 // The number of triangles to draw
            );
            game.GraphicsDevice.RasterizerState = oldState;
        }
    }
}
