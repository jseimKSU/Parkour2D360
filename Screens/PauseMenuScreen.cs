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
            MenuEntry quitGameMenuEntry = new MenuEntry("Quit Game");

            resumeGameMenuEntry.Selected += OnCancel;
            settingsGameMenuEntry.Selected += OnSettings;
            quitGameMenuEntry.Selected += QuitGameMenuEntrySelected;

            MenuEntries.Add(resumeGameMenuEntry);
            MenuEntries.Add(settingsGameMenuEntry);
            MenuEntries.Add(quitGameMenuEntry);
        }

        private void QuitGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
            LoadingScreen.Load(ScreenManager, false, null, new TitleScreen());
        }
    }
}
