namespace Parkour2D360.SettingsFolder
{
    public struct Settings
    {
        public KeyboardOptions KeyboardOptions { get; set; }

        public Settings()
        {
            KeyboardOptions = KeyboardOptions.MovementOnAWSD;
        }
    }
}
