namespace SixNimmtCompanion.UI.Models
{
    public class OverallStat : TrackPropertyChanged
    {
        private int _mostWins;
        public int mostWins
        {
            get { return _mostWins; }
            set
            {
                _mostWins = value;
                OnPropertyChanged(nameof(mostWins));
            }
        }

        private string _mostWinsPlayers;
        public string mostWinsPlayers
        {
            get { return _mostWinsPlayers; }
            set
            {
                _mostWinsPlayers = value;
                OnPropertyChanged(nameof(mostWinsPlayers));
            }
        }

        private int _mostLoses;
        public int mostLoses
        {
            get { return _mostLoses; }
            set
            {
                _mostLoses = value;
                OnPropertyChanged(nameof(mostLoses));
            }
        }

        private string _mostLosesPlayers;
        public string mostLosesPlayers
        {
            get { return _mostLosesPlayers; }
            set
            {
                _mostLosesPlayers = value;
                OnPropertyChanged(nameof(mostLosesPlayers));
            }
        }

        private int _bestScore;
        public int bestScore
        {
            get { return _bestScore; }
            set
            {
                _bestScore = value;
                OnPropertyChanged(nameof(bestScore));
            }
        }

        private string _bestScorePlayers;
        public string bestScorePlayers
        {
            get { return _bestScorePlayers; }
            set
            {
                _bestScorePlayers = value;
                OnPropertyChanged(nameof(bestScorePlayers));
            }
        }

        private int _worstScore;
        public int worstScore
        {
            get { return _worstScore; }
            set
            {
                _worstScore = value;
                OnPropertyChanged(nameof(worstScore));
            }
        }

        private string _worstScorePlayers;
        public string worstScorePlayers
        {
            get { return _worstScorePlayers; }
            set
            {
                _worstScorePlayers = value;
                OnPropertyChanged(nameof(worstScorePlayers));
            }
        }
    }
}
