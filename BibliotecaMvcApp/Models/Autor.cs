namespace BibliotecaMvcApp.Models
{
    // 👤 Classe que representa a entidade "Autor" no sistema da biblioteca
    public class Autor
    {
        // 🔑 Propriedade que representa a chave primária no banco de dados
        // É usada pelo Entity Framework para identificar o autor de forma única
        public int AutorId { get; set; }

        // 🧾 Nome do autor, com valor padrão como string vazia (evita nulo)
        public string Nome { get; set; } = string.Empty;

        // 📚 Lista de livros que esse autor escreveu
        // ✅ Representa um relacionamento 1:N (um autor → vários livros)
        // ✅ É uma propriedade de navegação, usada pelo Entity Framework
        public List<Livro> Livros { get; set; } = new();
    }
}
