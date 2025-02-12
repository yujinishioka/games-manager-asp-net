using System.Diagnostics;
using GamesManager.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace GamesManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public static void Data() {
            string connectionString = "postgresql://neondb_owner:npg_zRgeOqd0C7Vc@ep-dawn-unit-a5panjxz-pooler.us-east-2.aws.neon.tech/neondb?sslmode=require";

            using var conn = new NpgsqlConnection(connectionString);
            try
            {
                conn.Open();
                Console.WriteLine("Conexão bem-sucedida!");
                Console.WriteLine(conn);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
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
