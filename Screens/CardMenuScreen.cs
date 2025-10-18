using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Parkour2D360.StateManagment;

namespace Parkour2D360.Screens
{
    public class CardMenuScreen : MenuScreen
    {
        protected List<CardMenuEntry> _cardEntries = [];
        private int _selectedEntry;

        public CardMenuScreen(string menuTitle)
            : base(menuTitle)
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(1.5);
        }

        public override void Activate()
        {
            base.Activate();
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            base.HandleInput(gameTime, input);
        }

        protected virtual void OnSelectEntry(int entryIndex, PlayerIndex playerIndex) { }

        public override void Update(
            GameTime gameTime,
            bool otherScreenHasFocus,
            bool coveredByOtherScreen
        )
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
