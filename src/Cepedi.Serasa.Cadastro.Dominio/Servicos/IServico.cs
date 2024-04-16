using Refit;

namespace Cepedi.Serasa.Cadastro.Dominio.Services;
public interface IServico
{
    [Post("api/v1/Enviar")]
    Task<ApiResponse<HttpResponseMessage>> EnviarNotificacao([Body] object notificacao);
}
