using System.Threading.Tasks;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests.Validators
{
    public interface IValidacao
    {
        Task<bool> BeValidDateTimeAsync(DateTime? dateTime);
        Task<bool> ValidarValorTransacaoAsync(decimal? value);
    }
}
