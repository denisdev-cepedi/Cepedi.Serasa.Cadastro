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

            builder.HasOne(c => c.TipoMovimentacao)
                   .WithMany()
                   .HasForeignKey(c => c.IdTipoMovimentacao);
        }
    }
}
