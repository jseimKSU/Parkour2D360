using Parkour2D360.Saving;

namespace Parkour2D360.Screens
{
    // The pause menu comes up over the top of the game,
    // giving the player options to resume or quit.
    public class PauseMenuScreen : TextMenuScreen
    {
        public PauseMenuScreen()
            : base("Paused")
        {
            MenuEntry resumeGameMenuEntry = new MenuEntry("Resume Game");
            MenuEntry levelSelectGameMenuEntry = new MenuEntry("Level Select"); // add when level select screen is started
            MenuEntry settingsGameMenuEntry = new MenuEntry("Settings"); // need to fully implement
            MenuEntry returnToMainMenuEntry = new MenuEntry("Return To Main Menu");
            MenuEntry quitGameMenuEntry = new MenuEntry("Quit Game");

            resumeGameMenuEntry.Selected += OnCancel;
            settingsGameMenuEntry.Selected += OnSettings;
            returnToMainMenuEntry.Selected += ReturnToMainMenuSelected;
            quitGameMenuEntry.Selected += QuitGameMenuEntrySelected;

            MenuEntries.Add(resumeGameMenuEntry);
            MenuEntries.Add(settingsGameMenuEntry);
            MenuEntries.Add(returnToMainMenuEntry);
            MenuEntries.Add(quitGameMenuEntry);
        }

        private void QuitGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            GameState state = new GameState
            {
                HighestLevelCompleted = 1,
                CurrentSettings = ScreenManager.Settings,
            };
            SaveGame.Save(state);
            ScreenManager.Game.Exit();
        }

        private void ReturnToMainMenuSelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, null, new TitleScreen());
        }
    }
}
