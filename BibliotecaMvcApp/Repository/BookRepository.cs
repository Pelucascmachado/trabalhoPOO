using BibliotecaMvcApp.Data;
using BibliotecaMvcApp.Models;
using BibliotecaMvcApp.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMvcApp.Repository
{
    public class BookRepository : Repository<Livro>, IBookRepository
    {
        private readonly BibliotecaContext _context;

        public BookRepository(BibliotecaContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Livro?> GetBookWithAuthorsAsync(Guid id)
        {
            return await _context.Livros
                .Include(l => l.LivrosAutores)
                .ThenInclude(la => la.Autor)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<IEnumerable<Livro>> GetBooksWithAuthorsAsync()
        {
            return await _context.Livros
                .Include(l => l.LivrosAutores)
                .ThenInclude(la => la.Autor)
                .ToListAsync();
        }

        public async Task<Livro?> GetBookByIsbnAsync(string isbn)
        {
            return await _context.Livros
                .FirstOrDefaultAsync(l => l.ISBN == isbn);
        }

        public async Task<IEnumerable<Livro>> GetBooksByAuthorAsync(Guid authorId)
        {
            return await _context.Livros
                .Where(l => l.LivrosAutores.Any(la => la.AutorId == authorId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Livro>> GetBooksByTitleAsync(string title)
        {
            return await _context.Livros
                .Where(l => l.Titulo.Contains(title))
                .ToListAsync();
        }

        public async Task<IEnumerable<Livro>> GetBooksByGenreAsync(string genre)
        {
            return await _context.Livros
                .Where(l => l.Genero == genre)
                .ToListAsync();
        }

        public async Task<Livro> UpdateAsync(Livro livro)
        {
            _context.Livros.Update(livro);
            await _context.SaveChangesAsync();
            return livro;
        }

        public async Task<Livro> DeleteAsync(Livro livro)
        {
            _context.Livros.Remove(livro);
            await _context.SaveChangesAsync();
            return livro;
        }
    }
}