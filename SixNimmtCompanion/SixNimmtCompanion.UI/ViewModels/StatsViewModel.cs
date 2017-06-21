using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using SixNimmtCompanion.Data.Models;
using SixNimmtCompanion.Data.Repositories;
using SixNimmtCompanion.UI.Models;
using SixNimmtCompanion.UI.Views;
using Windows.UI.Xaml;
using MvvmDialogs;
using SixNimmtCompanion.UI.Dialogs;

namespace SixNimmtCompanion.UI.ViewModels
{
    public class StatsViewModel : BaseViewModel
    {
        private IDialogService _dialogService;
        private GameRepository _gameRepo;
        private PlayerRepository _playerRepo;
        private AchievementRepository _achievementRepo;

        private OneDriveService _oneDriveService;
        public OneDriveService oneDriveService
        {
            get { return _oneDriveService; }
            set
            {
                _oneDriveService = value;
                OnPropertyChanged(nameof(oneDriveService));
            }
        }

        private int _totalGames;
        public int totalGames
        {
            get { return _totalGames; }
            set
            {
                _totalGames = value;
                OnPropertyChanged(nameof(totalGames));
            }
        }

        private ObservableCollection<PlayerStat> _playerStats;
        public ObservableCollection<PlayerStat> playerStats
        {
            get { return _playerStats; }
            set
            {
                _playerStats = value;
                OnPropertyChanged(nameof(playerStats));
            }
        }

        private OverallStat _overallStats;
        public OverallStat overallStats
        {
            get { return _overallStats; }
            set
            {
                _overallStats = value;
                OnPropertyChanged(nameof(overallStats));
            }
        }

        private ICommand _backupCommand;
        public ICommand backupCommand
        {
            get
            {
                if (_backupCommand == null)
                {
                    _backupCommand = new Command(Backup);
                }
                return _backupCommand;
            }
            set { _backupCommand = value; }
        }

        private ICommand _restoreCommand;
        public ICommand restoreCommand
        {
            get
            {
                if (_restoreCommand == null)
                {
                    _restoreCommand = new Command(Restore);
                }
                return _restoreCommand;
            }
            set { _restoreCommand = value; }
        }

        private ICommand _navigateToPlayerStatCommand;
        public ICommand navigateToPlayerStatCommand
        {
            get
            {
                if (_navigateToPlayerStatCommand == null)
                {
                    _navigateToPlayerStatCommand = new Command<PlayerStat>(stat => ((App)Application.Current).rootFrame.Navigate(typeof(PlayerStatsView), stat));
                }
                return _navigateToPlayerStatCommand;
            }
            set { _navigateToPlayerStatCommand = value; }
        }

        public StatsViewModel()
        {
            _dialogService = new DialogService();
            oneDriveService = new OneDriveService();

            Setup();
        }

        public void Setup()
        {
            _gameRepo = new GameRepository();
            _playerRepo = new PlayerRepository();
            _achievementRepo = new AchievementRepository();

            totalGames = _gameRepo.GetTotalGames();

            GetPlayersData();
            GetOverallData();
        }

        private void GetOverallData()
        {
            overallStats = new OverallStat();

            if (playerStats.Count > 0)
            {
                overallStats.mostWins = playerStats.OrderByDescending(p => p.numberOfWins).First().numberOfWins;
                overallStats.mostWinsPlayers = ConstructListOfPlayers(playerStats.Where(p => p.numberOfWins == overallStats.mostWins).ToList());

                overallStats.mostLoses = playerStats.OrderByDescending(p => p.numberOfLoses).First().numberOfLoses;
                overallStats.mostLosesPlayers = ConstructListOfPlayers(playerStats.Where(p => p.numberOfLoses == overallStats.mostLoses).ToList());

                overallStats.bestScore = playerStats.OrderByDescending(p => p.bestScore).First().bestScore;
                overallStats.bestScorePlayers = ConstructListOfPlayers(playerStats.Where(p => p.bestScore == overallStats.bestScore).ToList());

                overallStats.worstScore = playerStats.OrderBy(p => p.worstScore).First().worstScore;
                overallStats.worstScorePlayers = ConstructListOfPlayers(playerStats.Where(p => p.worstScore == overallStats.worstScore).ToList());
            }
        }

        private async void GetPlayersData()
        {
            playerStats = new ObservableCollection<PlayerStat>();

            try
            {
                foreach (Player player in _playerRepo.GetAllPlayers())
                {
                    playerStats.Add(new PlayerStat
                    {
                        name = player.name,
                        numberOfWins = _playerRepo.GetNumberOfWinsById(player.id),
                        numberOfLoses = _playerRepo.GetNumberOfLosesById(player.id),
                        bestScore = _playerRepo.GetBestScoreById(player.id),
                        worstScore = _playerRepo.GetWorstScoreById(player.id),
                        numberOfGames = _playerRepo.GetNumberOfGamesById(player.id),
                        averageScore = _playerRepo.GetAverageScoreById(player.id),
                        achievements = new List<Achievement>(_achievementRepo.GetAchievementsById(player.id))
                    });
                }
            }
            catch (SQLiteException)
            {
                await _dialogService.ShowContentDialogAsync(new MessageDialogViewModel("Error", "Something went wrong, please try again"));
            }

            foreach (PlayerStat player in playerStats)
            {
                if (player.numberOfGames == 0)
                {
                    player.winPercentage = 0;
                }
                else
                {
                    player.winPercentage = Math.Round((Convert.ToDouble(player.numberOfWins) / Convert.ToDouble(player.numberOfGames)) * 100, 0);
                }
            }

            playerStats = new ObservableCollection<PlayerStat>(playerStats.OrderByDescending(p => p.winPercentage));
        }

        private async void Backup()
        {
            oneDriveService.isWorking = true;
            oneDriveService.backingUp = true;

            await oneDriveService.BackUp();

            oneDriveService.isWorking = false;
            oneDriveService.backingUp = false;
        }

        private async void Restore()
        {
            oneDriveService.isWorking = true;
            oneDriveService.restoring = true;

            await oneDriveService.Restore();
            Setup();

            oneDriveService.isWorking = false;
            oneDriveService.restoring = false;
        }


        private string ConstructListOfPlayers(List<PlayerStat> list)
        {
            string playersString = "(";

            for (int i = 0; i < list.Count(); i++)
            {
                if (i == 0)
                {
                    playersString += list[i].name;
                }
                else
                {
                    playersString += (", " + list[i].name);
                }
            }

            playersString += ")";

            return playersString;
        }
    }
}
