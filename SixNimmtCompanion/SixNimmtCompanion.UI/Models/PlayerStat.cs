using System.Collections.Generic;
using SixNimmtCompanion.Data.Models;

namespace SixNimmtCompanion.UI.Models
{
    public class PlayerStat
    {
        public string name { get; set; }
        public int numberOfGames { get; set; }
        public int bestScore { get; set; }
        public int worstScore { get; set; }
        public int numberOfWins { get; set; }
        public int numberOfLoses { get; set; }
        public double winPercentage { get; set; }
        public double averageScore { get; set; }

        public List<Achievement> achievements { get; set; }
    }
}
