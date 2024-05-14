using System;
using System.Threading.Tasks;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests.Validators
{
    public class Validacao : IValidacao
    {
        public Task<bool> BeValidDateTimeAsync(DateTime? dateTime)
        {
            return Task.FromResult(dateTime.HasValue && dateTime.Value > DateTime.MinValue && dateTime.Value < DateTime.MaxValue);
        }

        public Task<bool> ValidarValorTransacaoAsync(decimal? value)
        {
            return Task.FromResult(value.HasValue && value > 0);
        }


    }
}
