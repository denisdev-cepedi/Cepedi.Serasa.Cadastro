using Cepedi.Serasa.Cadastro.Shareable.Enums;

namespace Cepedi.Serasa.Cadastro.Shareable.Exceptions;
public class SemResultadosException : ApplicationException
{
    public SemResultadosException() : 
        base(BancoCentralMensagemErrors.SemResultados)
    {
    }
}
