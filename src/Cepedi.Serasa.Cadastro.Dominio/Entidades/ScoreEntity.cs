namespace Cepedi.Serasa.Cadastro.Dominio.Entidades;

public class ScoreEntity
{
    public int Id { get; set; }
    public PessoaEntity? Pessoa { get; set; }
    public required int IdPessoa { get; set; }
    public required double Score { get; set; }
}
