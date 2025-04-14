// Importa os modelos da aplicação (Autor e Livro)
using BibliotecaMvcApp.Models;

namespace BibliotecaMvcApp.Data
{
    // ✅ Classe estática usada para inicializar e popular o banco com dados de exemplo
    public static class DbInitializer
    {
        // ✅ Método responsável por inserir dados no banco caso ele esteja vazio
        public static void Inicializar(BibliotecaContext context)
        {
            // ✅ Verifica se já existem livros no banco de dados.
            // Isso evita que os dados sejam inseridos mais de uma vez.
            if (context.Livros.Any()) return;

            // ✅ Criação de autores (tabela relacionada com os livros - associação 1:N)
            var autor1 = new Autor { Nome = "Machado de Assis" };
            var autor2 = new Autor { Nome = "Clarice Lispector" };

            // ✅ Criação de lista de livros, já vinculando cada livro a um autor
            // Exemplo de relacionamento entre duas classes via propriedade de navegação
            var livros = new List<Livro>
            {
                new Livro { Titulo = "Dom Casmurro", AnoPublicacao = 1899, Autor = autor1 },
                new Livro { Titulo = "Memórias Póstumas de Brás Cubas", AnoPublicacao = 1881, Autor = autor1 },
                new Livro { Titulo = "A Hora da Estrela", AnoPublicacao = 1977, Autor = autor2 },
                new Livro { Titulo = "Laços de Família", AnoPublicacao = 1960, Autor = autor2 }
            };

            // ✅ Adiciona os autores e livros no contexto (ainda não persistiu no banco)
            context.Autores.AddRange(autor1, autor2);
            context.Livros.AddRange(livros);

            // ✅ Salva todas as alterações no banco de dados (persistência)
            context.SaveChanges();
        }
    }
}
