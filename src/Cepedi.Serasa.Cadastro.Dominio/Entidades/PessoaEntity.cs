namespace Cepedi.Serasa.Cadastro.Dominio.Entidades;
public class PessoaEntity
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public required string CPF { get; set; }
}
