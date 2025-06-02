using BibliotecaMvcApp.Models;

namespace BibliotecaMvcApp.Data
{
    public static class DbInitializer
    {
        public static void Inicializar(BibliotecaContext context)
        {
            if (context.Livros.Any()) return;
            var autor1 = new Autor { Nome = "Machado de Assis" };
            var autor2 = new Autor { Nome = "Clarice Lispector" };
            var livros = new List<Livro>
            {
                new Livro { Titulo = "Dom Casmurro", Genero = "Romance", NumeroPaginas = 256, Preco = 39.90m, Idioma = "Português", Sinopse = "Bentinho e Capitu.", Editora = "Editora A", ISBN = "978-85-01-00001-1", DataPublicacao = new DateTime(1899, 1, 1) },
                new Livro { Titulo = "Memórias Póstumas de Brás Cubas", Genero = "Romance", NumeroPaginas = 320, Preco = 42.50m, Idioma = "Português", Sinopse = "Narrativa pós-morte.", Editora = "Editora B", ISBN = "978-85-01-00002-8", DataPublicacao = new DateTime(1881, 1, 1) },
                new Livro { Titulo = "A Hora da Estrela", Genero = "Drama", NumeroPaginas = 96, Preco = 29.90m, Idioma = "Português", Sinopse = "Macabéa no Rio.", Editora = "Editora C", ISBN = "978-85-01-00003-5", DataPublicacao = new DateTime(1977, 1, 1) },
                new Livro { Titulo = "Laços de Família", Genero = "Contos", NumeroPaginas = 160, Preco = 34.90m, Idioma = "Português", Sinopse = "Contos sobre família.", Editora = "Editora D", ISBN = "978-85-01-00004-2", DataPublicacao = new DateTime(1960, 1, 1) }
            };
            context.Autores.AddRange(autor1, autor2);
            context.Livros.AddRange(livros);
            context.SaveChanges();

            var livroAutor = new List<LivroAutor>
            {
                new LivroAutor { AutorId = autor1.Id, LivroId = livros[0].Id },
                new LivroAutor { AutorId = autor1.Id, LivroId = livros[1].Id },
                new LivroAutor { AutorId = autor2.Id, LivroId = livros[2].Id },
                new LivroAutor { AutorId = autor2.Id, LivroId = livros[3].Id }
            };

            context.LivroAutores.AddRange(livroAutor);
            context.SaveChanges();
        }
    }
}
