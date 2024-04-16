using Cepedi.Serasa.Cadastro.Compartilhado.Enums;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Exececoes;
public class SemResultadoExcecao : ExcecaoAplicacao
{
    public SemResultadoExcecao() : 
        base(CadastroErros.SemResultados)
    {
    }
}
