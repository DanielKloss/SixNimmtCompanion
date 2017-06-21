using SixNimmtCompanion.Data.Models;

namespace SixNimmtCompanion.Data.Repositories
{
    public class GameRepository : Repository
    {
        public void AddGame(Game gameToAdd)
        {
            connection.Insert(gameToAdd);
        }

        public int GetTotalGames()
        {
            return connection.Table<Game>().Count();
        }
    }
}
