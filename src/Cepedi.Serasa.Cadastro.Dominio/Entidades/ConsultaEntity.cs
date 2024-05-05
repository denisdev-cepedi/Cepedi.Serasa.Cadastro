using System.ComponentModel.DataAnnotations.Schema;

namespace Cepedi.Serasa.Cadastro.Dominio.Entidades;

public class ConsultaEntity{
    public int Id { get; set; }
    public int IdPessoa { get; set; }
    [ForeignKey("IdPessoa")]
    public required DateTime Data { get; set; }
    public required bool Status { get; set; }
    

    internal void Atualizar(bool status)
    {
        Status = status;
    }
}
