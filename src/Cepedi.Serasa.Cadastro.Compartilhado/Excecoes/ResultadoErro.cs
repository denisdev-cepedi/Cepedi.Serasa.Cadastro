using Cepedi.Serasa.Cadastro.Compartilhado.Enums;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Exececoes;
public class ResultadoErro
{
    public string Titulo { get; set; } = default!;

    public string Descricao { get; set; } = default!;

    public ETipoErro Tipo { get; set; }
}
