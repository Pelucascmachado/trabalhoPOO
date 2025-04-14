// Importa namespaces necessários para funcionamento do controller e MVC
using System.Diagnostics;                     // Usado para capturar o ID da requisição na ação Error
using BibliotecaMvcApp.Models;                // Importa a classe ErrorViewModel
using Microsoft.AspNetCore.Mvc;               // Contém os atributos e classes do MVC, como Controller e IActionResult

namespace BibliotecaMvcApp.Controllers        // Define o namespace do projeto
{
    // Controlador principal da aplicação – padrão para tela inicial
    public class HomeController : Controller
    {
        // Injeção de dependência para registrar logs (útil em produção ou para depurar erros)
        private readonly ILogger<HomeController> _logger;

        // Construtor do controller com injeção de logger
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Ação padrão (rota: /Home/Index)
        public IActionResult Index()
        {
            return View(); // Retorna a View Index.cshtml localizada em Views/Home/
        }

        // Ação da rota /Home/Privacy
        public IActionResult Privacy()
        {
            return View(); // Retorna a View Privacy.cshtml localizada em Views/Home/
        }

        // Ação para lidar com erros
        // ResponseCache evita o cache da resposta para garantir que sempre receba a mensagem atual
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Retorna a View de erro passando um modelo com o ID da requisição atual
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
