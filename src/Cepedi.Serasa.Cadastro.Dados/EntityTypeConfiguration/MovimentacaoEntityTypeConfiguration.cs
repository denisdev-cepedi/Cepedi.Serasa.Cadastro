using Cepedi.Serasa.Cadastro.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cepedi.Serasa.Cadastro.Dados.EntityTypeConfiguration
{
    public class MovimentacaoEntityTypeConfiguration : IEntityTypeConfiguration<MovimentacaoEntity>
    {
        public void Configure(EntityTypeBuilder<MovimentacaoEntity> builder)
        {
            builder.ToTable("Movimentacao");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.IdPessoa).IsRequired();
            builder.Property(c => c.DataHora).IsRequired(); // Renomeada para DataHora
            builder.Property(c => c.IdTipoMovimentacao).IsRequired();
            builder.Property(c => c.Valor).IsRequired();

            builder.Property(c => c.NomeEstabelecimento).HasMaxLength(255); // Renomeada para NomeEstabelecimento

            // Configurando o relacionamento com PessoaEntity
            builder.HasOne(c => c.Pessoa)
                   .WithMany()  // Uma pessoa pode ter várias movimentações
                   .HasForeignKey(c => c.IdPessoa)  // Chave estrangeira
                   .IsRequired();  // É obrigatório ter uma pessoa associada a uma movimentação

            // Configurando o relacionamento com TipoMovimentacaoEntity
            builder.HasOne(c => c.TipoMovimentacao)
                   .WithMany()  // Um tipo de movimentação pode estar associado a várias movimentações
                   .HasForeignKey(c => c.IdTipoMovimentacao)  // Chave estrangeira
                   .IsRequired();  // É obrigatório ter um tipo de movimentação associado a uma movimentação
        }
    }
}
