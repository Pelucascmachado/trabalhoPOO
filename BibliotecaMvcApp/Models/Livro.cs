using BibliotecaMvcApp.Models; // Importa o namespace necessário para reconhecer a classe Autor

namespace BibliotecaMvcApp.Models
{
    // 🧱 Classe que representa um livro no sistema da biblioteca
    public class Livro
    {
        // 🔑 Chave primária do livro (campo único no banco de dados)
        public int LivroId { get; set; }

        // 📕 Título do livro (valor padrão: string vazia para evitar nulo)
        public string Titulo { get; set; } = string.Empty;

        // 📅 Ano de publicação do livro
        public int AnoPublicacao { get; set; }

        // 🔗 Chave estrangeira que associa o livro a um autor (relacionamento N:1)
        public int AutorId { get; set; }

        // 🤝 Propriedade de navegação que permite acessar os dados do autor relacionado
        // ✅ Obrigatória para que o Entity Framework entenda a relação entre Livro e Autor
        public Autor Autor { get; set; } = null!; // null! evita aviso de nulo sem precisar inicializar manualmente
    }
}
