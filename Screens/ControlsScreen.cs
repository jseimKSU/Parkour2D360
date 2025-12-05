using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkour2D360.Screens
{
    public class ControlsScreen : TextMenuScreen
    {
        public ControlsScreen()
            : base("Controls")
        {
            MenuEntry moveMenuEntry = new MenuEntry("Move ----- AWSD or Left Thumbstick");
            MenuEntry jumpMenuEntry = new MenuEntry("Jump --------------------- Space or A");
            MenuEntry rotateLevelMenuEntry = new MenuEntry("Rotate Level ------ Arrows or D-Pad");
            MenuEntry pauseScreenMenuEntry = new MenuEntry("Pause Game -------- Escape or Back");

            MenuEntries.Add(moveMenuEntry);
            MenuEntries.Add(jumpMenuEntry);
            MenuEntries.Add(rotateLevelMenuEntry);
            MenuEntries.Add(pauseScreenMenuEntry);
        }
    }
}
