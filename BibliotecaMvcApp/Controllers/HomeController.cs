// Importa namespaces necess�rios para funcionamento do controller e MVC
using System.Diagnostics;                     // Usado para capturar o ID da requisi��o na a��o Error
using BibliotecaMvcApp.Models;                // Importa a classe ErrorViewModel
using Microsoft.AspNetCore.Mvc;               // Cont�m os atributos e classes do MVC, como Controller e IActionResult

namespace BibliotecaMvcApp.Controllers        // Define o namespace do projeto
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
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
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
