using SQLite.Net.Attributes;

namespace SixNimmtCompanion.Data.Models
{
    public class PlayerGame : Entity
    {
        private int _id;
        [PrimaryKey, AutoIncrement]
        public int id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(id));
            }
        }

        private int _playerId;
        public int playerId
        {
            get { return _playerId; }
            set
            {
                _playerId = value;
                OnPropertyChanged(nameof(playerId));
            }
        }

        private int _gameId;
        public int gameId
        {
            get { return _gameId; }
            set
            {
                _gameId = value;
                OnPropertyChanged(nameof(gameId));
            }
        }

        private int _totalScore;
        public int totalScore
        {
            get { return _totalScore; }
            set
            {
                _totalScore = value;
                OnPropertyChanged(nameof(totalScore));
            }
        }

        private bool _isWinner;
        public bool isWinner
        {
            get { return _isWinner; }
            set
            {
                _isWinner = value;
                OnPropertyChanged(nameof(isWinner));
            }
        }

        private bool _isLoser;
        public bool isLoser
        {
            get { return _isLoser; }
            set
            {
                _isLoser = value;
                OnPropertyChanged(nameof(isLoser));
            }
        }
    }
}
