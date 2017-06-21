using MvvmDialogs;
using SixNimmtCompanion.Data.Models;
using SixNimmtCompanion.Data.Repositories;
using SixNimmtCompanion.UI.Dialogs;
using SixNimmtCompanion.UI.Views;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SixNimmtCompanion.UI.ViewModels
{
    public class ScoreboardViewModel : BaseViewModel
    {
        private GameRepository _gameRepo;
        private PlayerGameRepository _playerGameRepo;
        private SettingsService _settingsService;
        private IDialogService _dialogService;

        private bool _newRound;
        public bool newRound
        {
            get { return _newRound; }
            set
            {
                _newRound = value;
                OnPropertyChanged(nameof(newRound));
            }
        }

        private Game _game;
        public Game game
        {
            get { return _game; }
            set
            {
                _game = value;
                OnPropertyChanged(nameof(game));
            }
        }

        private ICommand _nextRoundCommand;
        public ICommand nextRoundCommand
        {
            get
            {
                if (_nextRoundCommand == null)
                {
                    _nextRoundCommand = new Command(NextRound);
                }
                return _nextRoundCommand;
            }
            set { _nextRoundCommand = value; }
        }

        private ICommand _finishGameCommand;
        public ICommand finishGameCommand
        {
            get
            {
                if (_finishGameCommand == null)
                {
                    _finishGameCommand = new Command(() => SaveGame());
                }
                return _finishGameCommand;
            }
            set { _finishGameCommand = value; }
        }

        private ICommand _cancelGameCommand;
        public ICommand cancelGameCommand
        {
            get
            {
                if (_cancelGameCommand == null)
                {
                    _cancelGameCommand = new Command(EndGame);
                }
                return _cancelGameCommand;
            }
            set { _cancelGameCommand = value; }
        }

        public ScoreboardViewModel(IEnumerable<Player> Players)
        {
            _dialogService = new DialogService();
            _gameRepo = new GameRepository();
            _playerGameRepo = new PlayerGameRepository();
            _settingsService = new SettingsService();
            game = new Game();

            game.round = 1;
            game.dateTime = DateTime.Now;
            game.isPointsBased = Convert.ToBoolean(_settingsService.GetGameType());
            game.length = Convert.ToInt32(_settingsService.GetGameLength());
            game.players = new ObservableCollection<Player>(Players);

            foreach (Player player in game.players)
            {
                player.achievements = new ObservableCollection<Achievement>();

                if (game.isPointsBased)
                {
                    player.totalScore = game.length;
                }
                else
                {
                    player.totalScore = 66;
                }

                game.currentPlayer = game.players[0];
            }
        }

        private void NextRound()
        {
            newRound = true;

            foreach (Player player in game.players)
            {
                player.totalScore -= player.roundScore;
                player.roundScore = 0;
            }

            if (CheckForEndOfGame())
            {
                SaveGame();
            }
            else
            {
                game.round++;
            }
        }

        private bool CheckForEndOfGame()
        {
            if (game.isPointsBased)
            {
                foreach (Player player in game.players)
                {
                    if (player.totalScore <= 0)
                    {
                        return true;
                    }
                }

                return false;
            }
            else
            {
                if (game.round == game.length)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async void EndGame()
        {
            ContentDialogResult result = await _dialogService.ShowContentDialogAsync(new ConfirmDialogViewModel("End Game", "You are about to quit an unfinished game. Are you sure you want to quit the game without logging the stats?"));

            if (result == ContentDialogResult.Primary)
            {
                ((App)Application.Current).rootFrame.Navigate(typeof(MainMenuView));
            }
        }

        private async void SaveGame()
        {
            if (!CheckForEndOfGame())
            {
                ContentDialogResult result = await _dialogService.ShowContentDialogAsync(new ConfirmDialogViewModel("End Game", "You are about to quit an unfinished game. Are you sure you want to quit the game and log the stats?"));

                if (result == ContentDialogResult.Secondary)
                {
                    return;
                }
            }

            CalculateScores();

            AchievementService achievementService = new AchievementService(game);

            achievementService.CheckForPersonalScores();
            achievementService.CheckForEverScores();

            try
            {
                _gameRepo.AddGame(game);

                foreach (Player player in game.players)
                {
                    _playerGameRepo.AddPlayerGame(player, game.id);
                }

                achievementService.CheckForFirsts();
                achievementService.CheckForMileStones();

                achievementService.AddAchievements();

                ((App)Application.Current).rootFrame.Navigate(typeof(StandingsView), game);
            }
            catch (SQLiteException)
            {
                await _dialogService.ShowContentDialogAsync(new MessageDialogViewModel("Error", "Something went wrong, please try again"));
            }
        }

        public void CalculateScores()
        {
            double maxScore = game.players.Max<Player, double>(p => p.totalScore);

            if (game.players.Where(p => p.totalScore == maxScore).Count() == game.players.Count())
            {
                if (maxScore > 0)
                {
                    foreach (Player player in game.players)
                    {
                        player.isWinner = true;
                    }
                }
                else
                {
                    foreach (Player player in game.players)
                    {
                        player.isLoser = true;
                    }
                }
            }
            else
            {
                foreach (Player player in game.players.Where(p => p.totalScore == maxScore))
                {
                    player.isWinner = true;
                }

                double minScore = game.players.Min<Player, double>(p => p.totalScore);

                foreach (Player player in game.players.Where(p => p.totalScore == minScore))
                {
                    player.isLoser = true;
                }
            }
        }
    }
}
