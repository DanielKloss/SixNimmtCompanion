using Windows.Storage;

namespace SixNimmtCompanion.UI
{
    public class SettingsService
    {
        private const string _gameLength = "gameLength";
        private const string _gameType = "gameType";

        public string GetGameLength()
        {
            return ApplicationData.Current.LocalSettings.Values[_gameLength]?.ToString() ?? "66";
        }

        public void SetGameLength(string gameLength)
        {
            ApplicationData.Current.LocalSettings.Values[_gameLength] = gameLength;
        }

        public string GetGameType()
        {
            return ApplicationData.Current.LocalSettings.Values[_gameType]?.ToString() ?? "true";
        }

        public void SetGameType(string gameType)
        {
            ApplicationData.Current.LocalSettings.Values[_gameType] = gameType;
        }
    }
}
