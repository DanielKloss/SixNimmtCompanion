using SixNimmtCompanion.UI.ViewModels;

namespace SixNimmtCompanion.UI.Dialogs
{
    public class GameOptionsDialogViewModel : BaseViewModel
    {
        private string _title;
        public string title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged(nameof(title));
            }
        }

        private int _gameLength;
        public int gameLength
        {
            get { return _gameLength; }
            set
            {
                _gameLength = value;
                OnPropertyChanged(nameof(gameLength));
            }
        }

        private bool _gameType;
        public bool gameType
        {
            get { return _gameType; }
            set
            {
                _gameType = value;
                OnPropertyChanged(nameof(gameType));
            }
        }

        public GameOptionsDialogViewModel(string Title, int GameLength, bool GameType)
        {
            title = Title;
            gameLength = GameLength;
            gameType = GameType;
        }
    }
}
