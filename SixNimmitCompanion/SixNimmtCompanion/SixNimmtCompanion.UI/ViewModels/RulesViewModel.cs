using System.Collections.ObjectModel;
using SixNimmtCompanion.UI.Models;

namespace SixNimmtCompanion.UI.ViewModels
{
    public class RulesViewModel : BaseViewModel
    {
        private ObservableCollection<RulesCategory> _ruleCategories;
        public ObservableCollection<RulesCategory> ruleCategories
        {
            get { return _ruleCategories; }
            set
            {
                _ruleCategories = value;
                OnPropertyChanged(nameof(ruleCategories));
            }
        }

        public RulesViewModel()
        {
            ruleCategories = new ObservableCollection<RulesCategory>
            {
                new RulesCategory
                {
                    header = "Setup",
                    instructions = new ObservableCollection<string>
                    {
                        "Decide what type of game you'd like to play - points or rounds",
                        "Decide how many points or rounds you are playing to",
                        "Deal 10 cards to each player",
                        "Deal 4 cards face up to the table",
                        "Each of these cards is the first card in the rows that players will be adding to"
                    }
                },
                new RulesCategory
                {
                    header = "Gameplay",
                    instructions = new ObservableCollection<string>
                    {
                        "Players choose a card to play and simultaneously reveal them to the table",
                        "Players add their cards to a row, the player with the lowest value card goes first",
                        "When deciding which row to add their card to players must follow two rules - 'Ascending Order' and 'Small Difference'",
                        "Ascending Order - The number of the card that is added to a row must be higher than the number of the current last row",
                        "Small Difference - A card must always be added to the row with the smallest possible difference between the current last card and the new one",
                        "If a player can't abide by either of these two rules they must pick up a row of their choice",
                        "If a player plays the sixth card in a row they must pick up the entire row, leaving their played card as the first card in the new row",
                        "The player with the most bullheads at the end of the game is the loser"
                    }
                },
                new RulesCategory
                {
                    header = "Variants",
                    instructions = new ObservableCollection<string>
                    {
                        "TACTICS - All cards in play are known. Only play with cards up to 10 times the number of players",
                        "LOGIC - Players draft their hand of cards before each round",
                        "PROFESSIONAL - Players can add cards to the left OR right of each row. Left is in descending order, right is in ascending order. A player who could add to one row on the right and another on the left must choose the row with the smallest difference",
                    }
                }
            };
        }
    }
}
