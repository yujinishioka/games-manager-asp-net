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
                string sql = @"
                    SELECT
                        g.id, g.name, g.company_id, g.year, g.status,
                        ge.id, ge.name, ge.style
                    FROM game g
                    LEFT JOIN game_genre gg ON g.id = gg.id_game
                    LEFT JOIN genre ge ON gg.id_genre = ge.id
                ";

                var gameDictionary = new Dictionary<int, GameModel>();

                var games = connection.Query<GameModel, GenreModel, GameModel>(
                    sql,
                    (game, genre) =>
                    {
                        if (!gameDictionary.TryGetValue(game.id, out var existingGame))
                        {
                            existingGame = game;
                            existingGame.genres = new List<GenreModel>();
                            gameDictionary.Add(game.id, existingGame);
                        }

                        if (genre != null)
                        {
                            existingGame.genres.Add(genre);
                        }

                        return existingGame;
                    },
                    splitOn: "id, id")
                .Distinct()
                .ToList();

                return games;
            }
        }

        public GameModel? GetGameById(int id)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                string sql = "SELECT id, name, company_id, year, \"status\" FROM game WHERE id = @id";
                return connection.QueryFirstOrDefault<GameModel>(sql, new { id });
            }
        }

        public void AddGame(GameModel game)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                string sql = "INSERT INTO game (name, company_id, year, \"status\") VALUES (@name, @company_id, @year, @status)";
                connection.Execute(sql, game);
            }
        }

        public void UpdateGame(GameModel game)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                string sql = "UPDATE game SET name = @name, company_id = @company_id, year = @year, \"status\" = @status WHERE id = @id";
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
