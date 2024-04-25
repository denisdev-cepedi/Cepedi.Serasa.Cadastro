namespace Cepedi.Serasa.Cadastro.Dominio.Entidades;
public class TipoMovimentacaoEntity
{
    public int Id { get; set; }
    public required string NomeTipo { get; set; }
    internal void Atualizar(string nomeTipo)
    {
        NomeTipo = nomeTipo;
    }
}