using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Parkour2D360.Screens
{
    public static class DrawGameScreenExitInstructions
    {

        private static SpriteFont _exitInstructionsFont;
        private static ContentManager _content;

        private static void LoadContent()
        {
            _exitInstructionsFont = _content.Load<SpriteFont>("OrbitronSmall");
        }

        public static void DrawExitInstructions(ContentManager content, SpriteBatch spriteBatch, bool currentInputIsKeyboard)
        {
            _content = content;
            LoadContent();

            ((string exitCombo, string simpleExit), bool isKeyboard) exitInstructions = (currentInputIsKeyboard) ?
                ((Constants.EXIT_COMBO_STRING_KEYBOARD, Constants.EXIT_SIMPLE_KEYBOARD), true) :
            ((Constants.EXIT_COMBO_STRING_GAMEPAD, Constants.EXIT_SIMPLE_GAMEPAD), false);

            spriteBatch.DrawString
                (
                    _exitInstructionsFont, 
                    $"Do {exitInstructions.Item1.exitCombo} to Exit or {exitInstructions.Item1.simpleExit}", 
                    (exitInstructions.isKeyboard) ? 
                    new Vector2(1635, 20) : 
                    new Vector2(1380, 20), Color.Black
                );
        }
    }
}
