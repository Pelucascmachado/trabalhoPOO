using BibliotecaMvcApp.Models;

namespace BibliotecaMvcApp.Repository.IRepository
{
    public interface IAuthorRepository : IRepository<Autor>
    {
        Task<Autor> GetAuthorWithBooksAsync(Guid id);
        Task<IEnumerable<Autor>> GetAuthorsWithBooksAsync();
        Task<Autor> GetAuthorByNameAsync(string name);
        Task<IEnumerable<Autor>> GetAuthorsByBookAsync(Guid bookId);
        Task<IEnumerable<Autor>> GetAuthorsByGenreAsync(string genre);
        Task<Autor> UpdateAsync(Autor autor);
        Task<Autor> DeleteAsync(Autor autor);
    }
}