using System;
using Microsoft.Xna.Framework;

namespace Parkour2D360.Screens
{
    public class CardMenuEntry
    {
        public string Title { get; set; }

        public Vector2 Position { get; set; }

        public event EventHandler<PlayerIndexEventArgs> Selected;

        protected internal virtual void OnSelectEntry(PlayerIndex playerIndex)
        {
            Selected?.Invoke(this, new PlayerIndexEventArgs(playerIndex));
        }

        public CardMenuEntry(string title, Vector2 position)
        {
            Title = title;
            Position = position;
        }

        public virtual void Update(bool isSelected, GameTime gameTime) { }

        public virtual void Draw(bool isSelected, GameTime gameTime) { }
    }
}
