namespace SalesWebMVC.Models.ViewModels
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public string Message { get; set; }

        //a fun��o vai retornar se ele n�o � string nulo ou vazio
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}