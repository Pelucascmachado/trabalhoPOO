using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BibliotecaMvcApp.Data;
using BibliotecaMvcApp.Models;
using System.Linq;

namespace BibliotecaMvcApp.Controllers
{
    public class LivrosController : Controller
    {
        private readonly BibliotecaContext _context;

        public LivrosController(BibliotecaContext context)
        {
            _context = context;
        }

        // GET: Livros
        public async Task<IActionResult> Index()
        {
            var livros = _context.Livros.Include(l => l.Autor);
            return View(await livros.ToListAsync());
        }

        // GET: Livros/Create
        public IActionResult Create()
        {
            ViewBag.Autores = _context.Autores.ToList();
            return View();
        }

        // POST: Livros/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LivroViewModel model)
        {
            if (ModelState.IsValid)
            {
                int autorId = 0;

                if (!string.IsNullOrEmpty(model.NovoAutor))
                {
                    var novoAutor = new Autor { Nome = model.NovoAutor };
                    _context.Autores.Add(novoAutor);
                    await _context.SaveChangesAsync();
                    autorId = novoAutor.AutorId;
                }
                else if (model.AutorId.HasValue)
                {
                    autorId = model.AutorId.Value;
                }

                var livro = new Livro
                {
                    Titulo = model.Titulo,
                    AnoPublicacao = model.AnoPublicacao,
                    AutorId = autorId
                };

                _context.Livros.Add(livro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Autores = _context.Autores.ToList();
            return View(model);
        }

        // GET: Livros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var livro = await _context.Livros.FindAsync(id);
            if (livro == null)
                return NotFound();

            var model = new LivroViewModel
            {
                LivroId = livro.LivroId,
                Titulo = livro.Titulo,
                AnoPublicacao = livro.AnoPublicacao,
                AutorId = livro.AutorId
            };

            ViewBag.Autores = _context.Autores.ToList();
            return View(model);
        }

        // POST: Livros/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LivroViewModel model)
        {
            if (id != model.LivroId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    int autorId = 0;

                    if (!string.IsNullOrEmpty(model.NovoAutor))
                    {
                        var novoAutor = new Autor { Nome = model.NovoAutor };
                        _context.Autores.Add(novoAutor);
                        await _context.SaveChangesAsync();
                        autorId = novoAutor.AutorId;
                    }
                    else if (model.AutorId.HasValue)
                    {
                        autorId = model.AutorId.Value;
                    }

                    var livro = await _context.Livros.FindAsync(id);
                    if (livro == null)
                        return NotFound();

                    livro.Titulo = model.Titulo;
                    livro.AnoPublicacao = model.AnoPublicacao;
                    livro.AutorId = autorId;

                    _context.Update(livro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Livros.Any(e => e.LivroId == model.LivroId))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Autores = _context.Autores.ToList();
            return View(model);
        }

        // GET: Livros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var livro = await _context.Livros
                .Include(l => l.Autor)
                .FirstOrDefaultAsync(m => m.LivroId == id);
            if (livro == null)
                return NotFound();

            return View(livro);
        }

        // POST: Livros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var livro = await _context.Livros.FindAsync(id);
            if (livro != null)
            {
                _context.Livros.Remove(livro);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // CONSULTAS PERSONALIZADAS
        // =========================

        // Consulta 1: Livros com seus autores (Junção)
        public async Task<IActionResult> LivrosComAutores()
        {
            var livrosComAutores = from livro in _context.Livros
                                   join autor in _context.Autores on livro.AutorId equals autor.AutorId
                                   select new
                                   {
                                       LivroTitulo = livro.Titulo,
                                       LivroAno = livro.AnoPublicacao,
                                       AutorNome = autor.Nome
                                   };

            var resultado = await livrosComAutores.ToListAsync();
            return View(resultado);
        }

        // Consulta 2: Livros por Autor (Agrupamento)
        public async Task<IActionResult> LivrosPorAutor()
        {
            var livrosPorAutor = await _context.Livros
                .Include(l => l.Autor)
                .GroupBy(l => l.AutorId)
                .Select(group => new
                {
                    AutorNome = group.FirstOrDefault().Autor.Nome,
                    QuantidadeDeLivros = group.Count()
                })
                .ToListAsync();

            return View(livrosPorAutor);
        }

        // Consulta 3: Livros com filtros (Where e Having)
        public async Task<IActionResult> LivrosFiltrados()
        {
            var livrosFiltrados = await _context.Livros
                .Where(l => l.AnoPublicacao > 2000)
                .Include(l => l.Autor)
                .GroupBy(l => l.AutorId)
                .Where(g => g.Count() > 3)
                .Select(group => new
                {
                    AutorNome = group.FirstOrDefault().Autor.Nome,
                    QuantidadeDeLivros = group.Count()
                })
                .ToListAsync();

            return View(livrosFiltrados);
        }
    }
}
