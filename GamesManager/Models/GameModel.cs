namespace GamesManager.Models
{
    public class GameModel
    {
        public required int id { get; set; }
        public required string name { get; set; }
        public required int company_id { get; set; }
        public List<GenreModel> genres { get; set; } = new List<GenreModel>();
        public string? year { get; set; }
        public required string status { get; set; }
    }
}
