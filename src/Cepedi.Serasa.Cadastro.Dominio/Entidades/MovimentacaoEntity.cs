using System;

namespace Cepedi.Serasa.Cadastro.Dominio.Entidades
{
    public class MovimentacaoEntity
    {
        public int MovimentacaoId { get; set; } 

        public int PessoaId { get; set; } // ID do Pessoa associado à movimentação

        public PessoaEntity Pessoa { get; set; } 

        public DateTime DataHora { get; set; } 

        public int TipoMovimentacaoId { get; set; } // Identificador do tipo de movimentação (Crédito, Débito, empréstimo, pagamento etc.)

        public TipoMovimentacaoEntity TipoMovimentacao { get; set; }

        public decimal Valor { get; set; } 

        public string? NomeEstabelecimento { get; set; } // Local ou estabelecimento relacionado à movimentação (Supermercado, Lanchonete, etc.)

    }
}
