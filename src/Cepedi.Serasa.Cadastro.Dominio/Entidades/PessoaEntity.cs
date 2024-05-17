namespace Cepedi.Serasa.Cadastro.Dominio.Entidades;
public class PessoaEntity
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public required string CPF { get; set; }

    // Propriedade de navegação para o Score associado à pessoa
    public ScoreEntity? Score { get; set; }

    internal void Atualizar(string nome, string cpf)
    {
        Nome = nome;
        CPF = cpf;
    }
}
