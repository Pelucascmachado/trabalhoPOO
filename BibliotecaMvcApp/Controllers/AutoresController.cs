using Microsoft.AspNetCore.Mvc;
using BibliotecaMvcApp.Models;
using BibliotecaMvcApp.Repository.IRepository;

namespace BibliotecaMvcApp.Controllers
{
    public class AutoresController : Controller
    {
        private readonly IAuthorRepository _autorRepository;

        public AutoresController(IAuthorRepository autorRepository)
        {
            _autorRepository = autorRepository;
        }

        // GET: /Autores
        public async Task<IActionResult> Index()
        {
            var autores = await _autorRepository.GetAuthorsWithBooksAsync();
            return View(autores);
        }

        // GET: /Autores/Details/{id}
        public async Task<IActionResult> Details(Guid id)
        {
            var autor = await _autorRepository.GetAuthorWithBooksAsync(id);
            if (autor == null)
                return NotFound();
            return View(autor);
        }

        // GET: /Autores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Autores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Autor autor)
        {
            if (!ModelState.IsValid)
            {
                return View(autor);
            }
            await _autorRepository.AddAsync(autor);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Autores/Edit/{id}
        public async Task<IActionResult> Edit(Guid id)
        {
            var autor = await _autorRepository.GetAsync(a => a.Id == id);
            if (autor == null)
                return NotFound();
            return View(autor);
        }

        // POST: /Autores/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Autor autor)
        {
            if (id != autor.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(autor);

            await _autorRepository.UpdateAsync(autor);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Autores/Delete/{id}
        public async Task<IActionResult> Delete(Guid id)
        {
            var autor = await _autorRepository.GetAuthorWithBooksAsync(id);
            if (autor == null)
                return NotFound();
            return View(autor);
        }

        // POST: /Autores/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var autor = await _autorRepository.GetAsync(a => a.Id == id);
            if (autor == null)
                return NotFound();

            await _autorRepository.DeleteAsync(autor);
            return RedirectToAction(nameof(Index));
        }
    }
}