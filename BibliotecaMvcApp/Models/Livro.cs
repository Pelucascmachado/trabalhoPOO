using BibliotecaMvcApp.Models; 
namespace BibliotecaMvcApp.Models
{
    public class Livro
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public DateTime? DataPublicacao { get; set; }
    public string ISBN { get; set; } = string.Empty;
    public string Editora { get; set; } = string.Empty;
    public string Genero { get; set; } = string.Empty;
    public int NumeroPaginas { get; set; }
    public decimal Preco { get; set; }
    public string Idioma { get; set; } = string.Empty;
    public string Sinopse { get; set; } = string.Empty;
    public List<LivroAutor> LivrosAutores { get; set; } = new();
}
}
