using BibliotecaMvcApp.Data;
using BibliotecaMvcApp.Models;
using BibliotecaMvcApp.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMvcApp.Repository
{
    public class AuthorRepository : Repository<Autor>, IAuthorRepository
    {
        private readonly BibliotecaContext _context;

        public AuthorRepository(BibliotecaContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Autor?> GetAuthorWithBooksAsync(Guid id)
        {
            return await _context.Autores
                .Include(a => a.LivrosAutores)
                .ThenInclude(la => la.Livro)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Autor?> GetAuthorByNameAsync(string name)
        {
            return await _context.Autores
                .FirstOrDefaultAsync(a => a.Nome == name);
        }

        public async Task<IEnumerable<Autor>> GetAuthorsByBookAsync(Guid bookId)
        {
            return await _context.Autores
                .Where(a => a.LivrosAutores.Any(l => l.Id == bookId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Autor>> GetAuthorsByGenreAsync(string genre)
        {
            return await _context.Autores
                .Where(a => a.LivrosAutores.Any(l => l.Livro.Genero == genre))
                .ToListAsync();
        }

        public async Task<Autor> UpdateAsync(Autor autor)
        {
            _context.Autores.Update(autor);
            await _context.SaveChangesAsync();
            return autor;
        }

        public async Task<Autor> DeleteAsync(Autor autor)
        {
            _context.Autores.Remove(autor);
            await _context.SaveChangesAsync();
            return autor;
        }

        public Task<IEnumerable<Autor>> GetAuthorsWithBooksAsync()
        {
            return _context.Autores
                .Include(a => a.LivrosAutores)
                .ThenInclude(la => la.Livro)
                .ToListAsync()
                .ContinueWith(task => (IEnumerable<Autor>)task.Result);
        }
    }
}