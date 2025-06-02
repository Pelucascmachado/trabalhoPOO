using Microsoft.AspNetCore.Mvc;
using BibliotecaMvcApp.Models;
using BibliotecaMvcApp.Repository.IRepository;
using System.Net.Http;
using System.Text.Json;

namespace BibliotecaMvcApp.Controllers
{
    public class LivrosController : Controller
    {
        private readonly IBookRepository _livroRepository;
        private readonly IAuthorRepository _authorRepository;

        public LivrosController(IBookRepository livroRepository, IAuthorRepository authorRepository)
        {
            _livroRepository = livroRepository;
            _authorRepository = authorRepository;
        }

        // GET: /Livros
        public async Task<IActionResult> Index()
        {
            var livros = await _livroRepository.GetBooksWithAuthorsAsync();
            return View(livros);
        }

        // GET: /Livros/Details/{id}
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var livro = await _livroRepository.GetAsync(l => l.Id == id);
            if (livro == null)
                return NotFound();
            return View(livro);
        }

        // GET: /Livros/Create
        public async Task<IActionResult> Create()
        {
            var autores = await _authorRepository.GetAllAsync();
            ViewBag.Autores = autores;
            return View();
        }

        // POST: /Livros/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Livro livro, Guid[] autoresSelecionados)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Autores = await _authorRepository.GetAllAsync();
                return View(livro);
            }

            // Relacionamento N x N
            if (autoresSelecionados != null && autoresSelecionados.Length > 0)
            {
                livro.LivrosAutores = autoresSelecionados.Select(aid => new LivroAutor { AutorId = aid, LivroId = livro.Id }).ToList();
            }

            await _livroRepository.AddAsync(livro);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Livros/Edit/{id}
        public async Task<IActionResult> Edit(Guid id)
        {
            var livro = await _livroRepository.GetBookWithAuthorsAsync(id);
            if (livro == null)
                return NotFound();

            var autores = await _authorRepository.GetAllAsync();
            ViewBag.Autores = autores;
            ViewBag.AutoresSelecionados = livro.LivrosAutores.Select(la => la.AutorId).ToArray();

            return View(livro);
        }

        // POST: /Livros/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Livro livro, Guid[] autoresSelecionados)
        {
            if (id != livro.Id)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                ViewBag.Autores = await _authorRepository.GetAllAsync();
                ViewBag.AutoresSelecionados = autoresSelecionados;
                return View(livro);
            }

            // Carrega o livro original com autores
            var livroOriginal = await _livroRepository.GetBookWithAuthorsAsync(id);
            if (livroOriginal == null)
                return NotFound();

            // Remove todos os vínculos antigos
            livroOriginal.LivrosAutores.Clear();

            // Adiciona apenas os autores selecionados
            if (autoresSelecionados != null && autoresSelecionados.Length > 0)
            {
                foreach (var autorId in autoresSelecionados)
                {
                    livroOriginal.LivrosAutores.Add(new LivroAutor { AutorId = autorId, LivroId = livroOriginal.Id });
                }
            }

            // Atualiza os demais campos do livro
            livroOriginal.Titulo = livro.Titulo;
            livroOriginal.DataPublicacao = livro.DataPublicacao;
            livroOriginal.ISBN = livro.ISBN;
            livroOriginal.Editora = livro.Editora;
            livroOriginal.Genero = livro.Genero;
            livroOriginal.NumeroPaginas = livro.NumeroPaginas;
            livroOriginal.Preco = livro.Preco;
            livroOriginal.Idioma = livro.Idioma;
            livroOriginal.Sinopse = livro.Sinopse;

            await _livroRepository.UpdateAsync(livroOriginal);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Livros/Delete/{id}
        public async Task<IActionResult> Delete(Guid id)
        {
            var livro = await _livroRepository.GetBookWithAuthorsAsync(id);
            if (livro == null)
                return NotFound();
            return View(livro);
        }

        // POST: /Livros/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var livro = await _livroRepository.GetBookWithAuthorsAsync(id);
            if (livro == null)
                return NotFound();

            await _livroRepository.DeleteAsync(livro);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Livros/Info/{id}
        [HttpGet("Livros/Info/{id}")]
        public async Task<IActionResult> Info(Guid id)
        {
            var livro = await _livroRepository.GetAsync(l => l.Id == id);
            if (livro == null)
                return NotFound();

            var isbn = livro.ISBN;
            object bookData = null;

            if (!string.IsNullOrWhiteSpace(isbn))
            {
                var url = $"https://openlibrary.org/api/books?bibkeys=ISBN:{isbn}&format=json&jscmd=data";
                using (var http = new HttpClient())
                {
                    var response = await http.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var dict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        if (dict != null && dict.TryGetValue($"ISBN:{isbn}", out var bookElement))
                        {
                            bookData = bookElement;
                        }
                    }
                }
            }

            ViewBag.Livro = livro;
            ViewBag.BookData = bookData;
            return View();
        }
    }
}
