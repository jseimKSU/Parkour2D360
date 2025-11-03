#nullable enable
using System;
using System.IO;
using System.Text.Json;

namespace Parkour2D360.Saving
{
    public static class SaveGame
    {
        private static string SavePath =>
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "MyParkourSave",
                "save.json"
            );

        public static void Save(GameState gameState)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(SavePath)!);
            JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
            string saveJson = JsonSerializer.Serialize(gameState, options);
            File.WriteAllText(SavePath, saveJson);
        }

        public static GameState? Load()
        {
            if (!File.Exists(SavePath))
                return null;
            string json = File.ReadAllText(SavePath);
            return JsonSerializer.Deserialize<GameState>(json);
        }
    }
}
