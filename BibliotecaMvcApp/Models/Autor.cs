namespace BibliotecaMvcApp.Models
{
    public class Autor
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime? DataNascimento { get; set; }
        public DateTime? DataFalecimento { get; set; }
        public string Nacionalidade { get; set; } = string.Empty;
        public string Biografia { get; set; } = string.Empty;
        public List<LivroAutor> LivrosAutores { get; set; } = new();
    }
}
