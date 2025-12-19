using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Parkour2D360
{
    public static class Constants
    {
        public const int SCREEN_WIDTH = 1920;
        public const int SCREEN_HEIGHT = 1080;
        public const string EXIT_COMBO_STRING_GAMEPAD =
            "Left Bumper + Right Bumper + A + B + X + Y + Left Stick";
        public const string EXIT_COMBO_STRING_KEYBOARD = "Q + W + E + R + T + Y";
        public const string EXIT_SIMPLE_GAMEPAD = "Back";
        public const string EXIT_SIMPLE_KEYBOARD = "Esc";

        public static Color NON_JUMPABLE_COLLIDABLE_COLOR = Color.DarkRed;
        public static Color NON_JUMPABLE_NON_COLLIDABLE_COLOR = Color.PaleVioletRed;
        public static Color JUMPABLE_COLLIDABLE_COLOR = Color.DarkGreen;
        public static Color JUMPABLE_NON_COLLIDABLE_COLOR = Color.LightGreen;
        public static Color COLLIDABLE_COLLECTABLE_COLOR = Color.DarkBlue;
        public static Color NON_COLLIDABLE_COLLECTABLE_COLOR = Color.LightBlue;
    }
}
