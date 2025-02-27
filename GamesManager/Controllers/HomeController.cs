using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GamesManager.Models;
using GamesManager.Repositories;
using GamesManager.ViewModels;

namespace GamesManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGameRepository _gameRepository;

        public HomeController(ILogger<HomeController> logger, IGameRepository gameRepository) {
            _logger = logger;
            _gameRepository = gameRepository;
        }

        public IActionResult Index()
        {
            try
            {
                var lista = new List<GameViewModel>();
                var games = _gameRepository.GetAllGames();
                foreach (var game in games)
                {
                    var gameViewModel = new GameViewModel();
                    gameViewModel.Game = game;
                    gameViewModel.Company = _gameRepository.GetCompanyById(game.company_id);
                    lista.Add(gameViewModel);
                }
                return View(lista);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro na busca de jogos - {ex.Message}");
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
