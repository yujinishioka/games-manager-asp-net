using Dapper;
using GamesManager.Helpers;
using GamesManager.Models;

namespace GamesManager.Repositories
{
    public interface IGameRepository {
        IEnumerable<GameModel> GetAllGames();
        GameModel? GetGameById(int id);
        void AddGame(GameModel game);
        void UpdateGame(GameModel game);
        void DeleteGame(int id);
        CompanyModel GetCompanyById(int id);
    };

    public class GameRepository: IGameRepository
    {
        private readonly DbContext _dbContext;

        public GameRepository(DbContext dbContext) {
            _dbContext = dbContext;
        }

        public IEnumerable<GameModel> GetAllGames()
        {
            using (var connection = _dbContext.CreateConnection())
            {
                string sql = "SELECT id, name, company_id, year FROM game";
                return connection.Query<GameModel>(sql).ToList();
            }
        }

        public GameModel? GetGameById(int id)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                string sql = "SELECT id, name, company_id, year FROM game WHERE id = @id";
                return connection.QueryFirstOrDefault<GameModel>(sql, new { id });
            }
        }

        public void AddGame(GameModel game)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                string sql = "INSERT INTO game (name, company_id, year) VALUES (@name, @company_id, @year)";
                connection.Execute(sql, game);
            }
        }

        public void UpdateGame(GameModel game)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                string sql = "UPDATE game SET name = @name, company_id = @company_id, year = @year WHERE id = @id";
                connection.Execute(sql, game);
            }
        }

        public void DeleteGame(int id)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                string sql = "DELETE FROM game WHERE id = @id";
                connection.Execute(sql, new { id });
            }
        }

        public CompanyModel GetCompanyById(int id) {
            using (var connection = _dbContext.CreateConnection())
            {
                string sql = "SELECT id, name, fundation, country FROM company WHERE id = @id";
                return connection.QueryFirstOrDefault<CompanyModel>(sql, new { id });
            }
        }
    }
}
