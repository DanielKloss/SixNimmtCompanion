using SixNimmtCompanion.Data.Models;

namespace SixNimmtCompanion.Data.Repositories
{
    public class PlayerGameRepository : Repository
    {
        public void AddPlayerGame(Player player, int GameId)
        {
            connection.Insert(new PlayerGame
            {
                gameId = GameId,
                playerId = player.id,
                isLoser = player.isLoser,
                isWinner = player.isWinner,
                totalScore = player.totalScore
            });
        }

        public int? GetWorstEverScore()
        {
            var scores = from playergame in connection.Table<PlayerGame>()
                         orderby playergame.totalScore ascending
                         select playergame;

            return scores.FirstOrDefault()?.totalScore;
        }

        public int? GetBestEverScore()
        {
            var scores = from playergame in connection.Table<PlayerGame>()
                         orderby playergame.totalScore descending
                         select playergame;

            return scores.FirstOrDefault()?.totalScore;
        }
    }
}
