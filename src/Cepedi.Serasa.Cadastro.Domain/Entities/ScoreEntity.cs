namespace Cepedi.Serasa.Cadastro.Domain.Entities;

public class ScoreEntity
{
    public int Id { get; set; }
    public PessoaEntity Pessoa { get; set; }
    public int IdPessoa { get; set; }
    public required double Score { get; set; }
}
