using System.Text.Json;
using Parkour2D360.SettingsFolder;

namespace Parkour2D360.Saving
{
    public class GameState
    {
        public int HighestLevelCompleted { get; set; }

        public Settings CurrentSettings { get; set; }
    }
}
