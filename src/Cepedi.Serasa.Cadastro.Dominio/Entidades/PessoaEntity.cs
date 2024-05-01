namespace Cepedi.Serasa.Cadastro.Dominio.Entidades;
public class PessoaEntity
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public required string CPF { get; set; }

    internal void Atualizar(string? nome, string? cpf)
    {
        if(nome is not null)
            Nome = nome;

        if(cpf is not null)
            CPF = cpf;
    }
}
