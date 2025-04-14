namespace BibliotecaMvcApp.Models
{
    // ✅ Classe utilizada na view de erro (Error.cshtml)
    // Ela representa os dados que serão exibidos ao usuário quando ocorrer uma falha
    public class ErrorViewModel
    {
        // 🆔 Armazena o identificador da requisição (útil para rastrear erros em logs)
        // Pode ser nulo, por isso é declarado como string?
        public string? RequestId { get; set; }

        // ✅ Propriedade somente leitura que indica se o RequestId deve ser exibido
        // Retorna true se houver um ID de requisição válido
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
