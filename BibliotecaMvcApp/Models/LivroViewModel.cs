namespace BibliotecaMvcApp.Models
{
    public class LivroViewModel
    {
        public int LivroId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public int AnoPublicacao { get; set; }

        public int? AutorId { get; set; }
        public string? NovoAutor { get; set; }  // Campo para novo autor
    }
}
