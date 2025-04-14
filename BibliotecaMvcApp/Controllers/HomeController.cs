// Importa namespaces necess�rios para funcionamento do controller e MVC
using System.Diagnostics;                     // Usado para capturar o ID da requisi��o na a��o Error
using BibliotecaMvcApp.Models;                // Importa a classe ErrorViewModel
using Microsoft.AspNetCore.Mvc;               // Cont�m os atributos e classes do MVC, como Controller e IActionResult

namespace BibliotecaMvcApp.Controllers        // Define o namespace do projeto
{
    // Controlador principal da aplica��o � padr�o para tela inicial
    public class HomeController : Controller
    {
        // Inje��o de depend�ncia para registrar logs (�til em produ��o ou para depurar erros)
        private readonly ILogger<HomeController> _logger;

        // Construtor do controller com inje��o de logger
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // A��o padr�o (rota: /Home/Index)
        public IActionResult Index()
        {
            return View(); // Retorna a View Index.cshtml localizada em Views/Home/
        }

        // A��o da rota /Home/Privacy
        public IActionResult Privacy()
        {
            return View(); // Retorna a View Privacy.cshtml localizada em Views/Home/
        }

        // A��o para lidar com erros
        // ResponseCache evita o cache da resposta para garantir que sempre receba a mensagem atual
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Retorna a View de erro passando um modelo com o ID da requisi��o atual
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
