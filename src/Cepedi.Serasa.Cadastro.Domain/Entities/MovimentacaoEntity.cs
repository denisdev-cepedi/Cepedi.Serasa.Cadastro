namespace Cepedi.Serasa.Cadastro.Domain.Entities;
public class MovimentacaoEntity
{
    public int Id { get; set; }
    public DateTime Data { get; set; }
    public string? Tipo { get; set; } // Crédito, Débito, etc.
    public decimal Valor { get; set; }
    public string? Descricao { get; set; } // Descrição da movimentação.
    public string? Categoria { get; set; } // Categoria (ex: Alimentação, Saúde, Lazer).
    public string? Origem { get; set; } // Origem da movimentação (ex: Salário, Compras, Empréstimo).
    public string? Localizacao { get; set; } // Localização geográfica da movimentação.
    public bool Confirmada { get; set; } // Indica se a movimentação foi confirmada.

}
