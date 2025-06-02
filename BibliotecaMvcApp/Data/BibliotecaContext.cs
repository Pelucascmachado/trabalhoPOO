
using Microsoft.EntityFrameworkCore;

using BibliotecaMvcApp.Models;

namespace BibliotecaMvcApp.Data
{
    public class BibliotecaContext : DbContext
    {
        public BibliotecaContext(DbContextOptions<BibliotecaContext> options)
            : base(options)
        {
        }
        public DbSet<Livro> Livros { get; set; } = null!;
        public DbSet<Autor> Autores { get; set; } = null!;
        public DbSet<LivroAutor> LivroAutores { get; set; } = null!;
    }
}
