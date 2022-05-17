namespace SalesWebMVC.Models.ViewModels
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public string Message { get; set; }

        //a função vai retornar se ele não é string nulo ou vazio
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}