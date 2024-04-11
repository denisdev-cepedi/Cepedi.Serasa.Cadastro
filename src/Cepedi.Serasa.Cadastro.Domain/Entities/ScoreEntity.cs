namespace Cepedi.Serasa.Cadastro.Domain;

public class ScoreEntity
{
    public int Id { get; set; }
    public required string IdPessoa { get; set; }
    public required double Score { get; set; }
}
