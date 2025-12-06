using Parkour2D360.Saving;
using Parkour2D360.Screens.LevelScreens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkour2D360.Screens
{
    public class FailureScreen : TextMenuScreen
    {
        private int levelFailed;
        public FailureScreen(int levelFailed) : base("LMAO You died - Try Again?")
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
            switch(levelFailed)
            {
                case 1:
                    LoadingScreen.Load(ScreenManager, true, ControllingPlayer, new Level1Screen());
                    break;
                case 2:
                    LoadingScreen.Load(ScreenManager, true, ControllingPlayer, new Level2Screen());
                    break;
                case 3:
                    LoadingScreen.Load(ScreenManager, true, ControllingPlayer, new Level3Screen());
                    break;
                default:
                    LoadingScreen.Load(ScreenManager, false, e.PlayerIndex, new TitleScreen());
                    break;
            }
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
