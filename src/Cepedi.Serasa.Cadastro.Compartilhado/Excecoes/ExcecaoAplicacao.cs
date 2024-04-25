namespace Cepedi.Serasa.Cadastro.Compartilhado.Exececoes;
public class ExcecaoAplicacao : Exception
{
    public ExcecaoAplicacao(ResultadoErro erro)
     : base(erro.Descricao) => ResultadoErro = erro;

    public ResultadoErro ResultadoErro { get; set; }
}
