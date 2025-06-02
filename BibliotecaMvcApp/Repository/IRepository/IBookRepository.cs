using BibliotecaMvcApp.Models;

namespace BibliotecaMvcApp.Repository.IRepository
{
    public interface IBookRepository : IRepository<Livro>
    {
        Task<Livro> GetBookWithAuthorsAsync(Guid id);

        Task<Livro> GetBookByIsbnAsync(string isbn);
        Task<Livro> UpdateAsync(Livro livro);
        Task<Livro> DeleteAsync(Livro livro);
        Task<IEnumerable<Livro>> GetBooksByAuthorAsync(Guid authorId);
        Task<IEnumerable<Livro>> GetBooksByTitleAsync(string title);
        Task<IEnumerable<Livro>> GetBooksByGenreAsync(string genre);
        Task<IEnumerable<Livro>> GetBooksWithAuthorsAsync();
    }
}


