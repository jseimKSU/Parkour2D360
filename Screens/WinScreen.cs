using Parkour2D360.Screens.LevelScreens;

namespace Parkour2D360.Screens
{
    public class WinScreen : TextMenuScreen
    {
        public WinScreen()
            : base("You Won!")
        {
            MenuEntry exitToMainMenuEntry = new MenuEntry("Exit to Main Menu");
            MenuEntry quitGameEntry = new MenuEntry("Quit Game");

            exitToMainMenuEntry.Selected += OnExitToMainMenu;
            quitGameEntry.Selected += OnQuitGame;

            MenuEntries.Add(exitToMainMenuEntry);
            MenuEntries.Add(quitGameEntry);
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
