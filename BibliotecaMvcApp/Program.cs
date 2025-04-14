// Importa a classe de contexto do banco de dados
using BibliotecaMvcApp.Data;
// Importa o provedor do EF Core para trabalhar com SQLite
using Microsoft.EntityFrameworkCore;

// Cria o construtor da aplicação (usado para configurar serviços e middlewares)
var builder = WebApplication.CreateBuilder(args);

// ✅ REGISTRO DO BANCO DE DADOS (Persistência)
// Adiciona o contexto BibliotecaContext ao container de serviços com a configuração do SQLite
builder.Services.AddDbContext<BibliotecaContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ REGISTRO DO MVC
// Adiciona suporte a Controllers e Views (estrutura padrão do ASP.NET Core MVC)
builder.Services.AddControllersWithViews();

// Cria o objeto final da aplicação com todas as configurações anteriores
var app = builder.Build();

// ✅ POPULAR O BANCO DE DADOS COM DADOS INICIAIS
// Cria um escopo de serviço para acessar o contexto
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    // Obtém uma instância do contexto configurado acima
    var context = services.GetRequiredService<BibliotecaContext>();

    // ✅ Garante que o banco e as tabelas sejam criados se ainda não existirem
    context.Database.EnsureCreated();

    // ✅ Chama o método que insere os autores e livros no banco
    DbInitializer.Inicializar(context);
}

// ✅ PIPELINE HTTP

// Se não estiver em ambiente de desenvolvimento, ativa tratamento de exceções e segurança
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Adiciona cabeçalhos HTTP Strict Transport Security
}

// Redireciona HTTP para HTTPS
app.UseHttpsRedirection();

// Permite servir arquivos estáticos (como CSS, JS, imagens)
app.UseStaticFiles();

// Ativa o roteamento
app.UseRouting();

// Ativa a autorização (caso use autenticação futura)
app.UseAuthorization();

// ✅ Define a rota padrão da aplicação
// Exemplo: /Home/Index ou /Livros/Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Inicia a aplicação
app.Run();
