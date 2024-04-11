namespace Cepedi.Serasa.Cadastro.Domain.Entities;

public class ConsultaEntity{
    public int Id { get; set; }
    public int IdPessoa { get; set; }
    public required DateTime Data { get; set; }
    public required bool Status { get; set; }
    public Pessoa Pessoa{ get; set; }
    
}