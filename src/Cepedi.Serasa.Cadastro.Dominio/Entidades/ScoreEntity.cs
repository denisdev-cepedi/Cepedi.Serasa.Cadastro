using System.ComponentModel.DataAnnotations.Schema;

namespace Cepedi.Serasa.Cadastro.Dominio.Entidades;

public class ScoreEntity
{
    public int Id { get; set; }
    public int IdPessoa { get; set; }
    [ForeignKey("IdPessoa")]
    public required double Score { get; set; }

    internal void Atualizar(double score)
    {
        Score = score;
    }
}
