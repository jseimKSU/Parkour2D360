﻿using System;
using Microsoft.Xna.Framework;

namespace Parkour2D360.Screens
{
    // Custom event argument which includes the index of the player who
    // triggered the event. This is used by the MenuEntry.Selected event.
    public class PlayerIndexEventArgs : EventArgs
    {
        public PlayerIndex PlayerIndex { get; }

        public PlayerIndexEventArgs(PlayerIndex playerIndex)
        {
            PlayerIndex = playerIndex;
        }
    }
}
