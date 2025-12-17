using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parkour2D360.Saving;
using Parkour2D360.Screens.LevelScreens;

namespace Parkour2D360.Screens
{
    public class FailureScreen : TextMenuScreen
    {
        private int levelFailed;

        public FailureScreen(int levelFailed)
            : base("LMAO You died - Try Again?")
        {
            this.levelFailed = levelFailed;
            MenuEntry tryAgainEntry = new MenuEntry("Try Level Again?");
            MenuEntry exitToMainMenuEntry = new MenuEntry("Exit to Main Menu");
            MenuEntry quitGameEntry = new MenuEntry("Quit Game");

            tryAgainEntry.Selected += OnTryAgain;
            exitToMainMenuEntry.Selected += OnExitToMainMenu;
            quitGameEntry.Selected += OnQuitGame;

            MenuEntries.Add(tryAgainEntry);
            MenuEntries.Add(exitToMainMenuEntry);
            MenuEntries.Add(quitGameEntry);
        }

        private void OnTryAgain(object sender, PlayerIndexEventArgs e)
        {
            switch (levelFailed)
            {
                case 1:
                    ScreenManager.AddScreen(new Level1Screen(), ControllingPlayer);
                    break;
                case 2:
                    ScreenManager.AddScreen(new Level2Screen(), ControllingPlayer);
                    break;
                case 3:
                    ScreenManager.AddScreen(new Level3Screen(), ControllingPlayer);
                    break;
                default:
                    ScreenManager.AddScreen(new Level1Screen(), ControllingPlayer);
                    break;
            }
            ScreenManager.RemoveScreen(this);
        }

        private void OnExitToMainMenu(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, null, new TitleScreen());
        }

        private void OnQuitGame(object sender, PlayerIndexEventArgs e)
        {
            // SaveGame.Save();
            ScreenManager.Game.Exit();
        }
    }
}
