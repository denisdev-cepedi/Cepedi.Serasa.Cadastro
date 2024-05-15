using System;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests.Movimentacao.Validators
{
    public static class ValidacaoUtils
    {
        public static bool BeValidDateTime(DateTime dateTime)
        {
            return dateTime > DateTime.MinValue && dateTime < DateTime.MaxValue;
        }

    }
}
