namespace Cepedi.Serasa.Cadastro.Compartilhado.Responses
{
    public class DeletarMovimentacaoResponse
    {
        public bool Success { get; }
        public string Message { get; }

        public DeletarMovimentacaoResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
