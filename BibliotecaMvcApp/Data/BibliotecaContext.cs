// Importa os recursos do Entity Framework Core para trabalhar com banco de dados
using Microsoft.EntityFrameworkCore;

// Importa os modelos (entidades) da aplicação: Livro e Autor
using BibliotecaMvcApp.Models;

namespace BibliotecaMvcApp.Data
{
    // ✅ Esta classe representa o "Contexto" da base de dados
    // Ela mapeia as classes C# para tabelas no banco de dados
    public class BibliotecaContext : DbContext
    {
        // Construtor que recebe as configurações de contexto (ex: tipo de banco e string de conexão)
        public BibliotecaContext(DbContextOptions<BibliotecaContext> options)
            : base(options)
        {
        }

        // ✅ Mapeia a classe Livro como uma tabela do banco
        public DbSet<Livro> Livros { get; set; } = null!;

        // ✅ Mapeia a classe Autor como uma tabela do banco
        public DbSet<Autor> Autores { get; set; } = null!;
    }
}
