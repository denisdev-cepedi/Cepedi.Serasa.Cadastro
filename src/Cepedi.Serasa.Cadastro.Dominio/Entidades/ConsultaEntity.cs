namespace Cepedi.Serasa.Cadastro.Dominio.Entidades;

public class ConsultaEntity{
    public int Id { get; set; }
    public int IdPessoa { get; set; }
    public required DateTime Data { get; set; }
    public required bool Status { get; set; }
    public PessoaEntity Pessoa{ get; set; }
    
}
