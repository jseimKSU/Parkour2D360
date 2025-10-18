using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Parkour2D360.Settings;
using Parkour2D360.StateManagment;

namespace Parkour2D360.Screens
{
    public class MenuScreen : GameScreen
    {
        protected readonly string _menuTitle;

        protected InputAction _menuUp;
        protected InputAction _menuDown;
        protected InputAction _menuSelect;
        protected InputAction _menuCancel;

        public MenuScreen(string menuTitle)
        {
            _menuTitle = menuTitle;
        }

        public override void Activate()
        {
            base.Activate();

            bool movementOnAWSD = (
                ScreenManager.Settings.KeyboardOptions == KeyboardOptions.MovementOnAWSD
            );

            _menuUp = new InputAction(
                new[] { Buttons.DPadUp, Buttons.LeftThumbstickUp },
                new[] { (movementOnAWSD) ? Keys.W : Keys.Up },
                true
            );
            _menuDown = new InputAction(
                new[] { Buttons.DPadDown, Buttons.LeftThumbstickDown },
                new[] { (movementOnAWSD) ? Keys.S : Keys.Down },
                true
            );
            _menuSelect = new InputAction(
                new[] { Buttons.A, Buttons.Start },
                new[] { Keys.Enter, Keys.Space },
                true
            );
            _menuCancel = new InputAction(
                new[] { Buttons.B, Buttons.Back },
                new[] { Keys.Back, Keys.Escape },
                true
            );
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            base.HandleInput(gameTime, input);
        }

        protected virtual void OnCancel(PlayerIndex playerIndex)
        {
            ExitScreen();
        }

        // Helper overload makes it easy to use OnCancel as a MenuEntry event handler.
        protected void OnCancel(object sender, PlayerIndexEventArgs e)
        {
            OnCancel(e.PlayerIndex);
        }
    }
}
