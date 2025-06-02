namespace BibliotecaMvcApp.Models
{
    //Relacionamento entre Livro e Autor (NxN)
    public class LivroAutor
    {
        public Guid Id { get; set; }
        public Guid AutorId { get; set; }
        public Autor Autor { get; set; } = null!;
        public Guid LivroId { get; set; }
        public Livro Livro { get; set; } = null!;

    }
}